
using System.Collections.Generic;
namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public static class BarChartDataViewModel
    {
        public static IList<SeriesViewModel> GetSeries()
        {
            string[] periodes = new string[] { "January", "February", "March", "April", "Mei" };
            string[] series = new string[] { "x", "y", "z" };
            Dictionary<string, IList<double>> values = new Dictionary<string, IList<double>>();
            values.Add(series[0], new double[] { 423.721, 178.719, 308.845, 348.555, 160.274 });
            values.Add(series[1], new double[] { 476.851, 195.769, 335.793, 374.771, 182.373 });
            values.Add(series[2], new double[] { 528.904, 227.271, 372.576, 418.258, 211.727 });

            List<SeriesViewModel> result = new List<SeriesViewModel>();
            foreach (string serie in series)
                for (int i = 0; i < periodes.Length; i++)
                    result.Add(new SeriesViewModel(periodes[i], serie, values[serie][i]));
            return result;
        }

    }

    public class SeriesViewModel
    {
        string series;
        string periode;
        double value;

        public string Series { get { return series; } }
        public string Periode { get { return periode; } }
        public double Value { get { return value; } }

        public SeriesViewModel(string series, string periode, double value)
        {
            this.series = series;
            this.periode = periode;
            this.value = value;
        }
    }
}