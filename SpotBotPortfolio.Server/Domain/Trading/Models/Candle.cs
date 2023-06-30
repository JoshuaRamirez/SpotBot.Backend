using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotBot.Server.Domain.Trading.Models
{
    public class Candle
    {
        public Candle()
        {
            Date = new DateTime();
            Open = 0m;
            Close = 0m;
            High = 0m;
            Low = 0m;
            Volume = 0m;
        }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
    }
}
