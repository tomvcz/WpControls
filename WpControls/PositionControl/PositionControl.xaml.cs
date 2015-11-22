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
        #region Properties

        /// <summary>
        /// Minimální hodnota
        /// </summary>
        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        /// <summary>
        /// Maximální hodnota
        /// </summary>
        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        /// <summary>
        /// Aktualní hodtona osy X
        /// </summary>
        public int ValueX
        {
            get { return (int)GetValue(ValueXProperty); }
            set { SetValue(ValueXProperty, value); }
        }

        /// <summary>
        /// Aktuální hodnota osy Y
        /// </summary>
        public int ValueY
        {
            get { return (int)GetValue(ValueYProperty); }
            set { SetValue(ValueYProperty, value); }
        }

        #endregion

        #region Dependency properties

        private DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof(int), typeof(PositionControl), new PropertyMetadata(0));

        private DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(int), typeof(PositionControl), new PropertyMetadata(200));

        private DependencyProperty ValueXProperty = DependencyProperty.Register("ValueX", typeof(int), typeof(PositionControl), new PropertyMetadata(0));

        private DependencyProperty ValueYProperty = DependencyProperty.Register("ValueY", typeof(int), typeof(PositionControl), new PropertyMetadata(0));


        #endregion

        private double maxLeft;
        private double minLeft;
        private double minTop;
        private double maxTop;

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
            SetValues(Canvas.GetTop(ManipulatePoint), Canvas.GetLeft(ManipulatePoint));
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

            maxLeft = CenterEllipse.ActualWidth - ManipulatePoint.ActualWidth;
            minLeft = 0;

            maxTop = Canvas.GetTop(CenterEllipse) + CenterEllipse.ActualHeight - ManipulatePoint.ActualHeight;
            minTop = Canvas.GetTop(CenterEllipse);


        }

        private void SetValues(double top, double left)
        {
            var x = (Max / (maxLeft - minLeft)) * (left - minLeft);

            var y = (Max / (maxTop - minTop)) * (top - minTop);

            ValueX = (int)x;
            ValueY = (int)y;

            txtValueX.Text = string.Format("X: {0}", ValueX);
            txtValueY.Text = string.Format("Y: {0}", ValueY);
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
            if (left < minLeft)
            {
                left = minLeft;
            }
            else if (left > maxLeft)
            {
                left = maxLeft;
            }

            if (top < minTop)
            {
                top = minTop;
            }
            else if (top > maxTop)
            {
                top = maxTop;
            }

            Canvas.SetLeft(elem, left);
            Canvas.SetTop(elem, top);


            SetValues(top, left);
        }

        private void ManipulatePoint_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }
    }
}
