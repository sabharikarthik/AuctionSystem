using System;
using System.Collections.Generic;

namespace AuctionSystemCore.AuctionSystemCore.Models
{
    public partial class SoldItems
    {
        public SoldItems()
        {
            Bids = new HashSet<Bids>();
        }

        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public double IncrementLimit { get; set; }

        public Events Event { get; set; }
        public ICollection<Bids> Bids { get; set; }
    }
}
