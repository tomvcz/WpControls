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

            Loaded += PositionControl_Loaded;

            ManipulatePoint.ManipulationMode = ManipulationModes.All;


            ManipulatePoint.ManipulationDelta += ManipulatePoint_ManipulationDelta;
            ManipulatePoint.ManipulationCompleted += ManipulatePoint_ManipulationCompleted;

        }

       

        private void PositionControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetLayout();
        }

        private void SetLayout()
        {
            var heigth = MainGrid.ActualHeight;
            var width = MainGrid.ActualWidth;

            LayoutRoot.Height = heigth;
            LayoutRoot.Width = width;

            CenterEllipse.Width = width;
            CenterEllipse.Height = width;

            

            Canvas.SetLeft(ManipulatePoint, width / 2 - ManipulatePoint.Width / 2);
            Canvas.SetTop(ManipulatePoint, heigth / 2 - ManipulatePoint.Height / 2);


            Canvas.SetLeft(CenterEllipse, width / 2 - CenterEllipse.Width / 2);
            Canvas.SetTop(CenterEllipse, heigth / 2 - CenterEllipse.Height / 2);


        }

        private void ManipulatePoint_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
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
            else if (left > (CenterEllipse.ActualWidth - elem.ActualWidth))
            {
                left = CenterEllipse.ActualWidth - elem.ActualWidth;
            }

            if (top < Canvas.GetTop(CenterEllipse))
            {
                top = Canvas.GetTop(CenterEllipse);
            }
            else if (top > (Canvas.GetTop(CenterEllipse) + CenterEllipse.ActualHeight - elem.ActualHeight))
            {
                top = Canvas.GetTop(CenterEllipse) + CenterEllipse.ActualHeight - elem.ActualHeight;
            }

            Canvas.SetLeft(elem, left);
            Canvas.SetTop(elem, top);

        }

        private void ManipulatePoint_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            
        }
    }
}
