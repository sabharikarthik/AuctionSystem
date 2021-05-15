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

namespace AuctionSystemCore.Helpers
{
    public static class ErrorMessageHelper
    {
        public static JObject GenerateResponse(int status, string msg)
        {
            return new JObject{ { "status_code", status }, { "message", "Event not found." } };
        }

    }

}