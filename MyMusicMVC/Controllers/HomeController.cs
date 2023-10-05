using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyMusicMVC.Models;
using MyMusicMVC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyMusicMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _Config;

        private string URLBase
        { 
            get
            {
                return _Config.GetSection("BaseURL").GetSection("URL").Value;
            }
        }

        public HomeController(ILogger<HomeController> logger,IConfiguration Config)
        {
            _logger = logger;
            _Config = Config;
        }

        public async Task<IActionResult> Index()
        {
            var listMusic = new ListMusicViewModel();

            var musicList = new List<Music>();
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URLBase + "Music"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    musicList = JsonConvert.DeserializeObject<List<Music>>(apiResponse);
                }
            }
            listMusic.ListMusic = musicList;
            return View(listMusic);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
