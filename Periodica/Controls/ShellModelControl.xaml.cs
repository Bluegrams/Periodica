using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.Text;
using Windows.UI;
using Bluegrams.Periodica.Data;
using Bluegrams.Periodica.Helpers;

namespace Bluegrams.Periodica.Controls
{
    public sealed partial class ShellModelControl : UserControl
    {
        private const double Pi2 = 2 * Math.PI;

        public Element Element { get; set; }

        public ShellModelControl()
        {
            this.InitializeComponent();
        }

        private void canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var ds = args.DrawingSession;
            var color = AppTheme.Instance.IsDark ? Colors.White : Colors.Black;
            var altColor = AppTheme.Instance.IsDark ? Colors.Black : Colors.White;
            float cX = (float)sender.ActualWidth / 2, cY = (float)sender.ActualHeight / 2;
            // Draw nucleus
            ds.FillCircle(cX, cY, 25, color);
            var format = new CanvasTextFormat();
            format.HorizontalAlignment = CanvasHorizontalAlignment.Center;
            format.VerticalAlignment = CanvasVerticalAlignment.Center;
            ds.DrawText(Element.Symbol, cX, cY, altColor, format);
            // Draw shells
            for (int i = 0; i < Element.ShellConfiguration.Length; i++)
            {
                int electrons = Element.ShellConfiguration[i];
                int r = 25 + (i + 1) * 21;
                double rad = Pi2 / electrons;
                ds.DrawCircle(cX, cY, r, color);
                for (int e = 0; e < electrons; e++)
                {
                    float x = cX + (float)Math.Sin(e * rad) * r;
                    float y = cY - (float)Math.Cos(e * rad) * r;
                    ds.FillCircle(x, y, 4, color);
                }
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.canvas.RemoveFromVisualTree();
            this.canvas = null;
        }
    }
}
