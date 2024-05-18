﻿using CoreHome.Infrastructure.Models;
using System.Text.Json;

namespace CoreHome.Infrastructure.Services
{
    public class BingWallpaperService
    {
        private string urlCache;
        private int lastDay;

        private readonly HttpClient httpClient;

        public BingWallpaperService()
        {
            httpClient = new()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }

        public async Task<string> GetUrl()
        {
            int nowDay = DateTime.Now.Day;

            if (nowDay == lastDay)
            {
                return urlCache;
            }
            else
            {
                try
                {
                    string url = "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1";

                    string jsonStr = await httpClient.GetStringAsync(url);
                    BingWallpaper wallpaper = JsonSerializer.Deserialize<BingWallpaper>(jsonStr);

                    urlCache = "https://www.bing.com/" + wallpaper.images[0].url;
                    lastDay = nowDay;
                    return urlCache;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
