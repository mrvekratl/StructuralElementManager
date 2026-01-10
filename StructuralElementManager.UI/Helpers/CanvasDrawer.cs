using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace StructuralElementManager.UI.Helpers
{
    public class CanvasDrawer
    {
        private Canvas _canvas;
        private double _scale = 1.0; // 1 cm = 1 pixel başlangıç

        public CanvasDrawer(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Clear()
        {
            _canvas.Children.Clear();
        }

        public void DrawColumn(StructuralColumn column, double x, double y)
        {
            var rect = new Rectangle
            {
                Width = column.Width * _scale,
                Height = column.Depth * _scale,
                Fill = new SolidColorBrush(Color.FromRgb(52, 152, 219)),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            _canvas.Children.Add(rect);

            // Label
            var label = new TextBlock
            {
                Text = column.Name,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                FontSize = 10
            };
            Canvas.SetLeft(label, x + 5);
            Canvas.SetTop(label, y + 5);
            _canvas.Children.Add(label);
        }

        public void DrawBeam(StructuralBeam beam, double x1, double y1, double x2, double y2)
        {
            var line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = new SolidColorBrush(Color.FromRgb(231, 76, 60)),
                StrokeThickness = beam.Width * _scale / 10
            };
            _canvas.Children.Add(line);
        }
    }
}
