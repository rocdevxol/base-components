﻿using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using LibraryLCSC;

namespace ComponentsTree.ShowModels
{
	/// <summary>
	/// Логика взаимодействия для ShowComponentsWindow.xaml
	/// </summary>
	public partial class ShowComponentsWindow : Window
	{
		private string ProjectFolder = string.Empty;
		private string NameBoard = string.Empty;

		private const int DefaultNumColumnsDescription = 7;
		private const int DefaultNumColumnsList = 4;

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
			NameBoard = nameBoard;
			if (nameBoard != string.Empty)
			{
				Title = string.Format("{0} - {1}", Title, nameBoard);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				ProjectFolder = MainWindow.ProjectFolder;

				dataGridRefDes.ItemsSource = ComponentsCollection;
				dataGridPosition.ItemsSource = ComponentsCollection;
				//dataGridRefDes.ItemsSource = new ObservableCollection<Models.Components.Component>(ComponentsCollection);

				GenerateDataGridColumns(dataGridRefDes, CountColumnsSubComponent);

				Models.ReportComponents report = new Models.ReportComponents(ComponentsCollection);
				ComponentsReport = report.UpdateReport();

				ComponentsCollection.CollectionChanged += ComponentsCollection_CollectionChanged;
				CalculatePrice();
			}
			catch// (Exception ex)
			{
				_ = MessageBox.Show("Сохраните, если вносились изменения. И переоткройте проект");
				Close();
			}
		}

		private void ComponentsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			CalculatePrice();
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

			_ = ComponentsCollection.Remove(Component);
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
			if (ComponentsCollection == null)
			{
				return;
			}

			if (ComponentsCollection.Count == 0)
			{
				return;
			}

