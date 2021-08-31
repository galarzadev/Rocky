using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rocky.Data;
using Rocky.Models;
using Rocky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> obList = _db.Category;
            return View(obList);
        }

        public IActionResult Create() //get - create
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj) //post -create
        {
            if (ModelState.IsValid)
            {
            _db.Category.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Edit( int? id) //get - edit
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj); //shows placeholder with data i am about to edit
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj) //post - edit
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id) //get - delete
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj); //shows placeholder with data i am about to edit
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id) //post - delete
        {
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

                _db.Category.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");


        }

    }
}
