using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NetCorePractice.Models;

namespace NetCorePractice.Controllers
{
    public class ValueController : Controller
    {
        private IConfiguration _config;
        public DemoUser _demoUser { get; set; }
        public PhoneNumber _phoneNumber { get; set; }
        public ValueController(IConfiguration config, IOptions<DemoUser> demoUser, IOptions<PhoneNumber> phoneNumber)
        {
            _config = config;
            _demoUser = demoUser.Value;
            _phoneNumber = phoneNumber.Value;
        }
        [HttpGet]
        public ActionResult Index()
        {
            Demo demo = new Demo
            {
                Name= _demoUser.Name,
                Phone=_phoneNumber.Phone,
                Tel=_phoneNumber.Tel,
                Address=_demoUser.Address,
                Email=_demoUser.Email,
                Age=_demoUser.Age,
                IsActive=_demoUser.IsActive,
                Facebook = _config.GetValue<string>("DemoUrl:Facebook"),
                Google = _config.GetValue<string>("DemoUrl:Google"),
                Yahoo = _config.GetValue<string>("DemoUrl:Yahoo"),
                ConfigValue = _config.GetValue<string>("ConfigValue")
            };

           



            return View(demo);
        }


    }
}
