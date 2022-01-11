using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FaqPanel.Views.Controls
{
    public class Separator : BoxView
    {
        public Separator()
        {
            BackgroundColor = Color.FromHex("#e5e5e5");
            HeightRequest = 1;
            VerticalOptions = LayoutOptions.End;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
