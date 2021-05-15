using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System;
using System.Data.SqlClient;
using Newtonsoft.Json; 
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
// using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AuctionSystemCore.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private static readonly Regex FKConstraintRegex =
                    new Regex("FK__([a-zA-Z0-9]*)__", RegexOptions.Compiled);
        private static readonly Regex FKTableRegex =
                    new Regex("dbo.([a-zA-Z0-9]*)", RegexOptions.Compiled);
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError($"Something went wrong with DbUpdateException:");
                var sqlEx = ex?.InnerException as SqlException;
                string exMsg = "";
                if (sqlEx != null)
                {
                    if(sqlEx.Number == 547)
                    {
                        var constraintMatches = FKConstraintRegex.Matches(sqlEx.Errors[0].Message);
                        var tableMatches = FKTableRegex.Matches(sqlEx.Errors[0].Message);
                        exMsg = ( constraintMatches.Count > 0 && tableMatches.Count > 0 ) ?
                            $"Invalid reference of {tableMatches[0].Groups[1].Value} in table {constraintMatches[0].Groups[1].Value}" :
                            "Invalid Foreign key exception";
                    }
                }
                await HandleExceptionAsync(httpContext, 400, exMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, 500, ex.Message);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(new JObject
            {
                { "StatusCode", context.Response.StatusCode},
                { "Message",  message}
            }.ToString());
        }
    }
}