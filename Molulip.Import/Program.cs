using Molulip.Import.Extentions;
using Molulip.Import.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Molulip.Import
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World !!!");
        }

        public static List<Meal> GetMeals()
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
