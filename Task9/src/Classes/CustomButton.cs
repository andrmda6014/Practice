using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Task9.Classes
{
    public class CustomButton : Button
    {
        public CustomButton()
        {
            this.Style = (Style)Application.Current.Resources["CustomButtonStyle"];
        }

        protected override void OnClick()
        {
            base.OnClick();
            AnimateButton();
        }

        private void AnimateButton()
        {
            var scaleTransform = new ScaleTransform(1, 1);
            this.RenderTransform = scaleTransform;

            var animation = new DoubleAnimation(0.9, 1, TimeSpan.FromMilliseconds(100));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
    }
}
