using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.Views.Controls {
    public sealed class ExtendedContentView : ContentView {
        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(
                "BorderThickness",
                typeof(int),
                typeof(ExtendedContentView),
                defaultValue: default(int));

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(
                "BorderColor",
                typeof(Color),
                typeof(ExtendedContentView),
                defaultValue: default(Color));

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(
                "CornerRadius",
                typeof(int),
                typeof(ExtendedContentView),
                defaultValue: default(int));

        /// <summary>
        /// 
        /// </summary>
        public int BorderThickness {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public Color BorderColor {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CornerRadius {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}
