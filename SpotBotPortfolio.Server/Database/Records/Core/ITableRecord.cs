using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotBot.Server.Database.Records.Core
{
    internal interface ITableRecord
    {
        public int? Id { get; set; }
    }
}
