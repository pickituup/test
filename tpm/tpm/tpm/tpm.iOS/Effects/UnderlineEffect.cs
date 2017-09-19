using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using tpm.iOS.Effects;

[assembly: ResolutionGroupName("tpm.ToolKit.Effects")]
[assembly: ExportEffect(typeof(UnderlineEffect), nameof(UnderlineEffect))]
namespace tpm.iOS.Effects {
    public class UnderlineEffect : PlatformEffect {

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAttached() {
            SetUnderline(true);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDetached() {
            SetUnderline(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args) {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == Label.TextProperty.PropertyName || args.PropertyName == Label.FormattedTextProperty.PropertyName) {
                SetUnderline(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="underlined"></param>
        private void SetUnderline(bool underlined) {
            try {
                var label = (UILabel)Control;
                var text = (NSMutableAttributedString)label.AttributedText;
                var range = new NSRange(0, text.Length);

                if (underlined) {
                    text.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), range);
                }
                else {
                    text.RemoveAttribute(UIStringAttributeKey.UnderlineStyle, range);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Cannot underline Label. Error: ", ex.Message);
            }
        }
    }
}
