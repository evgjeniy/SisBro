using SisBro.Models;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace SisBro.Controllers
{
    public class HomeController : Controller
    {
        public static Record Record { get; set; } = new()
        {
            AllBro = 0,
            AllSis = 0,
            LastBroMessage = "",
            LastSisMessage = ""
        };
        public IActionResult SendBro()
        {
            string name = (User?.Identity != null && User.Identity.IsAuthenticated) ? User.Identity.Name : "anonymous";
            
            string json = System.IO.File.ReadAllText("records.json");
            Record = JsonSerializer.Deserialize<Record>(json);

            Record.LastBroMessage = "Sent by " + name;
            Record.AllBro++;
            
            System.IO.File.WriteAllText("records.json", JsonSerializer.Serialize(Record));
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SendSis()
        {
            string name = (User?.Identity != null && User.Identity.IsAuthenticated) ? User.Identity.Name : "anonymous";
            
            string json = System.IO.File.ReadAllText("records.json");
            Record = JsonSerializer.Deserialize<Record>(json);

            Record.LastSisMessage = "Sent by " + name;
            Record.AllSis++;
            
            System.IO.File.WriteAllText("records.json", JsonSerializer.Serialize(Record));
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            Record = JsonSerializer.Deserialize<Record>(System.IO.File.ReadAllText("records.json"));
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}