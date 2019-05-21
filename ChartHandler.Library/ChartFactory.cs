using System.Linq;
using System.Web.UI.DataVisualization.Charting;

namespace ChartHandler.Library
{
    public class ChartFactory
    {
        public static Chart CreateTestChart(int width, int height, string title, int predefinedFormat, string xAxisName,
            string yAxisName, string series1Name, string series1ChartType, object[] series1XValues,
            object[] series1YValues)
        {
            var chart = new Chart();

            chart.Width = width;
            chart.Height = height;

            // Series
            if (!string.IsNullOrWhiteSpace(series1Name))
                AddSeries(chart, series1Name, series1ChartType, series1XValues, series1YValues);

            // Title
            if (!string.IsNullOrWhiteSpace(title))
                chart.Titles.Add(new Title(title));

            var chartArea = new ChartArea();

            // x Axis Name
            if (!string.IsNullOrWhiteSpace(xAxisName))
                chartArea.AxisX.Title = xAxisName;

            // y Axis Name
            if (!string.IsNullOrWhiteSpace(yAxisName))
                chartArea.AxisY.Title = yAxisName;

            chart.ChartAreas.Add(chartArea);

            chart = StyleWithPredefinedFormat(predefinedFormat, series1ChartType, chart);

            return chart;
        }

        private static Chart StyleWithPredefinedFormat(int predefinedFormat, string series1ChartType, Chart chart)
        {
            if ((predefinedFormat == 1 && series1ChartType == "line") || (predefinedFormat == 1 && series1ChartType == "area"))
            {
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart.ChartAreas[0].AxisX.Interval = 1;

                var series = chart.Series.First();

                series.BorderWidth = 3;
                series.BorderColor = System.Drawing.ColorTranslator.FromHtml("#258DC5");
                series.BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#EAF4F9");
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 7;
                series.MarkerColor = System.Drawing.ColorTranslator.FromHtml("#258DC5");
            }

            return chart;
        }

        private static void AddSeries(Chart chart, string name, string chartType, object[] xValues, object[] yValues)
        {
            var series = new Series(name);

            switch (chartType)
            {
                case "bar":
                    series.ChartType = SeriesChartType.Bar;
                    break;

                case "column":
                    series.ChartType = SeriesChartType.Column;
                    break;

                case "pie":
                    series.ChartType = SeriesChartType.Pie;
                    break;

                case "line":
                    series.ChartType = SeriesChartType.Line;
                    break;

                case "area":
                    series.ChartType = SeriesChartType.Area;
                    break;
            }

            for (int pointNumber = 0; pointNumber < xValues.Length; pointNumber++)
            {
                var xValue = xValues[pointNumber].ToString();

                series.Points.AddXY(xValue, yValues[pointNumber]);
            }

            chart.Series.Add(series);
        }
    }
}
