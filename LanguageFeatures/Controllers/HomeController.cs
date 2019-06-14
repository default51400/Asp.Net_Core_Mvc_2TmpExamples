﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageFeatures.Models;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ViewResult> Index()
        {
            long? length = await MyASyncMethods.GetPageLength();

            return View(new string[] { $"Length: {length}" });
        }
    }
}
