using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VaccineManagementMVC.Models;

namespace VaccineManagementMVC.Controllers
{
   
    public class AdminController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44387/api");
        HttpClient client;
        public AdminController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        // GET: Admin
        public ActionResult Index()
        {
            List<User> l = new List<User>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<User>>(Data);
            }
            return View(l);

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            List<Admin> l = new List<Admin>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Admin").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<Admin>>(Data);
            }
            string username = Request["email"].ToString();
            string password = Request["password"].ToString();
            var found = l.Find(x => x.AdminEmail== username);
            if (found != null)
            {
                if (found.AdminPassword == password)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = "Incorrect password";
                }
            }
            else
            {
                ViewBag.Msg = "Admin not Found";
            }


            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(baseAddress + "/user", Content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            User l = new User();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<User>(Data);
            }
            return View(l);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(baseAddress + "/user/" + model.UserId, Content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            User l = new User();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<User>(Data);
            }
            return View(l);
        }
        [HttpPost]
        public ActionResult Delete(User model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync(data).Result;


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}