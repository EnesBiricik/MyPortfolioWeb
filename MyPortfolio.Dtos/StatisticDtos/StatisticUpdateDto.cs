using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Dtos
{
    public class StatisticUpdateDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public uint VisitorCount { get; set; }
    }
}
