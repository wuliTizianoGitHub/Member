﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MISD.SZMDA.Member.Runtime.Domain.Repositories;
using NHibernate;

namespace MISD.SZMDA.Member.Runtime.NHibernateCore.Repositories
{
    internal class NhRepositoryInstaller : IWindsorInstaller
    {
        private readonly ISessionFactory _sessionFactory;

        public NhRepositoryInstaller(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(() => _sessionFactory).LifeStyle.Singleton,
                Component.For(typeof(IRepository<>)).ImplementedBy(typeof(NhRepositoryBase<>)).LifestyleTransient(),
                Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(NhRepositoryBase<,>)).LifestyleTransient()
                );

            //container.AddFacility<NHibernateFacility>();
            //container.Register(
            //     Component.For(typeof(IRepository<>)).ImplementedBy(typeof(NhRepositoryBase<>)).LifestyleTransient(),
            //     Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(NhRepositoryBase<,>)).LifestyleTransient()
            //    );
        }
    }
}
