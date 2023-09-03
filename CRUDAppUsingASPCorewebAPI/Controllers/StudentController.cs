using CRUDAppUsingASPCorewebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    }
}
