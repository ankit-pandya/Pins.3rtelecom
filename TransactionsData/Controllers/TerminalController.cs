using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionsData.Controllers
{
    [Authorize]
    public class TerminalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
