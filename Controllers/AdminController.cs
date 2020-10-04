using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HUY_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HUY_Store.Data;
using Microsoft.EntityFrameworkCore;

namespace HUY_Store.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController

        private readonly HUY_StoreContext _context;

        public AdminController(HUY_StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View(_context.Sneaker.ToList());
        }

        #region Đăng xuất đăng nhập


        public IActionResult Login()
        {
            return View();
        }

        // hàm kiểm tra đăng nhập admin trả về true là đăng nhập đúng, trả về false là đăng nhập sai
        public bool KiemTraDN(string username, string pass)
        {
            if (_context.Admin.Where(t => t.UserName == username && t.Password == pass).Count() > 0)
            {
                return true;
            }
            return false;
        }

        // Đăng nhập admin
        [HttpPost]
        public IActionResult Login(IFormCollection collect,[Bind("UserName,Password")] Admin model)
        {
            if (KiemTraDN(model.UserName,model.Password) == true)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewData["Message"] = "Wrong Password or UserName";
                return View(model);
            }
        }

        #endregion

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
