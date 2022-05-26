using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ComponentsTree
{
	/// <summary>
	/// Логика взаимодействия для WindowDownloadLibrary.xaml
	/// </summary>
	public partial class WindowDownloadLibrary : Window
	{
		private DispatcherTimer timer;

		public WindowDownloadLibrary()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(0.5);
			timer.Tick += Timer_Tick;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			textBlockCountCatalogs.Text = LibraryLCSC.LCSCBaseData.TotalCountCatalogs.ToString();
			textBlockCountPages.Text = LibraryLCSC.LCSCBaseData.TotalCountPages.ToString();
			textBlockReadCatalogs.Text = LibraryLCSC.LCSCBaseData.TotalReadCatalogs.ToString();
			textBlockReadPages.Text = LibraryLCSC.LCSCBaseData.TotalReadPages.ToString();
			if (LibraryLCSC.LCSCBaseData.TotalCountCatalogs != 0)
				progressBarCatalogs.Value = (double)LibraryLCSC.LCSCBaseData.TotalReadCatalogs / (double)LibraryLCSC.LCSCBaseData.TotalCountCatalogs * 100.0;
			else
				progressBarCatalogs.Value = 0;
			if (LibraryLCSC.LCSCBaseData.TotalCountPages != 0)
				progressBarPages.Value = (double)LibraryLCSC.LCSCBaseData.TotalReadPages / (double)LibraryLCSC.LCSCBaseData.TotalCountPages * 100.0;
			else
				progressBarPages.Value = 0;
		}

		private void buttonDownload_Click(object sender, RoutedEventArgs e)
		{
			timer.Start();
			LibraryLCSC.LCSCBaseData.IsCanceled = false;
			LibraryLCSC.LCSCBaseData.DownloadLibrary();
		}

		private void buttonUpdate_Click(object sender, RoutedEventArgs e)
		{
			timer.Start();
			LibraryLCSC.LCSCBaseData.IsCanceled = false;
			LibraryLCSC.LCSCBaseData.UpdateLibrary();
		}


		private void buttonStop_Click(object sender, RoutedEventArgs e)
		{
			LibraryLCSC.LCSCBaseData.IsCanceled = true;
			timer.Stop();
		}

		private void buttonOK_Click(object sender, RoutedEventArgs e)
		{
			LibraryLCSC.LCSCBaseData.StoredLibrary();
			LibraryLCSC.LCSCBaseData.StoredLibraryJson();
		}

		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{

		}

	}
}
