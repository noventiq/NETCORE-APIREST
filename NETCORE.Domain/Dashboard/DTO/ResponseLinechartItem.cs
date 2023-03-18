using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETCORE.Domain.Dashboard.DTO
{
    public class ResponseLinechartItem
    {
        public string Label { get; set; }
        public double Sale { get; set; }
        public double Profit { get; set; }
    }
}
