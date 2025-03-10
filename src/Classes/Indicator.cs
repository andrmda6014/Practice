using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;

namespace Task9.Classes
{
    public class Indicator : Control
    {
        private Ellipse _ellipse;

        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register("IsOn", typeof(bool), typeof(Indicator), new PropertyMetadata(false, OnIsOnChanged));

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        private static void OnIsOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var indicator = d as Indicator;

            if (indicator != null)
            {
                indicator.UpdateIndicator();
            }
        }

        public Indicator()
        {
            _ellipse = new Ellipse();
            this.AddVisualChild(_ellipse);
            UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            _ellipse.Fill = IsOn ? Brushes.Green : Brushes.Red;
            _ellipse.Stroke = Brushes.Black;
            _ellipse.Width = 50;
            _ellipse.Height = 50;
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            return _ellipse;
        }
    }
}
