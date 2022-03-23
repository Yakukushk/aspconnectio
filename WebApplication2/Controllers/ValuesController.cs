using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private string str = "Ціна";
        private string str_ = "Країна";
        private readonly DBLibraryContext _contextl;

        public ValuesController(DBLibraryContext contextl)
        {
            _contextl = contextl;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {

            var categories_ = _contextl.Tour.ToList();
            List<object> list = new List<object>();
            list.Add(new[] { str, str_ });
            foreach (var m in categories_)
            {
                list.Add(new object[] { m.Name, m.Price });
            }
            return new JsonResult(list);


        } 
    }
}
