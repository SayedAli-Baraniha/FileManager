using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace FileManager
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string present = "0%";
        private string presentone = "Uploading 1 file";
        private string presentview;
        public string Present
        {
            get
            {
                return present;
            }
            set
            {
                present = value;
                NotifyPropertyChanged();
            }
        }
        public string PresentOne
        {
            get
            {
                return presentone;
            }
            set
            {
                presentone = value;
                NotifyPropertyChanged();
            }
        }
        public string PresentView
        {
            get
            {
                return presentview;
            }
            set
            {
                presentview = value;
                NotifyPropertyChanged();
            }
        }
        double width, height, heightLow;
        DisplayInfo mainDisplayInfo;

        public MainPage()
        {
            InitializeComponent();
            //Download("IMG_20221003_084749.jpg", "notebarana");
            mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            width = mainDisplayInfo.Width / mainDisplayInfo.Density;
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            this.BindingContext = this;
        }
        void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            // Process changes
            mainDisplayInfo = e.DisplayInfo;
            width = mainDisplayInfo.Width / mainDisplayInfo.Density;
        }
        public static byte[] GetBytesFromStream(Stream fileContentStream)
        {
            using (var memoryStreamHandler = new MemoryStream())
            {
                fileContentStream.CopyTo(memoryStreamHandler);
                return memoryStreamHandler.ToArray();
            }
        }
        public async Task<byte[]> Download(string serverUri, string bucketName)
        {
            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("2559ef37-8107-4b05-aa47-bddbf84fa4a4", "9ff57498db7d3f55d609eda8f40ec4c9296809283792bad682a5819bcfbcd081");
                var config = new AmazonS3Config { ServiceURL = "https://s3.ir-thr-at1.arvanstorage.com" };
                IAmazonS3 _s3Client = new AmazonS3Client(awsCredentials, config);
                string responseBody = string.Empty;
                try
                {
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = serverUri,
                    };
                    GetObjectResponse response = new GetObjectResponse();
                    response = await _s3Client.GetObjectAsync(request);

                    Stream responseStream = response.ResponseStream;
                    byte[] resize = GetBytesFromStream(responseStream);
                    return resize;

                }
                catch (AmazonS3Exception)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string lo = ex.Message;
                return null;
            }
        }
        bool isc = true;
        private async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    if (e.TotalX > 120)
                    {
                        MainVi.CornerRadius = 25;
                        FrameGFT.CornerRadius = 25;
                        new Animation { { 0, 1, new Animation(v => MainVi.Scale = v, MainVi.Scale, 0.8) }, { 0, 1, new Animation(v => MainVi.TranslationX = v, MainVi.TranslationX, 230) }, { 0, 1, new Animation(v => FrameGFT.Scale = v, FrameGFT.Scale, 0.93) }, { 0, 1, new Animation(v => FrameGFT.TranslationX = v, FrameGFT.TranslationX, 90) } }.Commit(this, "MenuAnimationsone", 16, 450, Easing.CubicInOut, null);
                        isc = false;
                    }
                    else if (e.TotalX < -120)
                    {
                        new Animation { { 0, 1, new Animation(v => MainVi.Scale = v, MainVi.Scale, 1) }, { 0, 1, new Animation(v => MainVi.TranslationX = v, MainVi.TranslationX, 0) }, { 0, 1, new Animation(v => FrameGFT.Scale = v, FrameGFT.Scale, 1) }, { 0, 1, new Animation(v => FrameGFT.TranslationX = v, FrameGFT.TranslationX, 0) } }.Commit(this, "MenuFalseAnimationsone", 16, 450, Easing.CubicInOut, null);
                        MainVi.CornerRadius = 0;
                        FrameGFT.CornerRadius = 0;
                        isc = true;

                    }
                    break;
            }
        }
        private async void BtnPls_Clicked(object sender, EventArgs e)
        {
            if (ViewFolder.IsVisible)
            {
                if (isc)
                {
                    isc = false;
                    MainVi.CornerRadius = 25;
                    FrameGFT.CornerRadius = 25;
                    new Animation { { 0, 1, new Animation(v => MainVi.Scale = v, MainVi.Scale, 0.8) }, { 0, 1, new Animation(v => MainVi.TranslationX = v, MainVi.TranslationX, 230) }, { 0, 1, new Animation(v => FrameGFT.Scale = v, FrameGFT.Scale, 0.93) }, { 0, 1, new Animation(v => FrameGFT.TranslationX = v, FrameGFT.TranslationX, 90) } }.Commit(this, "MenuAnimationsone", 16, 450, Easing.CubicInOut, null);
                }
                else
                {
                    isc = true;

                    new Animation { { 0, 1, new Animation(v => MainVi.Scale = v, MainVi.Scale, 1) }, { 0, 1, new Animation(v => MainVi.TranslationX = v, MainVi.TranslationX, 0) }, { 0, 1, new Animation(v => FrameGFT.Scale = v, FrameGFT.Scale, 1) }, { 0, 1, new Animation(v => FrameGFT.TranslationX = v, FrameGFT.TranslationX, 0) } }.Commit(this, "MenuFalseAnimationsone", 16, 450, Easing.CubicInOut, null);
                    await Task.Delay(450);
                    MainVi.CornerRadius = 0;
                    FrameGFT.CornerRadius = 0;
                }
            }
            else
            {
                MenuSuper.Source = "TrboMenu";
                await Scrll.ScrollToAsync(0, 0, false);
                ViewFolder.IsVisible = true;
                ViewFile.IsVisible = false;
                BtnPls.IsVisible = true;
                Scrll.IsEnabled = true;
            }

        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            // LabSText.Text += "در حال آپلود ...";
            // await  Hello(PathFile, NameFile, "notebarana");
            if (ViewAdd.IsVisible) ViewAdd.IsVisible = false;
            else ViewAdd.IsVisible = true;
        }

        private static IAmazonS3 _s3Client;
        private async Task UploadObjectFromFileAsync(IAmazonS3 client, string bucketName, string objectName, string filePath)
        {
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };

                putRequest.Metadata.Add("x-amz-meta-title", "someTitle");
                PutObjectResponse response = await client.PutObjectAsync(putRequest);
            }
            catch (AmazonS3Exception e)
            {

            }
        }
        public async Task<bool> Hello(string destinationFile, string fileName, string BUCKET_NAME)
        {
            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("2559ef37-8107-4b05-aa47-bddbf84fa4a4", "9ff57498db7d3f55d609eda8f40ec4c9296809283792bad682a5819bcfbcd081");
                var config = new AmazonS3Config { ServiceURL = "https://s3.ir-thr-at1.arvanstorage.com" };
                _s3Client = new AmazonS3Client(awsCredentials, config);

                await UploadObjectFromFileAsync(_s3Client, BUCKET_NAME, fileName, destinationFile);
                return true;
            }
            catch (Exception)
            {
                return (false);
            }
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {

        }

        string PathFile = null;
        string NameFile = null;
        async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    /*if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        ImageSelf.Source = ImageSource.FromStream(() => stream);
                    }*/
                    PathFile = result.FullPath;
                    NameFile = result.FileName;
                    Sqa.IsVisible = true;
                    await Sqa.TranslateTo(0, -(heightLow - 380), 250, Easing.CubicInOut);
                    PresentView = "Uploading ...";
                    TrackMPUAsync();
                }

                return result;
            }
            catch (Exception)
            {
                // The user canceled or something went wrong
            }

            return null;
        }

        private void MenuSuper_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnVertic_Clicked(object sender, EventArgs e)
        {
            BtnVerticMove.TranslateTo(0, 0, 250, Easing.CubicInOut);
        }

        private void BtnHoriz_Clicked(object sender, EventArgs e)
        {
            BtnVerticMove.TranslateTo(45, 0, 250, Easing.CubicInOut);
        }
        bool IsOne = true, frt = true;
        private async void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            // nam.TranslationY = e.ScrollY;
            // LabSText.Text = e.ScrollY.ToString();
            /*if (e.ScrollY == 0)
            {
                Gty.TranslateTo(0, 0, 250, Easing.CubicInOut);
                Sw.TranslateTo(0, -10, 250, Easing.CubicInOut);
            }
            else
            {
                Gty.TranslationY = (e.ScrollY * 0.2);
                Sw.TranslationY = (e.ScrollY * 0.2);
            }*/
            Gty.TranslationY = (e.ScrollY * 0.2);
            Sw.TranslationY = (e.ScrollY * 0.2 -10);
            //Hiew.TranslationY = (e.ScrollY * 0.5) - 35;



            if (IsOne && e.ScrollY > ScrollFrame)
            {
                new Animation { { 0, 0.5, new Animation(v => ImageSelf.TranslationY = v, 35, 20) }, { 0.5, 1, new Animation(v => ImageSelf.TranslationY = v, 20, 35) }, { 0, 0.5, new Animation(v => MenuSuper.TranslationY = v, 35, 20) }, { 0.5, 1, new Animation(v => MenuSuper.TranslationY = v, 20, 35) } }.Commit(this, "ChildAnimations", 16, 250, null);
                await Task.Delay(250);
                IsOne = false;
                frt = true;
            }
            else if (!IsOne && e.ScrollY < ScrollFrame) IsOne = true;
            else if (frt && e.ScrollY < 1)
            {                
                new Animation { { 0, 0.5, new Animation(v => Gty.TranslationY = v, 0, -15) }, { 0.5, 1, new Animation(v => Gty.TranslationY = v, -15, 0) } }.Commit(this, "ChildAnimationso", 16, 250, null);
                new Animation { { 0, 0.5, new Animation(v => Sw.TranslationY = v, -10, -25) }, { 0.5, 1, new Animation(v => Sw.TranslationY = v, -25, -10) } }.Commit(this, "ChildAnimationsf", 16, 250, null);
                await Task.Delay(250);
                IsOne = false;
            }
            else if (!frt && e.ScrollY > 1) frt = true;
        }
        bool k = true;
        double lowerHeight = 0;
        double ScrollFrame = 0;
        private void AbsoluteLayout_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var Kiks = (AbsoluteLayout)sender;
            if (Kiks.Height > 0 && k)
            {
                height = Kiks.Height;
                heightLow = height - 90;
                lowerHeight = heightLow - 410;
                Cor.HeightRequest = heightLow;
                Cor.VerticalOptions = LayoutOptions.StartAndExpand;
                double f = heightLow - 445;
                Jij.HeightRequest = f;
                Jij.VerticalOptions = LayoutOptions.EndAndExpand;
                ScrollFrame = heightLow - (height - 400) - 10;
                k = false;
            }
        }

        private void BtnS_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewAdd.IsVisible = false;
        }
        private string bucketName = "notebarana";
        // Specify your bucket region (an example region is shown).
        private static IAmazonS3 s3Client;

        private async void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            var options = new PickOptions
            {
                PickerTitle = "لطفا فایل مورد نظر را انتخاب کنید."
            };
            await PickAndShow(options);
        }
        TimeSpan StartTime;
        TimeSpan LastTime;
        TimeSpan dateTime;
        public async void TrackMPUAsync()
        {
            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("2559ef37-8107-4b05-aa47-bddbf84fa4a4", "9ff57498db7d3f55d609eda8f40ec4c9296809283792bad682a5819bcfbcd081");
                var config = new AmazonS3Config { ServiceURL = "https://s3.ir-thr-at1.arvanstorage.com" };
                s3Client = new AmazonS3Client(awsCredentials, config);
                var fileTransferUtility = new TransferUtility(s3Client);
                var uploadRequest = new TransferUtilityUploadRequest { BucketName = bucketName, FilePath = PathFile, Key = NameFile };
                uploadRequest.UploadProgressEvent += new EventHandler<UploadProgressArgs>(uploadRequest_UploadPartProgressEvent);
                if (!Prgress.Spin)
                {
                    Prgress.Spin = true;
                    Prgress.Easing = false;
                    Prgress.Progress = 1;
                }
                Present = "0%";
                StartTime = DateTime.Now.TimeOfDay;
                await fileTransferUtility.UploadAsync(uploadRequest);
                await Task.Delay(100);
                PresentView = "Upload completed";
                Prgress.Progress = 100;
                Present = $"100%";
                await Sqa.TranslateTo(0, 0, 450, Easing.CubicInOut);
                PresentView = "";
                Prgress.Progress = 0;
                Present = $"0%";
            }
            catch (AmazonS3Exception e)
            {
                PresentView = e.Message;
                await Task.Delay(650);
                await Sqa.TranslateTo(0, 0, 450, Easing.CubicInOut);
                PresentView = "";
                Prgress.Progress = 0;
                Present = $"0%";
            }
            catch (Exception e)
            {
                PresentView = e.Message;
                await Task.Delay(650);
                await Sqa.TranslateTo(0, 0, 450, Easing.CubicInOut);
                PresentView = "";
                Prgress.Progress = 0;
                Present = $"0%";
            }
        }
        double totalSec, total;
        string PhotoPath;

        private async void ImageButton_Clicked_3(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }
        private void ImageButton_Clicked_4(object sender, EventArgs e)
        {
            //  await Sqa.TranslateTo(0, 0, 250, Easing.CubicInOut);
        }

        int d = 0;

        private async void ImageButton_Clicked_5(object sender, EventArgs e)
        {
            if (FrmOne.HasShadow)
            {
                FrmOne.HasShadow = false;
                FrmTwo.HasShadow = false;
                new Animation { { 0, 1, new Animation(v => FrmOne.TranslationX = v, FrmTwo.TranslationX, 0) }, { 0, 1, new Animation(v => FrmTwo.TranslationX = v, FrmTwo.TranslationX, 0) } }.Commit(this, "ChildAnimations", 16, 150, null);
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
                new Animation { { 0, 1, new Animation(v => FrmOne.TranslationX = v, FrmTwo.TranslationX, -25) }, { 0, 1, new Animation(v => FrmTwo.TranslationX = v, FrmTwo.TranslationX, -25) } }.Commit(this, "ChildAnimations", 16, 150, null);
                double w = 0;
                for (int i = 0; i < 16; i++)
                {

                    FrmTwo.Margin = new Thickness(0, 0, w, 0);
                    w += 2;
                    await Task.Delay(1);
                }
            }
        }

        private async void ImageButton_Clicked_6(object sender, EventArgs e)
        {
            if (FrmOneS.HasShadow)
            {
                FrmOneS.HasShadow = false;
                FrmTwoS.HasShadow = false;
                new Animation { { 0, 1, new Animation(v => FrmOneS.TranslationX = v, FrmTwo.TranslationX, 0) }, { 0, 1, new Animation(v => FrmTwoS.TranslationX = v, FrmTwoS.TranslationX, 0) } }.Commit(this, "ChildAnimations", 16, 100, null);
                double w = 30;
                for (int i = 0; i < 16; i++)
                {

                    FrmTwoS.Margin = new Thickness(0, 0, w, 0);
                    w -= 2;
                    await Task.Delay(1);
                }
            }
            else
            {
                FrmOneS.HasShadow = true;
                FrmTwoS.HasShadow = true;
                new Animation { { 0, 1, new Animation(v => FrmOneS.TranslationX = v, FrmTwoS.TranslationX, -25) }, { 0, 1, new Animation(v => FrmTwoS.TranslationX = v, FrmTwoS.TranslationX, -25) } }.Commit(this, "ChildAnimations", 16, 100, null);
                double w = 0;
                for (int i = 0; i < 16; i++)
                {

                    FrmTwoS.Margin = new Thickness(0, 0, w, 0);
                    w += 2;
                    await Task.Delay(1);
                }
            }
        }

        private async void PanGestureRecognizer_PanUpdated_1(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    if (e.TotalX > 40)
                    {
                        if (FrmOne.HasShadow)
                        {
                            FrmOne.HasShadow = false;
                            FrmTwo.HasShadow = false;
                            new Animation { { 0, 1, new Animation(v => FrmOne.TranslationX = v, FrmTwo.TranslationX, 0) }, { 0, 1, new Animation(v => FrmTwo.TranslationX = v, FrmTwo.TranslationX, 0) } }.Commit(this, "ChildAnimations", 16, 150, null);
                            double w = 30;
                            for (int i = 0; i < 16; i++)
                            {

                                FrmTwo.Margin = new Thickness(0, 0, w, 0);
                                w -= 2;
                                await Task.Delay(1);
                            }
                        }
                    }
                    else if (e.TotalX < -40)
                    {
                        if (!FrmOne.HasShadow)
                        {
                            FrmOne.HasShadow = true;
                            FrmTwo.HasShadow = true;
                            new Animation { { 0, 1, new Animation(v => FrmOne.TranslationX = v, FrmTwo.TranslationX, -25) }, { 0, 1, new Animation(v => FrmTwo.TranslationX = v, FrmTwo.TranslationX, -25) } }.Commit(this, "ChildAnimations", 16, 150, null);
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

        private async void PanGestureRecognizer_PanUpdated_2(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    if (e.TotalX > 40)
                    {
                        if (FrmOneS.HasShadow)
                        {
                            FrmOneS.HasShadow = false;
                            FrmTwoS.HasShadow = false;
                            new Animation { { 0, 1, new Animation(v => FrmOneS.TranslationX = v, FrmTwo.TranslationX, 0) }, { 0, 1, new Animation(v => FrmTwoS.TranslationX = v, FrmTwoS.TranslationX, 0) } }.Commit(this, "ChildAnimations", 16, 100, null);
                            double w = 30;
                            for (int i = 0; i < 16; i++)
                            {

                                FrmTwoS.Margin = new Thickness(0, 0, w, 0);
                                w -= 2;
                                await Task.Delay(1);
                            }
                        }
                    }
                    else if (e.TotalX < -40)
                    {
                        if (!FrmOneS.HasShadow)
                        {
                            FrmOneS.HasShadow = true;
                            FrmTwoS.HasShadow = true;
                            new Animation { { 0, 1, new Animation(v => FrmOneS.TranslationX = v, FrmTwoS.TranslationX, -25) }, { 0, 1, new Animation(v => FrmTwoS.TranslationX = v, FrmTwoS.TranslationX, -25) } }.Commit(this, "ChildAnimations", 16, 100, null);
                            double w = 0;
                            for (int i = 0; i < 16; i++)
                            {

                                FrmTwoS.Margin = new Thickness(0, 0, w, 0);
                                w += 2;
                                await Task.Delay(1);
                            }
                        }
                    }
                    break;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Scrll.ScrollToAsync(0, ScrollFrame + 10,false);
            ViewFolder.IsVisible = false;
            ViewFile.IsVisible = true;
            BtnPls.IsVisible = false;
            Scrll.IsEnabled = false;
            MenuSuper.Source = "Arrow";
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Scrll.ScrollToAsync(0, ScrollFrame + 10, false);
            ViewFolder.IsVisible = false;
            ViewFile.IsVisible = true;
            BtnPls.IsVisible = false;
            Scrll.IsEnabled = false;
            MenuSuper.Source = "Arrow";
        }

        private void BtnVerticS_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnHorizS_Clicked(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Scrll.ScrollToAsync(0, ScrollFrame + 10, false);
            ViewFolder.IsVisible = false;
            ViewFile.IsVisible = true;
            BtnPls.IsVisible = false;
            Scrll.IsEnabled = false;
            MenuSuper.Source = "Arrow";
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Scrll.ScrollToAsync(0, ScrollFrame + 10, false);
            ViewFolder.IsVisible = false;
            ViewFile.IsVisible = true;
            BtnPls.IsVisible = false;
            Scrll.IsEnabled = false;
            MenuSuper.Source = "Arrow";
        }

        int Hors = 0, Min = 0;
        string def;
        double kq = 0;

        public void uploadRequest_UploadPartProgressEvent(object sender, UploadProgressArgs e)
        {
            //await Task.Delay(10);
            if (Prgress.Spin)
            {
                Prgress.Spin = false;
                Prgress.Easing = true;
            }
            LastTime = DateTime.Now.TimeOfDay;
            dateTime = LastTime.Subtract(StartTime);
            totalSec = dateTime.TotalMilliseconds;
            kq = e.PercentDone;
            double h = e.TotalBytes - e.TransferredBytes;
            total = h * totalSec / e.TransferredBytes;
            d = 0;
            d = Convert.ToInt32(total);
            Hors = 0; Min = 0;
            if (d > 3600000)
            {
                Hors = d / 3600000;
                int k = Hors * 3600000;
                d -= k;
            }
            if (d > 60000)
            {
                Min = d / 60000;
                int k = Min * 60000;
                d -= k;
            }
            int sec = d / 1000;
            if (Hors > 0)
            {
                if (Hors == 1)
                {
                    if (Min == 0) def = $"{Hors} hour left";
                    if (Min == 1) def = $"{Hors} hour and {Min} min left";
                    else def = $"{Hors} hour and {Min} mins left";

                }
                else
                {
                    if (Min == 0) def = $"{Hors} hours left";
                    if (Min == 1) def = $"{Hors} hours and {Min} min left";
                    else def = $"{Hors} hours and {Min} mins left";
                }
            }
            else if (Min > 0)
            {
                if (Min == 1)
                {
                    if (d == 0) def = $"{Min} min left";
                    if (d == 1) def = $"{Min} min and {d / 1000} secend left";
                    else def = $"{Min} min and {d / 1000} secends left";

                }
                else
                {
                    if (sec == 0) def = $"{Min} mins left";
                    if (sec == 1) def = $"{Min} mins and {sec} secend left";
                    else def = $"{Min} mins and {sec} secends left";
                }
            }
            else if (sec > 0)
            {
                if (sec == 1) def = $"{sec} secend left";
                else def = $"{sec} secends left";
            }
            else
            {
                def = "few secends Left";
            }
            Prgress.Progress = e.PercentDone;
            Present = e.PercentDone.ToString() + "%";

            //KPrgress.Text = $"{e.PercentDone}%";
            //StartTime = DateTime.Now.TimeOfDay;
            //Ui.Text = def;
            PresentView = def;
            Present = kq.ToString() + "%";
        }
    }
}
