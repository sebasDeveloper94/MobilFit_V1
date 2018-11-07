using Microcharts;
using MobilFit_v1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dataChart = Microcharts.Entry;
using SkiaSharp;

namespace MobilFit_v1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfomancePage : ContentPage
    {
        public PerfomancePage()
        {
            InitializeComponent();
            this.BindingContext = new PerfomanceViewModel();

            Makegraphics();

        }

        private void Makegraphics()
        {
            List<dataChart> dataList = new List<dataChart>
            {
                new dataChart(3.4f)
                {
                   Color = SKColor.Parse("9c27b0"),
                   TextColor = SKColor.Parse("9c27b0"),
                   Label = "IMC:",
                   ValueLabel = "82.2"
                },
                new dataChart(7.4f)
                {
                   Color = SKColor.Parse("1e88e5"),
                   TextColor = SKColor.Parse("1e88e5"),
                   Label = "Masa muscular:",
                   ValueLabel = "14.5"
                },
                new dataChart(6.4f)
                {
                   Color = SKColor.Parse("cddc39"),
                   TextColor = SKColor.Parse("cddc39"),
                   Label = "IGM;",
                   ValueLabel = "72.2"
                },
                new dataChart(2.4f)
                {
                   Color = SKColor.Parse("00b0ff"),
                   TextColor = SKColor.Parse("00b0ff"),
                   Label = "Peso:",
                   ValueLabel = "104kg"
                },

            };
            grafica1.Chart = new DonutChart
            {
                Entries = dataList
            };
        }
    }
}