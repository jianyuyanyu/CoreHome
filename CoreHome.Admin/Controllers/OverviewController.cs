﻿using CoreHome.Admin.Attributes;
using CoreHome.Admin.Filter;
using CoreHome.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace CoreHome.Admin.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class OverviewController : Controller
    {
        private static byte[] data;

        private const int length = 1024 * 1024 * 1;

        private readonly IPusher<WebSocket> pusher;

        public OverviewController()
        {
            pusher = new WebSocketPusher();
        }

        private static byte[] GetData()
        {
            if (data == null)
            {
                data = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    data[i] = 1;
                }
            }
            return data;
        }

        public IActionResult Index()
        {
            return View();
        }

        [NoCache]
        [ForceWebSocket]
        public async Task<IActionResult> Pushing()
        {
            await pusher.Accept(HttpContext);
            for (int i = 0; i < 36000 && pusher.Connected; i++)
            {
                try
                {
                    pusher.SendMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff") + $"|{i + 1}").GetAwaiter();
                    await Task.Delay(100);
                }
                catch
                {
                    break;
                }
            }
            return null;
        }

        [NoCache]
        public IActionResult Download()
        {
            HttpContext.Response.Headers.Add("Content-Length", length.ToString());
            return new FileContentResult(GetData(), "application/octet-stream");
        }

        [NoCache]
        public JsonResult GetPing()
        {
            return Json(new List<object>());
        }
    }
}