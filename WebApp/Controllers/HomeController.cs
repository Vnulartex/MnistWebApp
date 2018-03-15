using System.Linq;
using System.Web.Mvc;
using System.Drawing;
using System.Text;
using Accord.Math;
using NeuralNet;
using WebApplication1.Properties;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private const int arrSize = 28;
        private readonly Net net = Init.InitNetwork(Encoding.UTF8.GetString(Resource.ver1)); 
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxActionResult(string imageString)
        {
            //try catch
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