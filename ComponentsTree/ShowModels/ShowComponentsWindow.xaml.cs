﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ComponentsTree.ShowModels
{
	/// <summary>
	/// Логика взаимодействия для ShowComponentsWindow.xaml
	/// </summary>
	public partial class ShowComponentsWindow : Window
	{
		public ObservableCollection<Models.Components.Component> ComponentsCollection { get; set; }

		public ObservableCollection<Models.Components.Component> ComponentsReport { get; set; }

		private Models.Components.Component Component { get; set; }

		/// <summary>
		/// Количество столбцов суб компонентов
		/// </summary>
		private int CountColumnsSubComponent { get; set; }

		public ShowComponentsWindow(Models.Components.ComponentList componentList, string nameBoard)
		{
			InitializeComponent();
			ComponentsCollection = componentList.Components;
			CountColumnsSubComponent = GetSubComponents(componentList.Components) + 1;
			if (nameBoard != string.Empty)
			{
				Title = string.Format("{0} - {1}", Title, nameBoard);
			}
		}
			
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				dataGridRefDes.ItemsSource = ComponentsCollection;
				//dataGridRefDes.ItemsSource = new ObservableCollection<Models.Components.Component>(ComponentsCollection);

				GenerateDataGridColumns(dataGridRefDes, CountColumnsSubComponent);

				Models.ReportComponents report = new Models.ReportComponents(ComponentsCollection);
				ComponentsReport = report.UpdateReport();
			}
			catch// (Exception ex)
			{
				MessageBox.Show("Сохраните, если вносились изменения. И переоткройте проект");
				Close();
			}
		}

		#region Команды
		/// <summary>
		/// Добавить компонент в перечень
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddComponent_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			EditionArrays.AddComponentWindow addComponentWindow = new EditionArrays.AddComponentWindow();
			bool? result = addComponentWindow.ShowDialog();
			if (result != true)
			{
				return;
			}

			ComponentsCollection.Add(addComponentWindow.Component);
		}

		/// <summary>
		/// Удалить выделенный компонент
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveComponent_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Component == null)
			{
				return;
			}

			ComponentsCollection.Remove(Component);
		}

		/// <summary>
		/// Изменить выделенный компонент
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChangeComponent_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Component == null)
			{
				return;
			}

			EditionArrays.AddComponentWindow addComponentWindow = new EditionArrays.AddComponentWindow(Component);
			bool? result = addComponentWindow.ShowDialog();
			if (result != true)
			{
				return;
			}

			int index = ComponentsCollection.IndexOf(Component);
			ComponentsCollection.RemoveAt(index);
			ComponentsCollection.Insert(index, addComponentWindow.Component);

			// Осуществляем обновление количества столбцов
			CountColumnsSubComponent = GetSubComponents(ComponentsCollection) + 1;
			GenerateDataGridColumns(dataGridRefDes, CountColumnsSubComponent);
		}

		/// <summary>
		/// Проверить перечень компонентов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckComponents_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (ComponentsCollection == null) return;
			if (ComponentsCollection.Count == 0) return;

			int empty = ClearEmptyParts();
			CheckComponents();
			SortComponents();
		}

		/// <summary>
		/// Экспорт перечня компонентов с указанием Позиционных обозначенией
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportRefDesComponents_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			ExportExcel.ExcelRefDesBoard.ExportDataToExcel(ExportExcel.ExcelRefDesBoard.CreateDataToExport(ComponentsCollection));
		}

		/// <summary>
		/// Экспорт перечня компонентов обобщенный
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportComponents_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Models.ReportComponents report = new Models.ReportComponents(ComponentsCollection);
			ObservableCollection<Models.Components.Component> result = report.UpdateReport();
			ExportExcel.ExcelExportTotalComps.ExportDataToExcel(ExportExcel.ExcelExportTotalComps.CreateDataToExport(result));
		}
		#endregion

		#region DataGrid Methods
		private void DataGridRefDes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems == null)
			{
				return;
			}

			if (e.AddedItems.Count == 0)
			{
				return;
			}

			if (e.AddedItems[0] as Models.Components.Component == null)
			{
				return;
			}

			Component = e.AddedItems[0] as Models.Components.Component;
		}

		private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			e.Row.Header = (e.Row.GetIndex() + 1).ToString();
		}

		private void DataGridRefDes_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			//e.Row.Header = (e.Row.GetIndex() + 1).ToString();
			if (e.Row.GetIndex() < ComponentsCollection.Count)
				e.Row.Header = (e.Row.GetIndex() + 1).ToString();
			else
				e.Row.Header = "*";
		}

		private void DataGridRefDes_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
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

		private void DataGridRefDes_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			for (int i = 0; i <= 100; i++)
			{
				;
			}
		}
		#endregion

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// (e.AddedItems[0] as TabItem)
			if ((sender as TabControl).SelectedIndex == 1)
			{
				Models.ReportComponents report = new Models.ReportComponents(ComponentsCollection);
				ComponentsReport = report.UpdateReport();
				int count = GetSubComponents(ComponentsReport);

				GenerateDataGridColumns(dataGridTotal, count);

				dataGridTotal.ItemsSource = ComponentsReport;
			}
			else
			{
				dataGridTotal.ItemsSource = null;
			}
		}

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

			int index = ComponentsCollection.IndexOf(change);
			ComponentsCollection.RemoveAt(index);
			ComponentsCollection.Insert(index, copy);
		}

		/// <summary>
		/// Определяем какое количество субкомпонентов находится в компоненте
		/// </summary>
		/// <param name="collection">Перечень компонентов</param>
		/// <returns>Количество субкомпонентов + 1</returns>
		private int GetSubComponents(ObservableCollection<Models.Components.Component> collection)
		{
			int max = -1;
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
			if ((sender as DataGrid).Name == dataGridRefDes.Name)
			{
				nowColumns = (nowColumns - 5) / 2;
				for (; nowColumns < count; nowColumns++)
				{
					AddTextColumn(sender, string.Format("Корпус {0}", nowColumns + 1), string.Format("Names[{0}].Package.Name", nowColumns));
					AddTextColumn(sender, string.Format("Значение {0}", nowColumns + 1), string.Format("Names[{0}].Name", nowColumns));
				}
			}
			else if ((sender as DataGrid).Name == dataGridTotal.Name)
			{
				nowColumns = (nowColumns - 3) / 2;
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
			for (int indexFirst = 0; indexFirst < ComponentsCollection.Count; indexFirst++)
			{
				Models.Components.Component first = ComponentsCollection[indexFirst];
				for (int indexSecond = 0; indexSecond < ComponentsCollection.Count; indexSecond++)
				{
					Models.Components.Component second = ComponentsCollection[indexSecond];

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

		/// <summary>
		/// Удаление пустых SubComponent в перечне
		/// </summary>
		private int ClearEmptyParts()
		{
			int countEmpty = 0;
			foreach (Models.Components.Component component in ComponentsCollection)
			{
				for (int i = 0; i < component.Names.Count; i++)
				{
					Models.Components.SubComponent sub = component.Names[i];
					if (sub.IsFulled() != true)
					{
						component.Names.Remove(sub);
						countEmpty++;
					}
				}
			}
			return countEmpty;
		}

		private void SortComponents()
		{
			ComponentsCollection.OrderBy(x => x.RefDes);
		}

		#endregion
	}
}