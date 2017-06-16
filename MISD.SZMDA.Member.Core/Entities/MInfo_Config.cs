using MISD.SZMDA.Member.Runtime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Core.Entities
{
    public class MInfo_Config : IEntity
    {
        public virtual int MC_ID { get; set; }
        public virtual int MC_Type { get; set; }
        public virtual string MC_Content { get; set; }
        public virtual string MC_UpdateBy { get; set; }
        public virtual DateTime MC_UpdateOn { get; set; }
    }
}
