using Microsoft.Win32;
using Models.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace ComponentsTree
{
	/// <summary>
	/// Логика взаимодействия для WindowImportAltium.xaml
	/// </summary>
	public partial class WindowImportAltium : Window
	{
		public ObservableCollection<Component> components { get; set; }

		public WindowImportAltium()
		{
			InitializeComponent();
		}

		private void ButtonBom_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog
			{
				Filter = "Текстовый файл (*.txt)|*.txt"
			};
			bool? result = open.ShowDialog();
			if (result != true)
			{
				return;
			}
			textBoxBom.Text = open.FileName;

			if (textBoxBom.Text != string.Empty && textBoxPickPlace.Text != string.Empty)
				buttonCheck.IsEnabled = true;
			buttonImport.IsEnabled = false;
			buttonOk.IsEnabled = false;
		}

		private void ButtonPickPlace_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog
			{
				Filter = "Текстовый файл (*.txt)|*.txt"
			};
			bool? result = open.ShowDialog();
			if (result != true)
			{
				return;
			}
			textBoxPickPlace.Text = open.FileName;

			if (textBoxBom.Text != string.Empty && textBoxPickPlace.Text != string.Empty)
				buttonCheck.IsEnabled = true;
			buttonImport.IsEnabled = false;
			buttonOk.IsEnabled = false;
		}

		private void ButtonCheck_Click(object sender, RoutedEventArgs e)
		{
			StreamReader reader = new StreamReader(textBoxPickPlace.Text);
			string result = reader.ReadLine();
			reader.Close();
			if (result != "Altium Designer Pick and Place Locations")
			{
				MessageBox.Show("Проверьте правильность введенных файлов");
				return;
			}

			reader = new StreamReader(textBoxBom.Text);
			result = reader.ReadLine();
			reader.Close();
			if (result != "Designator\tComment\tDescription\tFootprint\tLibRef\tLCSC\tPart Number")
			{
				MessageBox.Show("Проверьте правильность введенных файлов\n BOM: Designator Comment Description Footprint   LibRef LCSC    Part Number");
				return;
			}

			buttonImport.IsEnabled = true;
		}

		private void ButtonImport_Click(object sender, RoutedEventArgs e)
		{
			SeparateAltium.SeparateBomPickPlace pickPlace = new SeparateAltium.SeparateBomPickPlace();
			Dictionary<string, Position> position = pickPlace.ImportPickPlace(textBoxPickPlace.Text);
			components = pickPlace.ImportBom(textBoxBom.Text);
			buttonOk.IsEnabled = true;
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
