using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace WpControls.PositionControl
{
    public sealed partial class PositionControl : UserControl
    {
        public PositionControl()
        {
            this.InitializeComponent();
        }

     

        private void Ellipse_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement elem = sender as FrameworkElement;
            if (elem == null)
                return;

            double left = Canvas.GetLeft(elem);
            double top = Canvas.GetTop(elem);

            left += e.Delta.Translation.X;
            top += e.Delta.Translation.Y;

            //check for bounds
            if (left < 0)
            {
                left = 0;
            }
            else if (left > (LayoutRoot.ActualWidth - elem.ActualWidth))
            {
                left = LayoutRoot.ActualWidth - elem.ActualWidth;
            }

            if (top < 0)
            {
                top = 0;
            }
            else if (top > (LayoutRoot.ActualHeight - elem.ActualHeight))
            {
                top = LayoutRoot.ActualHeight - elem.ActualHeight;
            }

            Canvas.SetLeft(elem, left);
            Canvas.SetTop(elem, top);

        }
    }
}
