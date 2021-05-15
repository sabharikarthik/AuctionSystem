using System;
using System.Collections.Generic;

namespace AuctionSystemCore.AuctionSystemCore.Models
{
    public partial class Users
    {
        public Users()
        {
            Bids = new HashSet<Bids>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Bids> Bids { get; set; }
    }
}
