using System;
using System.Collections.Generic;

namespace AuctionSystemCore.AuctionSystemCore.Models
{
    public partial class Bids
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SoldItemId { get; set; }
        public double StartBid { get; set; }
        public double MaxBid { get; set; }
        public double IncrementAmount { get; set; }

        public SoldItems SoldItem { get; set; }
        public Users User { get; set; }
    }
}
