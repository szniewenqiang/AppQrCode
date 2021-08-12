using System.Windows.Controls;

using AppQrCode.Contracts.Views;
using AppQrCode.ViewModels;

using MahApps.Metro.Controls;

namespace AppQrCode.Views
{
    public partial class ShellWindow : MetroWindow, IShellWindow
    {
        public ShellWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();
    }
}
