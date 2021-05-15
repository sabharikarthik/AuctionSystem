using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AuctionSystemCore.AuctionSystemCore.Models;

namespace AuctionSystemCore.Serializers
{
    public class BidRequest
    {
        [Required] 
        public float start_bid { get; set; }

        [Required]
        public float max_bid { get; set; }

        [Required]
        public float increment_amount { get; set; }

        public Bids createBid(int sold_item_id)
        {
            return new Bids
            {
                StartBid = start_bid,
                MaxBid = max_bid,
                IncrementAmount = increment_amount,
                SoldItemId = sold_item_id
            };
        }

    }

    public class BidRequestRoot
    {
        [Required]
        public BidRequest bid { get; set; }

        public Bids createBid(int sold_item_id)
        {
            return bid.createBid(sold_item_id);
        }
    }

}