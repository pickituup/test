using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using tpm.Droid.Renderers;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace tpm.Droid.Renderers {
    public class CustomEditorRenderer: EditorRendererBase {
    }
}