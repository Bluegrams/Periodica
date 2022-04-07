using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Windows.UI;
using Windows.ApplicationModel.Resources;
using Bluegrams.Periodica.ViewModels;

namespace Bluegrams.Periodica.Helpers
{
    class RenderPeriodicHelper : INotifyPropertyChanged
    {
        // Instance
        private static readonly RenderPeriodicHelper instance;
        private static ResourceLoader loader;

        static RenderPeriodicHelper()
        {
            instance = new RenderPeriodicHelper();
            loader = ResourceLoader.GetForViewIndependentUse("CodeStrings");
        }

        public static RenderPeriodicHelper Instance { get { return instance; } }

        // Members

        public event PropertyChangedEventHandler PropertyChanged;

        private bool renderMode;
        public bool RenderMode
        {
            get { return renderMode; }
            set
            {
                renderMode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RenderMode)));
            }
        }

        private RenderPeriodicHelper() { }

        public static async Task SavePeriodicTableView(RenderTargetBitmap renderTargetBmp)
        {
            var savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add("PNG File", new[] { ".png" });
            savePicker.FileTypeChoices.Add("BMP File", new[] { ".bmp" });
            savePicker.SuggestedFileName = "Periodica";
            StorageFile sFile = await savePicker.PickSaveFileAsync();
            if (sFile != null)
            {
                using (var fileStream = await sFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    Guid bmpEncodeGuid;
                    switch (sFile.FileType)
                    {
                        case ".bmp":
                            bmpEncodeGuid = BitmapEncoder.BmpEncoderId;
                            break;
                        default:
                            bmpEncodeGuid = BitmapEncoder.PngEncoderId;
                            break;
                    }
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(bmpEncodeGuid, fileStream);
                    var buffer = await renderTargetBmp.GetPixelsAsync();
                    byte[] pixels = renderExportBitmap(renderTargetBmp.PixelWidth, renderTargetBmp.PixelHeight,
                        buffer.ToArray());
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                              (uint)renderTargetBmp.PixelWidth,
                              (uint)renderTargetBmp.PixelHeight,
                              96.0,
                              96.0,
                              pixels.ToArray());
                    await encoder.FlushAsync();
                }
            }
        }

        private static byte[] renderExportBitmap(int width, int height, byte[] input)
        {
            Color textcolor = AppTheme.Instance.IsDark ? Colors.White : Colors.Black;
            CanvasDevice device = CanvasDevice.GetSharedDevice();
            CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, width, height, 96);
            renderTarget.SetPixelBytes(input);
            using (CanvasDrawingSession ds = renderTarget.CreateDrawingSession())
            {
                System.Diagnostics.Debug.WriteLine(textcolor);
                CanvasTextFormat format = new CanvasTextFormat();
                format.FontFamily = "Segoe UI";
                format.FontStretch = Windows.UI.Text.FontStretch.Expanded;
                format.FontSize = 18;
                ds.DrawText("bluegrams.com/periodica", 20, 1160, textcolor, format);
                int sX = 575, sY = 150, count = 0;
                foreach (ColoringOption opt in ColoringViewModel.StaticExplanationItems)
                {
                    int cX = sX + count / 7 * 250, cY = sY + count % 7 * 33;
                    ds.FillRectangle(cX, cY, 22, 22, opt.Brush.Color);
                    ds.DrawText(opt.Name, cX + 30, cY, textcolor, format);
                    count++;
                }
                format.FontSize = 50;
                format.FontWeight = Windows.UI.Text.FontWeights.SemiBold;
                format.HorizontalAlignment = CanvasHorizontalAlignment.Center;
                ds.DrawText(loader.GetString("Title_PeriodicTable"), 750, 35, textcolor, format);
            }
            return renderTarget.GetPixelBytes();
        }
    }
}
