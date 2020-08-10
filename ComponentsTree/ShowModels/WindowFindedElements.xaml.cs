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

namespace ComponentsTree.ShowModels
{
	/// <summary>
	/// Логика взаимодействия для WindowFindedElements.xaml
	/// </summary>
	public partial class WindowFindedElements : Window
	{
		public List<Models.Components.Component> Components { get; set; }
		public List<Models.Mechanical.MechanicalComp> Mechanicals { get; set; }
		public List<Models.Wires.Wire> Wires { get; set; }

		private Models.Components.Component component;
		private Models.Mechanical.MechanicalComp Mechanical;
		private Models.Wires.Wire Wire;


		/// <summary>
		/// Количество столбцов суб компонентов
		/// </summary>
		private int CountColumnsSubComponent { get; set; }

		public WindowFindedElements()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (Components != null)
			{
				dataGridComponents.ItemsSource = Components;
			}
			else
			{
				dataGridComponents.ItemsSource = null;
				tabItemComponents.Visibility = Visibility.Collapsed;
			}

			if (Mechanicals != null)
			{
				dataGridMechanical.ItemsSource = Mechanicals;
			}
			else
			{
				dataGridMechanical.ItemsSource = null;
				tabItemMechanical.Visibility = Visibility.Collapsed;
			}

			if (Wires != null)
			{
				dataGridWires.ItemsSource = Wires;
			}
			else
			{
				dataGridWires.ItemsSource = null;
				tabItemWires.Visibility = Visibility.Collapsed;
			}

			if (Components != null && Components.Count != 0)
			{
				CountColumnsSubComponent = GetSubComponents(Components) + 1;
				GenerateDataGridColumns(dataGridComponents, CountColumnsSubComponent);
			}
		}


		#region DataGrid Methods
		private void DataGridComponents_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems == null) return;
			if (e.AddedItems.Count == 0) return;
			if (e.AddedItems[0] as Models.Components.Component == null) return;

			component = e.AddedItems[0] as Models.Components.Component;
		}

		private void DataGridComponents_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			//e.Row.Header = (e.Row.GetIndex() + 1).ToString();
			if (e.Row.GetIndex() < Components.Count)
				e.Row.Header = (e.Row.GetIndex() + 1).ToString();
			else
				e.Row.Header = "*";
		}

		private void DataGridComponents_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			if (e.Column.SortMemberPath.Contains("Names"))
			{
				string[] parts = e.Column.SortMemberPath.Split(new char[] { '[', ']' });
				int number = int.Parse(parts[1]) + 1;
				Models.Components.Component component = (Models.Components.Component)e.Row.Item;
				while (component.Names.Count < number)
				{
					component.Names.Add(new Models.Components.SubComponent(string.Empty));
				}

				if (number >= CountColumnsSubComponent)
				{
					CountColumnsSubComponent = number + 1;
					GenerateDataGridColumns(sender, CountColumnsSubComponent);
				}
			}
		}

		private void DataGridComponents_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			for (int i = 0; i <= 100; i++)
			{
				;
			}
		}
		#endregion


		#region Дополнительные методы
		/// <summary>
		/// Вывод необходимой строки для несоответствующих компонентов
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		private string PrepareString(object first, object second)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("Не соответствие компонентов\n");
			builder.Append("\nКОМПОНЕНТ 1:\n");
			builder.Append(first);
			builder.Append("\nКОМПОНЕНТ 2:\n");
			builder.Append(second);
			return builder.ToString();
		}

		/// <summary>
		/// Замена компонентов в перечне
		/// </summary>
		/// <param name="copied">Элемент который копируется</param>
		/// <param name="change">Элемент, который должен быть заменен</param>
		private void ChangeParts(Models.Components.Component copied, Models.Components.Component change)
		{
			Models.Components.Component copy = (Models.Components.Component)copied.Clone();
			copy.RefDes = change.RefDes;

			int index = Components.IndexOf(change);
			Components.RemoveAt(index);
			Components.Insert(index, copy);
		}

		/// <summary>
		/// Определяем какое количество субкомпонентов находится в компоненте
		/// </summary>
		/// <param name="collection">Перечень компонентов</param>
		/// <returns>Количество субкомпонентов + 1</returns>
		private int GetSubComponents(List<Models.Components.Component> collection)
		{
			int max = -1;
			if (collection == null) return 1;
			foreach (Models.Components.Component component in collection)
			{
				if (component.Names.Count > max)
				{
					max = component.Names.Count;
				}
			}

			return max;
		}

		/// <summary>
		/// Генерация столбцов в DataGrid
		/// </summary>
		/// <param name="count">Максимальное количество субэлементов</param>
		private void GenerateDataGridColumns(object sender, int count)
		{
			int nowColumns = (sender as DataGrid).Columns.Count;
			if ((sender as DataGrid).Name == dataGridComponents.Name)
			{
				nowColumns = (nowColumns - 5) / 2;
				for (; nowColumns < count; nowColumns++)
				{
					AddTextColumn(sender, string.Format("Корпус {0}", nowColumns + 1), string.Format("Names[{0}].Package.Name", nowColumns));
					AddTextColumn(sender, string.Format("Значение {0}", nowColumns + 1), string.Format("Names[{0}].Name", nowColumns));
				}
			}
		}

		/// <summary>
		/// Добавление столбцов в DataGrid
		/// </summary>
		/// <param name="sender">Наименование DataGrid</param>
		/// <param name="header">Отформатированный заголовок</param>
		/// <param name="binding">Название параметра на который делается ссылка</param>
		private void AddTextColumn(object sender, string header, string binding)
		{
			DataGridTextColumn column = new DataGridTextColumn
			{
				Header = header,
				Binding = new Binding(binding)
			};
			(sender as DataGrid).Columns.Add(column);
		}

		/// <summary>
		/// Осуществляет проверку на дубликаты с некорректными параметрами
		/// </summary>
		private void CheckComponents()
		{
			for (int indexFirst = 0; indexFirst < Components.Count; indexFirst++)
			{
				Models.Components.Component first = Components[indexFirst];
				for (int indexSecond = 0; indexSecond < Components.Count; indexSecond++)
				{
					Models.Components.Component second = Components[indexSecond];

					if (first.CompareTo(second) != 0)
					{
						if (first.Description == second.Description)
						{
							CustomControls.UserMessageBox messageBox = new CustomControls.UserMessageBox("Подготовка компонентов", PrepareString(first, second), CustomControls.UserMessageBox.UserMessageButtons.Apply2Cancel);
							messageBox.ShowDialog();
							if (messageBox.DialogResult == CustomControls.UserMessageBox.UserMessageResult.Apply1)  // вместо второго копируем первый
							{
								ChangeParts(first, second);
							}
							else if (messageBox.DialogResult == CustomControls.UserMessageBox.UserMessageResult.Apply2) // вместо первого копируем второй
							{
								ChangeParts(second, first);
							}
						}
					}
				}
			}
		}
		#endregion
	}
}
