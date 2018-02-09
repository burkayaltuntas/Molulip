using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Molulip.Models;
using System.IO;
using OfficeOpenXml;
using System.Text;
using Molulip.Extentions;

namespace Molulip.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var x = GetMeals();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<Meal> GetMeals()
        {
            var mealList = new List<Meal>();

            try
            {
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                var fileName = "Feb.xlsx";

                var filePath = Path.GetFullPath(Path.Combine(baseDirectory, fileName));

                FileInfo file = new FileInfo(filePath);

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                    mealList = worksheet.ConvertSheetToObjects<Meal>().ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return mealList;
        }
    }
}
