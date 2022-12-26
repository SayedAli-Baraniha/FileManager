
using FileManager.ViewControler;
using System.ComponentModel;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(FileManager.UWP.CustomFrameRenderer))]
namespace FileManager.UWP
{
    public class CustomFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null)
            {
                UpdateCornerRadius();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(CustomFrame.CornerRadius) ||
                e.PropertyName == nameof(CustomFrame))
            {
                UpdateCornerRadius();
            }
        }

        private void UpdateCornerRadius()
        {
            var radius = ((CustomFrame)this.Element).CornerRadius;
            Control.CornerRadius = new Windows.UI.Xaml.CornerRadius(radius.TopLeft, radius.TopRight, radius.BottomRight, radius.BottomLeft);
        }
    }
}