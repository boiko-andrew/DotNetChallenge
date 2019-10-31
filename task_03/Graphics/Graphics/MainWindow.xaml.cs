using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            const int width = 400;
            const int height = 400;
            const int breaks = 10;

            double minDistance;
            int minDistanceX;
            int minDistanceY;

            double currentDistance;

            InitializeComponent();

            // Границы области построения
            DrawLine(0, 0, width, 0);
            DrawLine(width, 0, width, height);
            DrawLine(width, height, 0, height);
            DrawLine(0, height, 0, 0);

            // Нижняя часть узора
            for (int i = 0; i < breaks; i++)
            {
                DrawLine(width, i * (height / breaks),
                    width - (i + 1) * (width / breaks), height);
            }

            // Верхняя часть узора
            for (int i = 0; i < breaks; i++)
            {
                DrawLine(width - i * (width / breaks), 0,
                    0, (i + 1) * (height / breaks));
            }

            // Побочная диагональ
            DrawLine(width, 0, 0, height);

            // Центральная часть узора (ниже побочной диагонали)
            for (int i = 0; i < breaks - 1; i++)
            {
                minDistance = GetDistance(width, 0, 0, height);
                minDistanceX = 0;
                minDistanceY = height;

                for (int j = 0; j < breaks - 1; j++)
                {
                    GetXY((i + 1) * width / breaks, height, width, 0,
                        j * width / breaks, height,
                        width, height - (j + 1) * height / breaks,
                        out int x, out int y);
                    currentDistance = GetDistance(width, 0, x, y);

                    if ((currentDistance < minDistance)
                        && (0 < x) && (x < width)
                        && (0 < y) && (y < height))
                    {
                        minDistance = currentDistance;
                        minDistanceX = x;
                        minDistanceY = y;
                    }
                }
                DrawLine(width, 0, minDistanceX, minDistanceY);
            }

            // Центральная часть узора (выше побочной диагонали)
            for (int i = 0; i < breaks - 1; i++)
            {
                minDistance = GetDistance(width, 0, 0, height);
                minDistanceX = 0;
                minDistanceY = height;

                for (int j = 0; j < breaks - 1; j++)
                {
                    GetXY(0, height - height / breaks * (i + 1), width, 0,
                        0, height - height / breaks * j,
                        width / breaks * (j + 1), 0,
                        out int x, out int y);
                    currentDistance = GetDistance(width, 0, x, y);

                    if ((currentDistance < minDistance)
                        && (0 < x) && (x < width)
                        && (0 < y) && (y < height))
                    {
                        minDistance = currentDistance;
                        minDistanceX = x;
                        minDistanceY = y;
                    }
                }
                DrawLine(width, 0, minDistanceX, minDistanceY);
            }
        }

        private void DrawLine(int x1, int y1, int x2, int y2)
        {
            var line = new Line();
            line.Stroke = Brushes.CadetBlue;

            line.X1 = x1;
            line.Y1 = y1;

            line.X2 = x2;
            line.Y2 = y2;

            canvas.Children.Add(line);
        }

        private static void GetXY(double x11, double y11,
            double x12, double y12,
            double x21, double y21,
            double x22, double y22,
            out int x, out int y)
        {
            double k1;
            double b1;
            double k2;
            double b2;

            if ((x11 != x12) && (x21 != x22))
            {
                k1 = (y12 - y11) / (x12 - x11);
                b1 = y11 - k1 * x11;

                k2 = (y22 - y21) / (x22 - x21);
                b2 = y21 - k2 * x21;

                if (k1 != k2)
                {
                    x = (int)((b2 - b1) / (k1 - k2));
                    y = (int)(k1 * x + b1);
                }
                else
                {
                    x = 0;
                    y = 0;
                }
            }
            else if ((x11 == x12) && (x21 != x22))
            {
                k2 = (y22 - y21) / (x22 - x21);
                b2 = y21 - k2 * x21;

                x = (int)x11;
                y = (int)(k2 * x + b2);
            }
            else if ((x11 != x12) && (x21 == x22))
            {
                k1 = (y12 - y11) / (x12 - x11);
                b1 = y11 - k1 * x11;

                x = (int)x12;
                y = (int)(k1 * x + b1);
            }
            else
            {
                x = 0;
                y = 0;
            }
        }

        private static double GetDistance(double x1, double y1,
            double x2, double y2)
        {
            double d;
            d = Math.Sqrt(Math.Pow((x1 - x2), 2) +
                Math.Pow((y1 - y2), 2));
            return d;
        }
    }
}