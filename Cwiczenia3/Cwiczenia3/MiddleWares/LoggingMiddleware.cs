﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;
using System.Text;
using Cwiczenia3.Services;
namespace Cwiczenia3.MiddleWares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, DAL.IDbService service)
        {
            if (httpContext.Request != null)
            {
                string sciezka = httpContext.Request.Path; //"weatherforecast/cos"
                string querystring = httpContext.Request?.QueryString.ToString();
                string metoda = httpContext.Request.Method.ToString();
                string bodyStr = "";

                using (StreamReader reader
                 = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                }

                //logowanie do pliku
            }

            await _next(httpContext);
        }


    }
}
