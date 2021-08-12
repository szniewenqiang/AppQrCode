using System.Windows;
using System.Windows.Controls;
using AppQrCode.ViewModels;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AppQrCode.Views
{
    public partial class MainPage : Page
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            Loaded += (s, e) =>
            {
                WeakReferenceMessenger.Default.Register<string>(this, (obj, str) =>
                {
                    MessageBox.Show(str);
                });
            };

            Unloaded += (s, e) =>
            {
                WeakReferenceMessenger.Default.Unregister<string>(this);
            };
        }
    }
}
