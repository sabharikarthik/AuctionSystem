using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Azure;
using AuctionSystemCore.AuctionSystemCore.Models;
using AuctionSystemCore.Serializers;
using AuctionSystemCore.Helpers;
using AuctionSystemCore.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json; 
using Newtonsoft.Json.Linq;

namespace AuctionSystemCore.Controllers
{
    [ApiController]
    public class BidsController : ControllerBase
    {

        private auction_systemContext db;

        public BidsController(auction_systemContext dbContext)
        {
            db = dbContext;
        }

        [HttpPost]
        [Route("api/v{v:apiVersion}/events/{event_id}/sold_items/{sold_item_id}/bids")]
        public IActionResult Post([FromRoute] int event_id, [FromRoute] int sold_item_id, [FromBody] BidRequestRoot b_r)
        {
            //could avoid redundancy in this check
            Events e = db.Events.Where(r => r.Id == event_id ).FirstOrDefault();
            if(e == null)
                return StatusCode(404, ErrorMessageHelper.GenerateResponse(404, "Event not found"));

            SoldItems s = db.SoldItems.Where(r => r.EventId == event_id && r.Id == sold_item_id ).FirstOrDefault();
            if (s == null)
                return StatusCode(404,ErrorMessageHelper.GenerateResponse(404, "Sold Item not found"));

            if(! (DateTime.Now >= e.StartTime && DateTime.Now <= e.EndTime) )
                return StatusCode(400,ErrorMessageHelper.GenerateResponse(400, "Event is not active to receive bids"));

            Bids previous_bid = db.Bids.Where(r => r.SoldItemId == s.Id ).FirstOrDefault();
            if (previous_bid != null)
                return StatusCode(400,ErrorMessageHelper.GenerateResponse(400, "Bid found already"));

            Bids b = b_r.createBid(s.Id);
            db.Bids.Add(b);
            db.SaveChanges();
            return Ok(new JObject { { "success", true } });
        }

        [HttpPost]
        [Route("api/v{v:apiVersion}/events/{event_id}/sold_items/{sold_item_id}/generate_winning_bid")]
        public IActionResult Get( [FromRoute] int event_id, [FromRoute] int sold_item_id)
        {
            Events e = db.Events.Where(r => r.Id == event_id ).FirstOrDefault();
            if(e == null)
                return StatusCode(404,ErrorMessageHelper.GenerateResponse(404, "Event not found"));

            SoldItems s = db.SoldItems.Where(r => r.EventId == event_id && r.Id == sold_item_id ).FirstOrDefault();
            if (s == null)
                return StatusCode(404,ErrorMessageHelper.GenerateResponse(404, "Sold Item not found"));

            if(! (DateTime.Now >= e.EndTime) )
                return StatusCode(400,ErrorMessageHelper.GenerateResponse(400, "Event is not yet complete to generate the winning bid"));
            
            List<Bids> b_l = db.Bids.Where(r => r.SoldItemId == s.Id ).ToList();
            int winning_bid = ( b_l.Count == 0 ) ? 0 : WinningBidService.GenerateWinningBid(b_l, s);
            return Ok(new JObject { { "winning_bid", winning_bid } });
        }

    }
}
