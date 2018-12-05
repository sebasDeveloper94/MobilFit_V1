using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MobilFit_v1.Service;
using MobilFit_v1.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using dataChart = Microcharts.Entry;
using SkiaSharp;
using Microcharts;

namespace MobilFit_v1.ViewModels
{
    public class PerformanceViewModel : BaseViewModel
    {
        public ApiService apiService;

        #region attributes
        private LineChart chart;
        private string porcentajeRutinas;
        private string tiempoEntrenamiento;
        private string caloriasQuemadas;
        private string imc;
        private string igc;
        private ReporteDesempeño reporte;
        private Desempeño desempeño;
        #endregion

        #region Properties
        public LineChart Chart
        {
            get { return this.chart; }
            set { SetValue(ref this.chart, value); }
        }
        public string PorcentajeRutinas
        {
            get { return this.porcentajeRutinas; }
            set { SetValue(ref this.porcentajeRutinas, value); }
        }
        public string TiempoEntrenamiento
        {
            get { return this.tiempoEntrenamiento; }
            set { SetValue(ref this.tiempoEntrenamiento, value); }
        }
        public string CaloriasQuemadas
        {
            get { return this.caloriasQuemadas; }
            set { SetValue(ref this.caloriasQuemadas, value); }
        }
        public string IMC
        {
            get { return this.imc; }
            set { SetValue(ref this.imc, value); }
        }
        public string IGC
        {
            get { return this.igc; }
            set { SetValue(ref this.igc, value); }
        }
        public ReporteDesempeño Reporte
        {
            get { return this.reporte; }
            set { SetValue(ref this.reporte, value); }
        }
        public Desempeño Desempeño
        {
            get { return this.desempeño; }
            set { SetValue(ref this.desempeño, value); }
        }
        #endregion

        #region Constructor
        public PerformanceViewModel()
        {
            //apiService = new ApiService();
            this.Reporte = new ReporteDesempeño();
            //this.LoadReport();
        }
        #endregion

        #region Methods
        public async void LoadReport()
        {
            string rutinas = this.Reporte.PorcentajeRutina.ToString("0.0");
            string tiempo = this.Reporte.TiempoEntrenamiento.ToString("0.0");
            string calorias = this.Reporte.CaloriasQuemadas.ToString("0.000");
            string imc = this.Reporte.IMC.ToString("0.0");
            string igc = this.Reporte.IGC.ToString("0.0");

            this.PorcentajeRutinas = rutinas;
            this.TiempoEntrenamiento = tiempo;
            this.CaloriasQuemadas = calorias;
            this.IMC = imc;
            this.IGC = igc;

            List<dataChart> dataList = new List<dataChart>
            {
                new dataChart(float.Parse(PorcentajeRutinas))
                {
                   Color = SKColor.Parse("9c27b0"),
                   TextColor = SKColor.Parse("9c27b0"),
                   Label = "Porcentaje de rutinas completadas del plan:",
                   ValueLabel = porcentajeRutinas
                },
                new dataChart(float.Parse(TiempoEntrenamiento))
                {
                   Color = SKColor.Parse("9c27b0"),
                   TextColor = SKColor.Parse("9c27b0"),
                   Label = "Total de tiempo en entrenamiento:",
                   ValueLabel = TiempoEntrenamiento
                },
                new dataChart(float.Parse(CaloriasQuemadas))
                {
                   Color = SKColor.Parse("9c27b0"),
                   TextColor = SKColor.Parse("9c27b0"),
                   Label = "Calorías Quemadas:",
                   ValueLabel = CaloriasQuemadas
                },
                new dataChart(float.Parse(IMC))
                {
                   Color = SKColor.Parse("9c27b0"),
                   TextColor = SKColor.Parse("9c27b0"),
                   Label = "índice de masa corporal:",
                   ValueLabel = IMC
                },
                new dataChart(float.Parse(IGC))
                {
                   Color = SKColor.Parse("9c27b0"),
                   TextColor = SKColor.Parse("9c27b0"),
                   Label = "índice de grasa corporal:",
                   ValueLabel = IGC
                },
            };
            this.Chart = new LineChart()
            {
                Entries = dataList
            };


            this.PorcentajeRutinas = rutinas + " %";
            this.TiempoEntrenamiento = tiempo;
            this.CaloriasQuemadas = calorias;
            this.IMC = imc+ " %";
            this.IGC = igc + " %";
        }
        #endregion
    }
}
