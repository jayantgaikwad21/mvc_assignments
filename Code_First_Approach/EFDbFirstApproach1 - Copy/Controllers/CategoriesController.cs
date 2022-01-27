﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproach1.Models;

namespace EFDbFirstApproach1.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            ComponyDbContext db = new ComponyDbContext();
            List<Category> categorise = db.Categories.ToList();
            return View(categorise);
        }
    }
}