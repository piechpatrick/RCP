using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Prism.Mvvm;
using RCP.Core;
using System;
using System.Linq;

namespace RCP.ClientLite.Controls
{
    public class PlotViewModel : BindableBase
    {
        private PlotModel plotModel;



        public PlotViewModel()
        {
            this.PlotModel = new PlotModel();
            PlotModel.LegendTitle = "Legenda";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var dateAxis = new DateTimeAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Data",
                IntervalLength = 80,
                Position = AxisPosition.Bottom,
                StringFormat = "dd/MM/yy HH:mm"
            };
            PlotModel.Axes.Add(dateAxis);

            var valueAxis = new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Minuty", 
                Position = AxisPosition.Left
            };
            PlotModel.Axes.Add(valueAxis);

            this.LoadData();
        }

        public PlotModel PlotModel
        {
            get { return this.plotModel; }
            set { this.SetProperty(ref plotModel, value); }
        }

        private void LoadData()
        {
            var activities = Kernel.Instance.ActivityRepository.GetAll();
            //var gr = activities.GroupBy(a => a.StartDate);


            foreach (var data in activities)
            {
                var lineSerie = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 3,
                    MarkerStroke = OxyColors.Blue,
                    MarkerType = MarkerType.Square,
                    CanTrackerInterpolatePoints = false,
                    Title = data.Name,
                    Smooth = false,
                };
                lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.StartDate), Math.Round((data.EndDate - data.StartDate).TotalMinutes,2)));
                PlotModel.Series.Add(lineSerie);
            }


        }
    }
}
