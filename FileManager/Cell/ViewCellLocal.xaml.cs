using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace FileManager.Cell
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewCellLocal : LocalCell
    {
        protected override void InitializeCell()
        {
           
        }

        protected override void SetupCell(bool isRecycled)
        {

        }

        private async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    if (e.TotalX > 20)
                    {
                        if (FrmOne.HasShadow)
                        {
                            FrmOne.HasShadow = false;
                            FrmTwo.HasShadow = false;
                            new Animation { { 0, 1, new Animation(v => FrmOne.TranslationX = v, FrmTwo.TranslationX, 0) }, { 0, 1, new Animation(v => FrmTwo.TranslationX = v, FrmTwo.TranslationX, 0) } }.Commit((IAnimatable)this, "ChildAnimations", 16, 150, null);
                            double w = 30;
                            for (int i = 0; i < 16; i++)
                            {

                                FrmTwo.Margin = new Thickness(0, 0, w, 0);
                                w -= 2;
                                await Task.Delay(1);
                            }
                        }
                    }
                    else if (e.TotalX < -20)
                    {
                        if (!FrmOne.HasShadow)
                        {
                            FrmOne.HasShadow = true;
                            FrmTwo.HasShadow = true;
                            new Animation { { 0, 1, new Animation(v => FrmOne.TranslationX = v, FrmTwo.TranslationX, -25) }, { 0, 1, new Animation(v => FrmTwo.TranslationX = v, FrmTwo.TranslationX, -25) } }.Commit((IAnimatable)this, "ChildAnimations", 16, 150, null);
                            double w = 0;
                            for (int i = 0; i < 16; i++)
                            {

                                FrmTwo.Margin = new Thickness(0, 0, w, 0);
                                w += 2;
                                await Task.Delay(1);
                            }
                        }
                    }
                    break;
            }
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (FrmOne.HasShadow)
            {
                FrmOne.HasShadow = false;
                FrmTwo.HasShadow = false;
                new Animation { { 0, 1, new Animation(v => FrmOne.TranslationX = v, FrmTwo.TranslationX, 0) }, { 0, 1, new Animation(v => FrmTwo.TranslationX = v, FrmTwo.TranslationX, 0) } }.Commit((IAnimatable)this, "ChildAnimations", 16, 150, null);
                double w = 30;
                for (int i = 0; i < 16; i++)
                {

                    FrmTwo.Margin = new Thickness(0, 0, w, 0);
                    w -= 2;
                    await Task.Delay(1);
                }
            }
            else
            {
                FrmOne.HasShadow = true;
                FrmTwo.HasShadow = true;
                new Animation { { 0, 1, new Animation(v => FrmOne.TranslationX = v, FrmTwo.TranslationX, -25) }, { 0, 1, new Animation(v => FrmTwo.TranslationX = v, FrmTwo.TranslationX, -25) } }.Commit((IAnimatable)this, "ChildAnimations", 16, 150, null);
                double w = 0;
                for (int i = 0; i < 16; i++)
                {

                    FrmTwo.Margin = new Thickness(0, 0, w, 0);
                    w += 2;
                    await Task.Delay(1);
                }
            }
        }
    }
}