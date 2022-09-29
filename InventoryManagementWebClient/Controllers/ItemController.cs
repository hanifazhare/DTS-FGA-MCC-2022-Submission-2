using InventoryManagementWebClient.Context;
using InventoryManagementWebClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManagementWebClient.Controllers
{
    public class ItemController : Controller
    {
        DBContext dbContext;
        public ItemController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: ItemController
        public ActionResult Index()
        {
            var itemList = dbContext.Items.ToList();
            return View(itemList);
        }

        // GET: ItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(item);
                var result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: ItemController/Edit/5
        public ActionResult Edit(int itemId)
        {
            return View(dbContext.Items.Where(item => item.id == itemId).First());
            //var itemList = dbContext.Items.ToList();
            //return View(itemList.Find(item => item.id == itemId));
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                dbContext.Update(item);
                var result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: ItemController/Delete/5
        public ActionResult Delete(int itemId)
        {
            return View(dbContext.Items.Where(item => item.id == itemId).First());
            //var itemList = dbContext.Items.ToList();
            //return View(itemList.Find(item => item.id == itemId));
        }

        // POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Item item)
        {
            dbContext.Items.Remove(item);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
