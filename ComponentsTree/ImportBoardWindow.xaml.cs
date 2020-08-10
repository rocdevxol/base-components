using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// Логика взаимодействия для ImportBoardWindow.xaml
	/// </summary>
	public partial class ImportBoardWindow : Window
	{
		private readonly ObservableCollection<Models.Boards.Board> Boards;

		/// <summary>
		/// Ипортируемая плата в проект
		/// </summary>
		public Models.Boards.Board ImportedBoard {get; private set;}

		public ImportBoardWindow(ObservableCollection<Models.Boards.Board> boards, string projectName)
		{
			InitializeComponent();
			Boards = boards;
			Title = string.Format("Импорт печатной платы : {0}", projectName);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			listViewBoards.ItemsSource = Boards;
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

		private void ListViewBoards_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ImportedBoard = (Models.Boards.Board)e.AddedItems[0];
		}
	}
}
