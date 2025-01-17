using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace PrototipoVersion1._0.Visual.Deporte
{
    public class SomatocartaCreada
    {
        private Canvas canvas;
        private Grid container;
        private Dictionary<string, TextBox> plieguesTextBoxes;

        public SomatocartaCreada(Grid somatocartaContainer)
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

            // Centro del triángulo
            Point center = new Point(width / 2, height / 2);

            // Radio del triángulo
            double radius = Math.Min(width, height) * 0.4;

            // Dibujar el triángulo principal y sus divisiones
            DrawMainTriangle(center, radius);

            // Dibujar líneas de división internas
            DrawInternalDivisions(center, radius);

            // Agregar etiquetas
            AddLabel("Endomorfo", new Point(center.X - radius - 50, center.Y + radius - 20));
            AddLabel("Mesomorfo", new Point(center.X - 20, center.Y - radius - 20));
            AddLabel("Ectomorfo", new Point(center.X + radius - 20, center.Y + radius - 20));
        }

        private void DrawMainTriangle(Point center, double radius)
        {
            // Cálculo de puntos del triángulo principal
            Point p1 = new Point(center.X, center.Y - radius); // Arriba
            Point p2 = new Point(center.X - radius * Math.Cos(Math.PI / 6), center.Y + radius / 2); // Izquierda
            Point p3 = new Point(center.X + radius * Math.Cos(Math.PI / 6), center.Y + radius / 2); // Derecha

            // Dibujar el triángulo
            DrawTriangle(p1, p2, p3);
        }

        private void DrawInternalDivisions(Point center, double radius)
        {
            // Dibujar líneas desde el centro hacia los vértices
            Point p1 = new Point(center.X, center.Y - radius); // Arriba
            Point p2 = new Point(center.X - radius * Math.Cos(Math.PI / 6), center.Y + radius / 2); // Izquierda
            Point p3 = new Point(center.X + radius * Math.Cos(Math.PI / 6), center.Y + radius / 2); // Derecha

            DrawLine(center, p1);
            DrawLine(center, p2);
            DrawLine(center, p3);

            // Dibujar líneas horizontales de niveles
            for (int i = 1; i <= 4; i++) // Dividimos en 4 niveles internos
            {
                double factor = i / 4.0;
                Point np1 = new Point(
                    center.X,
                    center.Y - radius * factor
                );
                Point np2 = new Point(
                    center.X - radius * Math.Cos(Math.PI / 6) * factor,
                    center.Y + radius / 2 * factor
                );
                Point np3 = new Point(
                    center.X + radius * Math.Cos(Math.PI / 6) * factor,
                    center.Y + radius / 2 * factor
                );

                DrawTriangle(np1, np2, np3, Colors.Gray, 0.5); // Líneas grises y más delgadas
            }
        }

        private void DrawTriangle(Point p1, Point p2, Point p3, Color? strokeColor = null, double strokeWidth = 1)
        {
            var polygon = new Polygon
            {
                Points = new PointCollection { p1, p2, p3 },
                Stroke = new SolidColorBrush(strokeColor ?? Colors.Black),
                StrokeThickness = strokeWidth,
                Fill = new SolidColorBrush(Colors.Transparent)
            };

            canvas.Children.Add(polygon);
        }

        private void DrawLine(Point start, Point end)
        {
            var line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            };

            canvas.Children.Add(line);
        }

        private void DrawHeaderLines(Point center, double radius)
        {
            // Ángulos en radianes para 120 grados entre líneas
            double[] angles = { -Math.PI / 2, Math.PI / 6, 5 * Math.PI / 6 };

            foreach (double angle in angles)
            {
                // Calcular el punto final de la línea
                double x = center.X + radius * Math.Cos(angle);
                double y = center.Y + radius * Math.Sin(angle);

                // Dibujar la línea
                var line = new Line
                {
                    X1 = center.X,
                    Y1 = center.Y,
                    X2 = x,
                    Y2 = y,
                    Stroke = new SolidColorBrush(Colors.Gray),
                    StrokeThickness = 2
                };

                canvas.Children.Add(line);
            }
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