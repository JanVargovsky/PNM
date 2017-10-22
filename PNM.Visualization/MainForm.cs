using MathNet.Numerics.Integration;
using MoreLinq;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PNM.Visualization
{
    public partial class MainForm : Form
    {
        const int L = 10;
        const int N = 100;

        readonly IList<float> x, y;
        Func<float, float> func = Functions.Quadratic;
        Func<float, float> doubleIntegrateFunc = DoubleIntegrateFunctions.Quadratic;

        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 800);

            (x, y) = Sit1D.Calculate(L, N, func, 1, 1);
        }

        protected override void OnLoad(EventArgs e)
        {
            var view = new PlotView
            {
                Size = Size,
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS
            };

            Controls.Add(view);

            var model = new PlotModel { Title = "Struna" };
            void AddSerie(string title, DataPoint[] p, OxyColor? color = null)
            {
                var serie = new LineSeries { Title = title };
                if (color.HasValue)
                    serie.Color = color.Value;
                serie.Points.AddRange(p);
                model.Series.Add(serie);
            }

            AddSerie("", new DataPoint[] { new DataPoint(x[0], 0), new DataPoint(x[x.Count - 1], 0) }, OxyColors.Black);
            AddSerie("f", x.Zip(y, (x, y) => new DataPoint(x, func(x))).ToArray());
            AddSerie("Metoda siti", x.Zip(y, (x, y) => new DataPoint(x, -y)).ToArray());
            AddSerie("Analyticke reseni", x.Select(x => new DataPoint(x, doubleIntegrateFunc(x))).ToArray());

            view.Model = model;
        }
    }
}
