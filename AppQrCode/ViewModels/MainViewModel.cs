using AppQrCode.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AppQrCode.ViewModels
{
    public class MainViewModel : ObservableObject, IDisposable
    {
        private bool isYes;
        private bool isNo;

        private int selectedVersion;
        private int picPoint;
        private int iconSize;
        private int iconMagin;
        private string inputLink;
        private ImageSource qrImage;

        public MainViewModel()
        {
            InitialData();

            this.SelectedVersion = 1;
            this.PicPoint = 5;
            this.IconSize = 7;
            this.IconMagin = 3;
        }

        public bool IsYes
        {
            get => isYes;
            set => SetProperty(ref isYes, value);
        }

        public bool IsNo
        {
            get => isNo;
            set => SetProperty(ref isNo, value);
        }

        public ObservableCollection<int> ListVersions { get; private set; } = new ObservableCollection<int>();
        public ObservableCollection<int> ListPicPoints { get; } = new ObservableCollection<int>();
        public ObservableCollection<int> ListIconSizes { get; } = new ObservableCollection<int>();
        public ObservableCollection<int> ListIconMagins { get; } = new ObservableCollection<int>();


        public int SelectedVersion
        {
            get => selectedVersion;
            set => SetProperty(ref selectedVersion, value);
        }

        public int PicPoint
        {
            get => picPoint;
            set => SetProperty(ref picPoint, value);
        }

        public int IconSize
        {
            get => iconSize;
            set => SetProperty(ref iconSize, value);
        }

        public int IconMagin
        {
            get => iconMagin;
            set => SetProperty(ref iconMagin, value);
        }

        public string InputLink
        {
            get => inputLink;
            set => SetProperty(ref inputLink, value);
        }

        public void Dispose()
        {
        }

        void InitialData()
        {
            for (int i = 0; i < 40; i++)
            {
                ListVersions.Add(i + 1);
                ListIconMagins.Add(i + 1);
                ListIconSizes.Add(i + 1);
                ListPicPoints.Add(i + 1);
            }
        }

        public ImageSource QrImage
        {
            get => qrImage;
            set => SetProperty(ref qrImage, value);
        }

        public RelayCommand CmdProduct => new RelayCommand(() =>
          {
              int version = SelectedVersion;
              int pixel = PicPoint;
              int iconSize = IconSize;
              int iconMagin = IconMagin;
              string strMsg = InputLink;
              bool isLine = IsYes ? true : false;

              Bitmap bmp = QrEncoder.Encode(strMsg, version, pixel, "D:\\python.ico", iconSize, iconMagin, isLine);

              using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
              {
                  bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                  ms.Seek(0, System.IO.SeekOrigin.Begin);
                  var bmi = new BitmapImage();
                  bmi.BeginInit();
                  bmi.CacheOption = BitmapCacheOption.OnLoad;
                  bmi.StreamSource = ms;
                  bmi.EndInit();

                  this.QrImage = bmi;
              }
          });

        public RelayCommand CmdSave => new RelayCommand(() =>
          {
              if (this.QrImage==null)
              {
                  return;
              }

              JpegBitmapEncoder encoder = new JpegBitmapEncoder();
              string photoPath = "D:\\test.jpg";
              encoder.Frames.Add(BitmapFrame.Create((BitmapImage)this.QrImage));

              using (var fileStream = new FileStream(photoPath, FileMode.Create))
              {
                  encoder.Save(fileStream);

                  WeakReferenceMessenger.Default.Send<string>("Save file successfully!");
              }
          });
    }
}
