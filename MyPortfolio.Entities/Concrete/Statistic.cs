using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entities.Concrete
{
    public class Statistic : BaseEntity
    {
        public DateTime Date { get; set; }
        public uint VisitorCount { get; set; }

    }
}
