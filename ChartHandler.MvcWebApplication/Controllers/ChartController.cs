using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.Xml.Linq;
using ChartHandler.Library;

namespace ChartHandler.MvcWebApplication.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult View(int w, int h, string t, int f, string xn, string yn, string s1n, string s1t, string s1xv, string s1yv)
        {
            // w = width
            // h = height
            // t = title
            // f = predefined format
            // xn = x-axis name
            // yn = y-axis name
            // s1n = series 1 Name
            // s1xv = series 1 x Values
            // s1yv = series 1 y Values

            var xValues = s1xv.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            var yValues = s1yv.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            var chart = ChartFactory.CreateTestChart(w, h, t, f, xn, yn, s1n, s1t, xValues, yValues);

            using (var memoryStream = new MemoryStream())
            {
                chart.SaveImage(memoryStream, ChartImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return File(memoryStream.ToArray(), "image/png", "mychart.png");
            }
        }
    }
}