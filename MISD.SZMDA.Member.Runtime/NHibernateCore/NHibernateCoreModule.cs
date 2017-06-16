using MISD.SZMDA.Member.Runtime.Dependency;
using MISD.SZMDA.Member.Runtime.Modules;
using NHibernate;
using MISD.SZMDA.Member.Runtime.NHibernateCore.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Automapping;
using MISD.SZMDA.Member.Runtime.Domain.Entities;
using MISD.SZMDA.Member.Runtime.Attributes;
using Castle.Core.Internal;
using System;
using MISD.SZMDA.Member.Runtime.NHibernateCore.Repositories;
using MISD.SZMDA.Member.Runtime.NHibernateCore.Configuration.Startup;
using System.Reflection;

namespace MISD.SZMDA.Member.Runtime.NHibernateCore
{
    [DependsOn(typeof(KernelModule))]
    public class NHibernateCoreModule : BaseModule
    {
        private ISessionFactory _sessionFactory;

        //private string _nameOrconnectionString;

        //public NHibernateCoreModule()
        //{
        //}
        //public NHibernateCoreModule(string nameOrconnectionString)
        //{
        //    _nameOrconnectionString = nameOrconnectionString;
        //}

        /// 判断实体正确性
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected virtual bool IsDomainEntity(Type t)
        {
            return typeof(IEntity).IsAssignableFrom(t);
        }

        /// <summary>
        /// 应该忽略的属性
        /// </summary>
        /// <param name="Property"></param>
        private void ShouldIgnoreProperty(IPropertyIgnorer Property)
        {
            Property.IgnoreProperties(p => p.MemberInfo.HasAttribute<DoNotMapAttribute>());
        }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <returns></returns>
        //protected virtual IPersistenceConfigurer SetupDatabase()
        //{
        //    return MsSqlConfiguration.MsSql2008.UseOuterJoin()
        //        .ConnectionString(x => x.FromConnectionStringWithKey(_nameOrconnectionString)).ShowSql();
        //}

        /// <summary>
        /// 引用表和列
        /// </summary>
        /// <param name="config"></param>
        protected virtual void ConfigurePersistence(NHibernate.Cfg.Configuration config)
        {
            SchemaMetadataUpdater.QuoteTableAndColumns(config);
        }

        /// <summary>
        /// 创建映射模型
        /// </summary>
        /// <returns></returns>
        protected virtual AutoPersistenceModel CreateMappingModel()
        {
            return AutoMap.Assembly(typeof(IEntity).Assembly).Where(IsDomainEntity).OverrideAll(ShouldIgnoreProperty).IgnoreBase<IEntity>();
        }

        public override void PreInitialize()
        {
            IocManager.Register<INHibernateModuleConfiguration, NHibernateModuleConfiguration>();
            //Configuration.ReplaceService<IUnitOfWorkFilterExecuter, NhUnitOfWorkFilterExecuter>(DependencyLifeStyle.Transient);
        }


        public override void Initialize()
        {

            _sessionFactory = Configuration.Modules.NHibernateConfig().FluentConfiguration
                  .Mappings(m => m.AutoMappings.Add(CreateMappingModel))
                   .ExposeConfiguration(ConfigurePersistence)
                //.Mappings(m => m.FluentMappings.Add(typeof(MayHaveTenantFilter)))
                //.Mappings(m => m.FluentMappings.Add(typeof(MustHaveTenantFilter)))
                //.ExposeConfiguration(config => config.SetInterceptor(IocManager.Resolve<AbpNHibernateInterceptor>()))
                .BuildSessionFactory();

            //_sessionFactory = Fluently.Configure()
            //           .Database(SetupDatabase())
            //           .Mappings(m => m.AutoMappings.Add(CreateMappingModel))
            //           .ExposeConfiguration(ConfigurePersistence)
            //           .BuildSessionFactory();

            IocManager.IocContainer.Install(new NhRepositoryInstaller(_sessionFactory));
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void Shutdown()
        {
            _sessionFactory.Dispose();
        }
    }
}
