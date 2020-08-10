using System.Windows;

namespace ComponentsTree
{
	/// <summary>
	/// Логика взаимодействия для NameProjectWindow.xaml
	/// </summary>
	public partial class NameProjectWindow : Window
	{
		/// <summary>
		/// Наименование проекта при создании и при переименовании
		/// </summary>
		public string ProjectName;

		public NameProjectWindow(string projectName)
		{
			InitializeComponent();
			ProjectName = projectName;
			if (ProjectName != string.Empty)
			{
				Title = "Изменить проект";
			}
			
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			textBoxProjectName.Text = ProjectName;
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			ProjectName = textBoxProjectName.Text;
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
