using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json; 
using Newtonsoft.Json.Linq;
using AuctionSystemCore.AuctionSystemCore.Models;

namespace AuctionSystemCore.Services
{
    public static class WinningBidService
    {
        public static float GenerateWinningBid(List<Bids> b_l, SoldItems s)
        {
            IDictionary<int, float> current_user_bids = new Dictionary<int, float>();
            float current_item_bid = s.BasePrice;
            float current_user_bid;
            foreach(Bids b in b_l)
            {
                if (current_user_bids.TryGetValue(b.UserId, out current_user_bid)) 
                {
                    if(current_user_bid > current_item_bid && current_user_bid <= s.IncrementLimit)
                    {
                        current_item_bid = current_user_bid;
                    }else if(current_item_bid > current_user_bid && current_user_bid + b.IncrementAmount <= s.IncrementLimit && current_user_bid + b.IncrementAmount <= b.MaxBid)
                    {
                        current_user_bid = current_user_bid + b.IncrementAmount;
                        current_item_bid = current_user_bid;
                        current_user_bids[b.UserId] = current_user_bid;
                    }
                }else if(b.StartBid < s.IncrementLimit && b.StartBid <= b.MaxBid){
                    current_user_bids.Add(b.UserId, b.StartBid);
                }
            }
            return current_item_bid;
        }

    }

}