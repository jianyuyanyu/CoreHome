﻿using Infrastructure.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public static class BingWallpaperService
    {
        public static async Task<string> GetUrl()
        {
            string url = "https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1";
            using HttpClient httpClient = new HttpClient();
            string jsonStr = await httpClient.GetStringAsync(url);
            BingWallpaper wallpaper = JsonSerializer.Deserialize<BingWallpaper>(jsonStr);
            return "https://cn.bing.com" + wallpaper.images[0].url;
        }
    }
}