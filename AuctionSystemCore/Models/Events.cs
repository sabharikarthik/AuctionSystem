using System;
using System.Collections.Generic;

namespace AuctionSystemCore.AuctionSystemCore.Models
{
    public partial class Events
    {
        public Events()
        {
            SoldItems = new HashSet<SoldItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<SoldItems> SoldItems { get; set; }
    }
}
