using EShopAdminApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace EShopAdminApplication.Controllers
{
    public class OrderController : Controller
    {        
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL="http://localhost:5046/api/Admin/GetAllActiveOrders";
            HttpResponseMessage response=client.GetAsync(URL).Result;

            var data=response.Content.ReadAsAsync<List<Order>>().Result;

            return View(data);
        }
        public IActionResult Details(string OrderId)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5046/api/admin/GetOrderDetails";
            var model = new
            {
                Id=OrderId
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL,content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;

            return View(data);
        }
        public IActionResult CreateInvoice(string OrderId)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5046/api/admin/GetOrderDetails";
            var model = new
            {
                Id = OrderId
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;
            return null;
        }
    }
}
