using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MISD.SZMDA.Member.Runtime.Dependency;
using MISD.SZMDA.Member.Application.Service;

namespace MISD.SZMDA.Member.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfigAppService _configAppService;

        public AccountController(IConfigAppService configAppService)
        {
            _configAppService = configAppService;
        }

        public ActionResult Login(string backUrl)
        {
            var data = _configAppService.getall();
            ViewBag.BackUrl = backUrl;
            Session["VerifyCode"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName,string password,string verifyCode,string backUrl)
        {
            if (!string.IsNullOrWhiteSpace(verifyCode) && verifyCode.ToLower().Equals(Session["VerifyCode"].ToString().ToLower()))
            {
                if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
                {
                    
                }
            }
            else
            {

            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string data)
        {
            
            return View();
        }
    }
}