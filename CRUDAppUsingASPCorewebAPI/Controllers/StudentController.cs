﻿using CRUDAppUsingASPCorewebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace CRUDAppUsingASPCorewebAPI.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7102/api/StudentAPI/";
        private HttpClient client=new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new();
            HttpResponseMessage response= client.GetAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {
                string result=response.Content.ReadAsStringAsync().Result;
                var data=JsonConvert.DeserializeObject<List<Student>>(result);
                if(data !=null)
                {
                    students = data;
                }
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            //to do : convert std in to json
            string data=JsonConvert.SerializeObject(std);
            //to do : formated text we use stringcontent (stringcontent return content)
            StringContent content=new StringContent(data,Encoding.UTF8,"application/json");
            HttpResponseMessage response=client.PostAsync(url,content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        { 
            Student std= new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }
    }
}
