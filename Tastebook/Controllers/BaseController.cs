using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tastebook.Models;

namespace Tastebook.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext Db = new ApplicationDbContext();
    }
}