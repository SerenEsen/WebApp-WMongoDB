using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YazılımSitesi.Models;

namespace YazılımSitesi.Controllers
{
    [Serializable]
    public class Site
    {
        public ObjectId Id;
        public double row_number;
        public string site_name;
        public string site_exp;
        public string link;
        internal string site;

        [BsonElement("Id")]
        public ObjectId _id { get { return Id; } set { Id = value; } }

        [BsonElement("Row_number")]
        public double Row_number { get { return row_number; } set { row_number = value; } }

        [BsonElement("Site_name")]
        public string Site_name { get { return site_name; } set { site_name = value; } }

        [BsonElement("Site_exp")]
        public string Site_exp { get { return site_exp; } set { site_exp = value; } }

        [BsonElement("Link")]
        public string Link { get { return link; } set { link = value; } }

    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            MongoClient client = new MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
           
            IMongoDatabase db = client.GetDatabase("WEBSİTES"); //WEBSİTES adındaki veritabanına bağlantı sağlanıyor.
            IMongoCollection<Site> collection = db.GetCollection<Site>("Websites");//WebSites koleksiyonuna bağlanıyor.
            var result = collection.Find(x => x.Site_name != string.Empty).ToList();//Site ismi veritabanında boş değilse bunu listeleyip result değişkenine atmasını talep eder.
            return View(result.ToList());
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
