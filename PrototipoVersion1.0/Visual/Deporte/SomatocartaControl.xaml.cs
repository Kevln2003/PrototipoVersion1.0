// Somatocarta.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.Foundation;

namespace PrototipoVersion1._0.Visual.Deporte
{
    public class Somatocarta
    {
        private Canvas canvas;
        private Grid container;
        private Dictionary<string, TextBox> plieguesTextBoxes;

        public Somatocarta(Grid somatocartaContainer)
        {
            container = somatocartaContainer;
            canvas = new Canvas();
            canvas.Background = new SolidColorBrush(Colors.White);

            // Limpiar el contenedor y agregar el canvas
            container.Children.Clear();
            container.Children.Add(canvas);

            // Dibujar la cuadrícula base
            DrawBaseGrid();
        }

        private void DrawBaseGrid()
        {
            // Obtener dimensiones del contenedor
            double width = container.ActualWidth;
            double height = container.ActualHeight;

            // Dibujar el triángulo principal
            DrawTriangle(
                new Point(width / 2, height * 0.1),  // Top
                new Point(width * 0.1, height * 0.9),  // Bottom Left
                new Point(width * 0.9, height * 0.9)   // Bottom Right
            );

            // Agregar etiquetas
            AddLabel("Endomorfo", new Point(width * 0.1, height * 0.95));
            AddLabel("Mesomorfo", new Point(width * 0.5, height * 0.05));
            AddLabel("Ectomorfo", new Point(width * 0.8, height * 0.95));
        }

        private void DrawTriangle(Point p1, Point p2, Point p3)
        {
            var polygon = new Polygon
            {
                Points = new PointCollection { p1, p2, p3 },
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                Fill = new SolidColorBrush(Colors.Transparent)
            };

            canvas.Children.Add(polygon);
        }

        private void AddLabel(string text, Point position)
        {
            var textBlock = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(Colors.Black)
            };

            Canvas.SetLeft(textBlock, position.X);
            Canvas.SetTop(textBlock, position.Y);
            canvas.Children.Add(textBlock);
        }

        public void UpdateSomatotype(double endomorfia, double mesomorfia, double ectomorfia)
        {
            // Limpiar puntos anteriores
            var pointsToRemove = canvas.Children.OfType<Ellipse>().ToList();
            foreach (var punto in pointsToRemove)
            {
                canvas.Children.Remove(punto);
            }

            // Calcular posición en el triángulo
            double x = CalculateXPosition(endomorfia, ectomorfia);
            double y = CalculateYPosition(endomorfia, mesomorfia, ectomorfia);

            // Dibujar punto
            var point = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Red)
            };

            Canvas.SetLeft(point, x - 5);
            Canvas.SetTop(point, y - 5);
            canvas.Children.Add(point);
        }

        private double CalculateXPosition(double endomorfia, double ectomorfia)
        {
            double width = container.ActualWidth;
            return width * 0.5 + (ectomorfia - endomorfia) * width * 0.00833;
        }

        private double CalculateYPosition(double endomorfia, double mesomorfia, double ectomorfia)
        {
            double height = container.ActualHeight;
            return height * 0.5 - (2 * mesomorfia - endomorfia - ectomorfia) * height * 0.00833;
        }
    }
}