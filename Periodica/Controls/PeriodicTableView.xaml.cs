using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Bluegrams.Periodica.Data;
using Bluegrams.Periodica.ViewModels;
using System.ComponentModel;
using Bluegrams.Periodica.Helpers;
using Windows.UI.Xaml.Media.Imaging;

namespace Bluegrams.Periodica.Controls
{
    public sealed partial class PeriodicTableView : PeriodicTableControl
    {
        public PeriodicTableView()
        {
            this.InitializeComponent();
        }

        public float ZoomFactor
        {
            get => scrollViewer.ZoomFactor;
            set => scrollViewer.ChangeView(null, null, value);
        }

        protected override void OnPeriodicTableChanged(PeriodicTableViewModel periodicTable)
        {
            Grid grid = this.gridMain;
            if (grid.Children.Count > 30) return;
            foreach (ElementViewModel elem in periodicTable)
            {
                ElementBox box = new ElementBox();
                box.IsTabStop = true;
                box.TabIndex = elem.Element.AtomicNumber - 1;
                box.Element = elem;
                box.SetDisplayedProperty(Settings.Instance.DisplayedProperty);
                box.Click += this.ElementButton_Click;
                elem.PropertyChanged += delegate(object o, PropertyChangedEventArgs e)
                {
                    box.SetEnabled(elem.Visible);
                };
                Settings.Instance.PropertyChanged += delegate (object o, PropertyChangedEventArgs e)
                {
                    box.SetDisplayedProperty(Settings.Instance.DisplayedProperty);
                };
                Grid.SetColumn(box, TablePositionConverter.Convert((Element)elem, "Group"));
                Grid.SetRow(box, TablePositionConverter.Convert((Element)elem, "Period"));
                grid.Children.Add(box);
            }
        }

        private void ElementButton_Click(object sender, RoutedEventArgs e)
        {
            ElementViewModel elem = ((ElementBox)sender).Element;
            OnElementClicked(new ElementEventArgs(elem));
        }

        public async void ExportPeriodicTableView()
        {
            double oWidth = this.Width, oHeight = this.Height;
            this.Width = 2000;
            this.Height = 1200;
            RenderPeriodicHelper.Instance.RenderMode = true;
            RenderTargetBitmap renderTargetBmp = new RenderTargetBitmap();
            await renderTargetBmp.RenderAsync(viewTable);
            await RenderPeriodicHelper.SavePeriodicTableView(renderTargetBmp);
            RenderPeriodicHelper.Instance.RenderMode = false;
            this.Width = oWidth;
            this.Height = oHeight;
        }

        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            viewTable.Height = ((ScrollViewer)sender).ViewportHeight;
        }
    }
}
