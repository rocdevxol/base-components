using Models.Boards;
using System.Windows;

namespace ComponentsTree
{
	/// <summary>
	/// Логика взаимодействия для BoardWindow.xaml
	/// </summary>
	public partial class BoardWindow : Window
	{
		public string BoardName;
		public string BoardDescription;
		public string BoardDecimalNumber;
		public int Count;

		public BoardWindow(Board board)
		{
			InitializeComponent();

			BoardName = board.Name;
			BoardDescription = board.Description;
			BoardDecimalNumber = board.DecimalNumber;
			Count = board.Count;
		}

		public BoardWindow(string name, string descritpion, string decimalNumber, int count)
		{
			InitializeComponent();

			BoardName = name;
			BoardDescription = descritpion;
			BoardDecimalNumber = decimalNumber;
			Count = count;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			textBoxBoardName.Text = BoardName;
			textBoxBoardDescription.Text = BoardDescription;
			textBoxDecimalNumber.Text = BoardDecimalNumber;

			for (int i = 1; i <= 10; i++)
				comboBoxCount.Items.Add(i);

			if (Count == 0)
				Count = 1;

			comboBoxCount.SelectedItem = Count;
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			BoardName = textBoxBoardName.Text;
			BoardDescription = textBoxBoardDescription.Text;
			BoardDecimalNumber = textBoxDecimalNumber.Text;

			Count = (int)comboBoxCount.SelectedItem;

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
