using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Net.Mime;
using Accord.Math;
using NeuralNet;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private const int arrSize = 28;
        Net net = Init.InitNetwork(@"D:\jiriv\Documents\Visual Studio 2017\Projects\MnistWebApp\JsonTest\ver1.json");
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxActionResult(string imageString)
        {
            Bitmap resized = ((Bitmap)Init.InitBitmap(imageString)).New(arrSize, arrSize).Normalize();
            double[][] input = resized.ToArray(arrSize);
            double[] result = net.FeedForward(input.Reshape());
            double sum = result.Sum();
            ViewBag.Result = result.Select(a=>a/sum).ToArray();
            ViewBag.Input = input;
            return PartialView();
        }

    }
}