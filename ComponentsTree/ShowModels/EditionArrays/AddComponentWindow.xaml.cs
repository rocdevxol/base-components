using System.Windows;
using System.Windows.Controls;

namespace ComponentsTree.ShowModels.EditionArrays
{
	/// <summary>
	/// Логика взаимодействия для AddComponentWindow.xaml
	/// </summary>
	public partial class AddComponentWindow : Window
	{
		public Models.Components.Component Component{ get; private set; }
		
		public AddComponentWindow()
		{
			InitializeComponent();
			Component = new Models.Components.Component();
		}

		public AddComponentWindow(Models.Components.Component component)
		{
			InitializeComponent();
			Title = "Изменить компонент";
			Component = (Models.Components.Component)component.Clone();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LayoutRoot.DataContext = Component;
		}

		private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			if (e.Row.GetIndex() < Component.Names.Count)
				e.Row.Header = (e.Row.GetIndex() + 1).ToString();
			else
				e.Row.Header = "*";
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}

		private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
		{
			//Component.Names.Add(new Models.Components.SubComponent());
		}
	}
}
