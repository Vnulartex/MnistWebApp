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
        Net net = Net.Init(@"C:\Users\jiriv\source\repos\MnistWebApp\JsonTest\ver1.json");
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxActionResult(string imageString)
        {
            Bitmap resized = ((Bitmap)Functions.CreateBitmap(imageString)).Resize(arrSize, arrSize);
           // Bitmap resized = new Bitmap(@"C:\Users\jiriv\source\repos\MnistWebApp\mnist examples\Opera Snímek_2018-02-19_234304_www.tensorflow.org.png").Resize(arrSize,arrSize);
            double[][] input = resized.Enumerate(arrSize);
            double[] result = net.FeedForward(input.Reshape());
            double sum = result.Sum();
            ViewBag.Result = result.Select(a=>a/sum).ToArray();
            ViewBag.Input = input;
            return PartialView();
        }

    }
}