using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Prism.Commands;
using Prism.Mvvm;
using RCP.Core;
using System;
using System.Linq;
using System.Windows.Input;

namespace RCP.ClientLite.Controls
{
    public class PlotViewModel : BindableBase
    {

        private ICommand refreshCommand;
        private PlotModel plotModel;
        private string overall;


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
                Title = "Godziny", 
                Position = AxisPosition.Left
            };
            PlotModel.Axes.Add(valueAxis);

            this.LoadData();
        }

        public ICommand RefreshCommand => this.refreshCommand == null
            ? refreshCommand = new DelegateCommand(this.LoadData) : this.refreshCommand;

        public string Overall
        {
            get { return this.overall; }
            set { this.SetProperty(ref overall, value); }
        }

        public PlotModel PlotModel
        {
            get { return this.plotModel; }
            set { this.SetProperty(ref plotModel, value); }
        }

        private void LoadData()
        {
            PlotModel.Series.Clear();
            var activities = Kernel.Instance.ActivityRepository.GetAll();

            foreach (var data in activities)
            {
                var lineSerie = new LineSeries
                {
                    LineStyle = LineStyle.Automatic,
                    StrokeThickness = 2,
                    MarkerSize = 3,
                    MarkerStroke = OxyColors.Blue,
                    MarkerType = MarkerType.Square,
                    CanTrackerInterpolatePoints = true,
                    Title = data.Name,
                    Smooth = true,
                };
                lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.StartDate), Math.Round((data.EndDate - data.StartDate).TotalHours,2)));
                PlotModel.Series.Add(lineSerie);
            }
            this.PlotModel.ResetAllAxes();
            this.Overall = $" Godziny:  {activities.Select(a => Math.Round((a.EndDate - a.StartDate).TotalHours, 2)).Sum().ToString()}";
        }
    }
}
