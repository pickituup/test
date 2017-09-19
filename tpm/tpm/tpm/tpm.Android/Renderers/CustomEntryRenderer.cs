using tpm.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace tpm.Droid.Renderers {
    public class CustomEntryRenderer : EntryRendererBase {
    }
}