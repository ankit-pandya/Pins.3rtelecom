using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using TransactionsData.Data;
using TransactionsData.Models;

namespace TransactionsData.Controllers
{
    [Authorize]
    public class MTProductsController : Controller
    {
        public MTProductsController(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public IActionResult Index()
        {
            return View(Context.tblMTProducts.OrderBy(p => p.productName).ThenBy(p => p.value).ToList());
        }

        public IActionResult Create()
        {
            var mtp = new MTProductModel();
            return View(mtp);
        }

        [HttpPost]
        public IActionResult Create(MTProductModel mtp)
        {
            Context.tblMTProducts.Add(mtp);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {   
            var DataRecord = Context.tblMTProducts.FirstOrDefault(p => p.id == id);

            if (DataRecord == null)
               return NotFound();

            return View(DataRecord);
        }
        
        [HttpPost]
        public IActionResult Edit(MTProductModel mtp)
        {
            Context.tblMTProducts.Update(mtp);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            var DataRecord = Context.tblMTProducts.FirstOrDefault(p => p.id == id);

            if (DataRecord == null)
                return NotFound();

            Context.Remove(DataRecord);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
