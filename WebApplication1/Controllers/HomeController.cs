using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using NeuralNet;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private const int arrSize = 28;
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxActionResult(string imageString)
        {
            Bitmap resized = ((Bitmap)Functions.CreateBitmap(imageString)).Resize(arrSize, arrSize);
            double[][] canvasDoubles = resized.Enumerate(arrSize);
            ViewBag.Input = canvasDoubles;
            return PartialView();
        }

    }
}