			_ = ClearEmptyParts();
			CheckComponents();
			SortComponents();
			CalculatePrice();
		}

		/// <summary>
		/// Экспорт перечня компонентов с указанием Позиционных обозначенией
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportRefDesComponents_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			ExportExcel.ExcelPrepare.Folder = ProjectFolder;
			ExportExcel.ExcelRefDesBoard.ExportDataToExcel(ExportExcel.ExcelRefDesBoard.CreateDataToExport(ComponentsCollection));
		}

		/// <summary>
		/// Экспорт перечня компонентов обобщенный
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportBOMComponents_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Models.ReportComponents report = new Models.ReportComponents(ComponentsCollection);
			ObservableCollection<Models.Components.Component> result = report.UpdateReport();
			ExportExcel.ExcelPrepare.Folder = ProjectFolder;
			ExportExcel.ExcelExportBOM.ExportDataToExcel(ExportExcel.ExcelExportBOM.CreateDataToExport(result));
		}

		/// <summary>
		/// Экспорт координат для JLC
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportJLCPosition_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			ObservableCollection<Models.Components.Component> components = new ObservableCollection<Models.Components.Component>();
			foreach (Models.Components.Component component in ComponentsCollection)
			{
				if (checkBoxOnlySmdParts.IsChecked == true)
				{
					if (component.Names[0].Package.PackageType == Models.Components.PackageType.SMD_SMT)
					{
						components.Add(component);
					}
				}
				else
				{
					components.Add(component);
				}
			}
			ExportExcel.ExcelPrepare.Folder = ProjectFolder;
			ExportExcel.ExcelExportPosition.ExportDataToExcel(ExportExcel.ExcelExportPosition.CreateDataToExport(components), $"{NameBoard}_CPL");
		}

		/// <summary>
		/// Экспорт информации о компонентах
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportLCSC_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			ObservableCollection<Models.Components.Component> components = new ObservableCollection<Models.Components.Component>();
			foreach (Models.Components.Component component in ComponentsCollection)
			{
				if (checkBoxOnlySmdParts.IsChecked == true)
				{
					if (component.Names[0].Package.PackageType == Models.Components.PackageType.SMD_SMT)
					{
						components.Add(component);
					}
				}
				else
				{
					components.Add(component);
				}
			}

			Models.ReportComponents report = new Models.ReportComponents(components);
			ObservableCollection<Models.Components.Component> result = report.UpdateReport();
			ExportExcel.ExcelPrepare.Folder = ProjectFolder;
			ExportExcel.ExcelExportLCSC.ExportDataToExcel(ExportExcel.ExcelExportLCSC.CreateDataToExport(result), $"{NameBoard}_BOM");
		}
		
		private void UpdateComponentLCSCLibrary_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			foreach (Models.Components.Component component in ComponentsCollection)
			{
				string lcsc = component.Names.First().LCSC;
				if (string.IsNullOrEmpty(lcsc))
					continue;
				LibraryLCSC.LCSC.Product product = LCSCDownload.DownloadProduct(lcsc);
				if (product == null)
				{
					MessageBox.Show(Title, $"Проверьте компонент {component.RefDes}, код {lcsc} не найден в базе данных", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				component.TypeComponent = product.CatalogName;
				component.Names[0].Name = product.ProductModel;
				component.Names[0].Developer.Name = product.BrandNameEn;
				component.Names[0].Distributor.Name = "LCSC";
				component.Description = product.ProductIntroEn;
				if (product.ProductPriceList.Count > 0)
					component.Price = (double)product.ProductPriceList[0].UsdPrice;
				component.Names[0].Package.Name = product.EncapStandard;
			}
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
			{
				e.Row.Header = (e.Row.GetIndex() + 1).ToString();
			}
			else
			{
				e.Row.Header = "*";
			}
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
			if (e.Column.DisplayIndex == 6) // Price
			{
				CalculatePrice();
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
			if (index == -1)
			{
				return;
			}

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
			const int addColumns = 3;//2
			int nowColumns = (sender as DataGrid).Columns.Count;
			if ((sender as DataGrid).Name == dataGridRefDes.Name)
			{
				nowColumns = (nowColumns - DefaultNumColumnsDescription) / addColumns;
				for (; nowColumns < count; nowColumns++)
				{
					AddTextColumn(sender, string.Format("Корпус {0}", nowColumns + 1), string.Format("Names[{0}].Package.Name", nowColumns));
					AddTextColumn(sender, string.Format("Цена {0}", nowColumns + 1), string.Format("Names[{0}].Price", nowColumns), "F4");
					AddTextColumn(sender, string.Format("Значение {0}", nowColumns + 1), string.Format("Names[{0}].Name", nowColumns));
				}
			}
			else if ((sender as DataGrid).Name == dataGridTotal.Name)
			{
				nowColumns = (nowColumns - DefaultNumColumnsList) / addColumns;
				for (; nowColumns < count; nowColumns++)
				{
					AddTextColumn(sender, string.Format("Корпус {0}", nowColumns + 1), string.Format("Names[{0}].Package.Name", nowColumns));
					AddTextColumn(sender, string.Format("Цена {0}", nowColumns + 1), string.Format("Names[{0}].Price", nowColumns), "F4");
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
		/// Добавление столбцов в DataGrid
		/// </summary>
		/// <param name="sender">Наименование DataGrid</param>
		/// <param name="header">Отформатированный заголовок</param>
		/// <param name="binding">Название параметра на который делается ссылка</param>
		/// /// <param name="stringFormat">Форматирование выходного значениея</param>
		private void AddTextColumn(object sender, string header, string binding, string stringFormat)
		{
			DataGridTextColumn column = new DataGridTextColumn
			{
				Header = header,
				Binding = new Binding(binding)
			};
			column.Binding.StringFormat = stringFormat;
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
				if (string.IsNullOrEmpty(first.EquivalentName))
					first.EquivalentName = first.Description;
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

		private void CalculatePrice()
		{
			double price = 0;
			int smdComps = 0, thtComps = 0;
			int smdPins = 0, thtPins = 0;
			foreach (Models.Components.Component component in ComponentsCollection)
			{
				if (component == null)
				{
					continue;
				}

				if (component.Names.Count == 0)
				{
					continue;
				}

				price += component.Names[0].Price * component.Count;
				if (component.Names[0].Package.PackageType == Models.Components.PackageType.SMD_SMT)
				{
					smdComps++;
					smdPins += component.Names[0].Package.NumPins;
				}
				else if (component.Names[0].Package.PackageType == Models.Components.PackageType.THT)
				{
					thtComps++;
					thtPins += component.Names[0].Package.NumPins;
				}
			}
			textBlockTotalPrice.Text = price.ToString("F2");
			textBlockThtComps.Text = string.Format($"{thtComps} ({thtPins})");
			textBlockSmdComps.Text = string.Format($"{smdComps} ({smdPins})");
		}
		#endregion

		private void ButtonOffsetAll_Click(object sender, RoutedEventArgs e)
		{
			double dx = double.Parse(textBoxDx.Text);
			double dy = double.Parse(textBoxDy.Text);

			foreach (Models.Components.Component component in ComponentsCollection)
			{
				if (component == null)
				{
					continue;
				}

				component.Position.PositionX += dx;
				component.Position.PositionY += dy;
			}
		}

		private void ButtonOffsetSelected_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ButtonRotateClockwise_Click(object sender, RoutedEventArgs e)
		{
			if (dataGridPosition.CurrentItem == null)
			{
				return;
			}

			Models.Components.Component component = dataGridPosition.CurrentItem as Models.Components.Component;
			component.Position.Angle += 90;
			if (component.Position.Angle > 360)
			{
				component.Position.Angle -= 360;
			}
		}

		private void ButtonRotateCounterClockwise_Click(object sender, RoutedEventArgs e)
		{
			if (dataGridPosition.CurrentItem == null)
			{
				return;
			}

			Models.Components.Component component = dataGridPosition.CurrentItem as Models.Components.Component;
			component.Position.Angle -= 90;
			if (component.Position.Angle < 0)
			{
				component.Position.Angle += 360;
			}
		}

		private void ButtonImportPosition_Click(object sender, RoutedEventArgs e)
		{
			// Прежде осуществляем загрузку массива в отдельный список.
			OpenFileDialog open = new OpenFileDialog
			{
				Filter = "HTML файл (*.htm, *.html)|*.htm;*.html"
			};
			bool? result = open.ShowDialog();
			if (result != true)
			{
				return;
			}

			SeparateAllegroSpb.SeparateHtml separateHtml = new SeparateAllegroSpb.SeparateHtml();
			ObservableCollection<Models.Components.Component> components = separateHtml.ImportHtmlComponents(open.FileName);

			foreach (Models.Components.Component component in ComponentsCollection)
			{
				string refdes = component.RefDes;
				Models.Components.Position position = null;
				foreach (Models.Components.Component c0 in components)
				{
					if (c0.RefDes == refdes)
					{
						position = c0.Position;
						break;
					}
				}

				if (position != null)
				{
					component.Position = position;
				}
			}
		}
	}
}
