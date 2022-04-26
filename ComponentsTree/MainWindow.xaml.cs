using Microsoft.Win32;
using Models.Boards;
using Models.Components;
using Models.Gerber;
using Models.Mechanical;
using Models.Projects;
using Models.Wires;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ComponentsTree
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Данные
		/// <summary>
		/// Перечень проектов в дереве
		/// </summary>
		private ObservableCollection<Project> Projects { get; set; }

		/// <summary>
		/// Текущий рабочий проект
		/// </summary>
		private Project Project { get; set; }

		/// <summary>
		/// Текущая выбранная плата в дереве
		/// </summary>
		private Board Board { get; set; }

		/// <summary>
		/// Текущий Gerber слой
		/// </summary>
		private GerberLayer GerberLayer { get; set; }

		/// <summary>
		/// Каталог gerber файлов
		/// </summary>
		private Gerber GerberFolder { get; set; }

		/// <summary>
		/// Перечень компонентов
		/// </summary>
		private ComponentList ComponentList { get; set; }

		/// <summary>
		/// Используется для коллекции полей ввода
		/// </summary>
		private ObservableCollection<string> ComboBoxCollection;

		public static string ProjectFolder = string.Empty;
		#endregion

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Projects = new ObservableCollection<Project>();

			ComboBoxCollection = new ObservableCollection<string>();

			treeViewProject.ItemsSource = Projects;
			comboBoxSearch.ItemsSource = ComboBoxCollection;
		}

		#region Команды

		#region Меню Файл
		/// <summary>
		/// Создать проект
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateProject_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			NameProjectWindow nameProjectWindow = new NameProjectWindow(string.Empty);
			bool? result = nameProjectWindow.ShowDialog();
			if (result == true)
			{
				Projects.Add(new Project(nameProjectWindow.ProjectName));
			}
		}

		/// <summary>
		/// Открыть проект
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenProject_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Project project = OpenProject();
			if (project != null)
			{
				Projects.Add(project);
				ProjectFolder = project.ProjectFolder;
			}
		}

		/// <summary>
		/// Сохранить проект
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveProject_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Project == null)
			{
				return;
			}

			if (Project.FullNameProject == string.Empty)
			{
				SaveFileDialog sfd = new SaveFileDialog
				{
					Filter = "Проект печатных плат бинарный (*.prjbrd)|*.prjbrd|Проект печатных плат JSON (*.prjjson)|*.prjjson"
				};
				bool? result = sfd.ShowDialog();
				if (result == true)
				{
					Project.FullNameProject = sfd.FileName;
					ProjectFolder = Project.ProjectFolder;
				}
				else
				{
					return;
				}
			}
			if (Project.FullNameProject.Contains("json"))
			{
				ProjectJson projectJson = new ProjectJson(Project);
				Serilization.JsonSerilizate(Project.FullNameProject, projectJson);
				// проверка на правильность сохранения файла
				object obj = Serilization.JsonDeserilizate(Project.FullNameProject);
				if (obj == null)
				{
					_ = MessageBox.Show("Пересохраните файл, ошибка записи");
				}
			}
			else
			{
				Serilization.BinarySerilizate(Project.FullNameProject, Project);
			}
		}

		/// <summary>
		/// Сохранить проект с новым именем
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveProjectAs_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Project == null)
			{
				return;
			}

			SaveFileDialog sfd = new SaveFileDialog
			{
				Title = "Сохранить как...",
				Filter = "Проект печатных плат бинарный (*.prjbrd)|*.prjbrd|Проект печатных плат JSON (*.prjjson)|*.prjjson"
			};
			bool? result = sfd.ShowDialog();
			if (result == true)
			{
				Project.FullNameProject = sfd.FileName;
				ProjectFolder = Project.ProjectFolder;
			}
			else
			{
				return;
			}

			if (Project.FullNameProject.Contains("json"))
			{
				ProjectJson projectJson = new ProjectJson(Project);
				Serilization.JsonSerilizate(Project.FullNameProject, projectJson);
			}
			else
			{
				Serilization.BinarySerilizate(Project.FullNameProject, Project);
			}
		}

		/// <summary>
		/// Закрыть проект
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloseProject_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Project == null)
			{
				return;
			}

			_ = Projects.Remove(Project);
			ProjectFolder = string.Empty;
		}


		/// <summary>
		/// Экспорт полнного перечня компонентов в Excel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportProjectExcel_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Project == null)
			{
				_ = MessageBox.Show(Title, "Выберете проект в дереве");
				return;
			}
			ExportExcel.ExcelPrepare.Folder = ProjectFolder;
			ExportExcel.ExportProject.ExportComponentList(Project);

		}

		#endregion

		#region Меню Проект

		/// <summary>
		/// Добавить плату в перечень
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddBoard_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Project == null)
			{
				return;
			}

			BoardWindow boardWindow = new BoardWindow(string.Empty, string.Empty, string.Empty, 1);
			bool? result = boardWindow.ShowDialog();
			if (result == true)
			{
				Board board = new Board(boardWindow.BoardName, boardWindow.BoardDescription)
				{
					DecimalNumber = boardWindow.BoardDecimalNumber,
					Count = boardWindow.Count
				};
				Project.GetBoardList().Add(board);
			}
		}

		/// <summary>
		/// Добавить плату в перечень
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveBoard_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Board == null)
			{
				return;
			}

			Projects[0].GetBoardList().Boards.Remove(Board);

		}


		/// <summary>
		/// Импорт платы
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImportBoard_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Project project = OpenProject();
			if (project == null)
			{
				return;
			}

			ImportBoardWindow ibw = new ImportBoardWindow(project.GetBoardList().Boards, project.Name);
			bool? result = ibw.ShowDialog();
			if (result != true)
			{
				return;
			}

			Projects[0].GetBoardList().Add((Board)ibw.ImportedBoard.Clone());
		}

		/// <summary>
		/// Импорт перечня компонентов Allegro
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImportComponentAllegro_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Board == null)
			{
				return;
			}

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

			ObservableCollection<Component> components = separateHtml.ImportHtmlComponents(open.FileName);

			Board.GetComponentList().Components.Clear();

			foreach (Component component in components)
			{
				Board.GetComponentList().Add(component);
			}
		}

		/// <summary>
		/// Импорт перечня компонентов Altium
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImportComponentAltium_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Board == null)
			{
				return;
			}

			WindowImportAltium wia = new WindowImportAltium();
			bool? result = wia.ShowDialog();

			if (result != true)
				return;

			ObservableCollection<Component> components = wia.components;
			
			Board.GetComponentList().Components.Clear();

			foreach (Component component in components)
			{
				Board.GetComponentList().Add(component);
			}
		}


		#endregion

		#region ContextMenu Project
		/// <summary>
		/// Переименовать проект
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenameProject_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Project == null)
			{
				return;
			}

			NameProjectWindow nameProjectWindow = new NameProjectWindow(Project.Name);
			bool? result = nameProjectWindow.ShowDialog();
			if (result == true)
			{
				Project.Name = nameProjectWindow.ProjectName;
			}
		}
		#endregion

		#region ContextMenu Board
		/// <summary>
		/// Параметры печатной платы
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ParametersBoard_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Board == null)
			{
				return;
			}

			BoardWindow boardWindow = new BoardWindow(Board);
			bool? result = boardWindow.ShowDialog();
			if (result == true)
			{
				Board.Name = boardWindow.BoardName;
				Board.Description = boardWindow.BoardDescription;
				Board.DecimalNumber = boardWindow.BoardDecimalNumber;
				Board.Count = boardWindow.Count;
			}
		}

		/// <summary>
		/// Открыть перечень компонентов в печатной плате
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowComponentsList_Executed(object sender, ExecutedRoutedEventArgs e)
		{
		}

		/// <summary>
		/// Открыть перечень механических компонентов в печатной плате
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowMechanicalList_Executed(object sender, ExecutedRoutedEventArgs e)
		{
		}
		#endregion

		#region ContextMenu Gerber
		/// <summary>
		/// Добавить/Заменить слой в гербере
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddChangeGerberLayer_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();

			ofd.Filter = "Все файлы|*.*";
			bool? result = ofd.ShowDialog();
			if (result != true) return;

			string content = File.ReadAllText(ofd.FileName);

			GerberLayer.Content = content;
		}

		/// <summary>
		/// Добавить новый слой в плату
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewGerberLayer_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();

			ofd.Filter = "Все файлы|*.*";
			bool? result = ofd.ShowDialog();
			if (result != true) return;

			string content = File.ReadAllText(ofd.FileName);
			GerberFolder.AddLayer(content);
		}

		/// <summary>
		/// Показать слой гербера
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowGerberLayer_Executed(object sender, ExecutedRoutedEventArgs e)
		{
		}

		/// <summary>
		/// Показать все слои гербера
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowAllGerberLayers_Executed(object sender, ExecutedRoutedEventArgs e)
		{
		}

		/// <summary>
		/// Экспорт гербер слоев
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportGerberLayers_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}
		#endregion

		/// <summary>
		/// Поиск перечня компонентов (электронных компонентов, механических компонентов, проводов)
		/// по всем параметрам
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchParts_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			string search = e.Parameter.ToString();
			if (search == null || search == string.Empty)
			{
				return;
			}

			if (Projects.Count == 0)
			{
				return;
			}

			// поиск по платам
			List<Component> components = FindComponents(search);
			List<MechanicalComp> mechanicals = FindMechanical(search);
			List<Wire> wires = FindWires(search);

			ShowModels.WindowFindedElements wfe = new ShowModels.WindowFindedElements();
			if (components == null || components.Count == 0)
			{
				wfe.Components = null;
			}
			else
			{
				wfe.Components = components;
			}

			if (mechanicals == null || mechanicals.Count == 0)
			{
				wfe.Mechanicals = null;
			}
			else
			{
				wfe.Mechanicals = mechanicals;
			}

			if (wires == null || wires.Count == 0)
			{
				wfe.Wires = null;
			}
			else
			{
				wfe.Wires = wires;
			}

			wfe.ShowDialog();
		}

		#endregion

		#region Методы
		/// <summary>
		/// Отображение перечня компонентов
		/// </summary>
		private void ShowComponentList()
		{
			ComponentList componentList = null;
			string nameBoard = string.Empty;
			if (Board != null)
			{
				nameBoard = Board.Name;
				componentList = Board.GetComponentList();
			}
			else if (ComponentList != null)
			{
				componentList = ComponentList;
			}

			if (componentList != null)
			{
				ShowModels.ShowComponentsWindow window = new ShowModels.ShowComponentsWindow(componentList, nameBoard);
				window.Show();
			}
		}

		/// <summary>
		/// Открытие проекта из бинарного формата и JSON 
		/// </summary>
		/// <returns>Проект</returns>
		private Project OpenProject()
		{
			OpenFileDialog open = new OpenFileDialog
			{
				Filter = "Проект печатных плат бинарный (*.prjbrd)|*.prjbrd|Проект печатных плат JSON (*.prjjson)|*.prjjson"
			};
			bool? result = open.ShowDialog();
			if (result != true)
			{
				return null;
			}

			Project project = null;
			if (open.FilterIndex == 1)
			{
				project = (Project)Serilization.BinaryDeserilizate(open.FileName);
			}
			else if (open.FilterIndex == 2)
			{
				object projectJson = Serilization.JsonDeserilizate(open.FileName);
				if (projectJson != null)
				{
					project = new Project((ProjectJson)projectJson);
				}
				else
				{
					return null;
				}
			}
			project.FullNameProject = open.FileName;
			SortParts(project); // Сортировка частей проекта при открытии
			return project;
		}

		/// <summary>
		/// Сортировка компонентов в проекте
		/// </summary>
		/// <param name="project"></param>
		private void SortParts(Project project)
		{
			List<MechanicalComp> listMechanical;
			List<Component> listComponents;
			List<Wire> listWires;

			foreach (Board board in project.GetBoardList().Boards)
			{
				// 1. Сортировка компонентов в платах
				listComponents = board.GetComponentList().Components.ToList();
				listComponents.Sort(new ComponentRefdesCompare());
				//listComponents.Sort(ComponentRefdesCompare<Component>);
				board.GetComponentList().Components = new ObservableCollection<Component>(listComponents);

				// 1.1. Сортировка механических компонентов в платах
				listMechanical = board.GetMechanicalList().MechanicalComps.ToList();
				listMechanical.Sort();
				board.GetMechanicalList().MechanicalComps = new ObservableCollection<MechanicalComp>(listMechanical);
			}

			// 2. Сортировка механических компонентов в проекте
			listMechanical = project.GetMechanicalList().MechanicalComps.ToList();
			listMechanical.Sort();
			project.GetMechanicalList().MechanicalComps = new ObservableCollection<MechanicalComp>(listMechanical);

			// 3. Сoртировка проводов в проекте
			listWires = project.GetWireList().Wires.ToList();
			listWires.Sort();
			project.GetWireList().Wires = new ObservableCollection<Wire>(listWires);
		}

		/// <summary>
		/// Поиск списка электронных компонентов
		/// </summary>
		/// <param name="search">Строка по которой производится поиск
		/// '@' - в начале строки означает поиск по полному совпадению
		/// </param>
		/// <returns>список найденных компонентов</returns>
		private List<Component> FindComponents(string search)
		{
			List<Component> componentsFinded = new List<Component>(); // перечень электронных компонентов по платам
			foreach (Board board in Projects[0].GetBoardList().Boards)
			{
				List<Component> components = board.GetComponentList().Components.ToList();
				List<Component> comps = components.FindAll(i => i.FindElement(search));
				componentsFinded.AddRange(comps);
			}
			return componentsFinded.Count == 0 ? null : componentsFinded;
		}

		/// <summary>
		/// Поиск списка механических компонентов
		/// </summary>
		/// <param name="search">Строка по которой производится поиск
		/// '@' - в начале строки означает поиск по полному совпадению
		/// </param>
		/// <returns>список найденных механических компонентов</returns>
		private List<MechanicalComp> FindMechanical(string search)
		{
			List<MechanicalComp> mechanicalFinded = new List<MechanicalComp>(); // перечень электронных компонентов по платам

			return mechanicalFinded.Count == 0 ? null : mechanicalFinded;
		}

		/// <summary>
		/// Поиск списка проводов
		/// </summary>
		/// <param name="search">Строка по которой производится поиск
		/// '@' - в начале строки означает поиск по полному совпадению
		/// </param>
		/// <returns>список найденных проводов</returns>
		private List<Wire> FindWires(string search)
		{
			if (search is null)
			{
				throw new ArgumentNullException(nameof(search));
			}

			List<Wire> wireFinded = new List<Wire>(); // перечень электронных компонентов по платам

			return wireFinded.Count == 0 ? null : wireFinded;
		}
		#endregion

		#region TreeView
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeViewProject_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			Project = null;
			Board = null;
			ComponentList = null;
			GerberLayer = null;
			GerberFolder = null;

			if ((e.NewValue as Project) != null)
			{
				Project = e.NewValue as Project;
				treeViewProject.ContextMenu = treeViewProject.Resources["contextProject"] as ContextMenu;
				ProjectFolder = Project.ProjectFolder;
			}
			else if ((e.NewValue as BoardList) != null)
			{

			}
			else if ((e.NewValue as Board) != null)
			{
				Board = e.NewValue as Board;
				treeViewProject.ContextMenu = treeViewProject.Resources["contextBoard"] as ContextMenu;
			}
			else if ((e.NewValue as ComponentList) != null)
			{
				ComponentList = e.NewValue as ComponentList;
			}
			else if ((e.NewValue as Component) != null)
			{
				treeViewProject.ContextMenu = treeViewProject.Resources["contextComponent"] as ContextMenu;
			}
			else if ((e.NewValue as MechanicalList) != null)
			{

			}
			else if ((e.NewValue as MechanicalComp) != null)
			{

			}
			else if ((e.NewValue as WireList) != null)
			{

			}
			else if ((e.NewValue as Wire) != null)
			{

			}
			else if ((e.NewValue as Gerber) != null)
			{
				GerberFolder = e.NewValue as Gerber;
				treeViewProject.ContextMenu = treeViewProject.Resources["contextMenuGerbers"] as ContextMenu;
			}
			else if ((e.NewValue as GerberLayer) != null)
			{
				GerberLayer = e.NewValue as GerberLayer;
				treeViewProject.ContextMenu = treeViewProject.Resources["contextGerberLayer"] as ContextMenu;
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeViewProject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (Project != null)
			{
				// изменение название проекта
				NameProjectWindow npm = new NameProjectWindow(Project.Name);
				bool? result = npm.ShowDialog();
				if (result != true)
				{
					return;
				}

				Project.Name = npm.ProjectName;
			}
			else if (Board != null)
			{
				ShowComponentList();
			}
			else if (ComponentList != null)
			{
				ShowComponentList();
			}
		}
		#endregion

		#region Search Components
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonSearch_Click(object sender, RoutedEventArgs e)
		{
			string search = comboBoxSearch.Text;
			if (!ComboBoxCollection.Contains(search))
			{
				ComboBoxCollection.Add(search);
			}

			comboBoxSearch.Text = string.Empty;
			commandSearchParts.Command.Execute(search);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ComboBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			//https://ru.stackoverflow.com/questions/549935/%D0%96%D0%B8%D0%B2%D0%BE%D0%B9-%D0%BF%D0%BE%D0%B8%D1%81%D0%BA-system-windows-controls-combobox-c-wpf
			/*
			 CB.IsDropDownOpen = true;
			// убрать selection, если dropdown только открылся
			var tb = (TextBox)e.OriginalSource;
			tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
			CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB.ItemsSource);
			cv.Filter = s => ((string)s).IndexOf(CB.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;*/

			// убрать selection, если dropdown только открылся
			TextBox tb = (TextBox)e.OriginalSource;

			if (tb.SelectionStart != 0)
			{
				comboBoxSearch.SelectedItem = null; // Если набирается текст сбросить выбранный элемент
			}

			if (tb.SelectionStart == 0 && comboBoxSearch.SelectedItem == null)
			{
				comboBoxSearch.IsDropDownOpen = false;
			}
			else
			{
				comboBoxSearch.IsDropDownOpen = true;
			}

			tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
			CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(comboBoxSearch.ItemsSource);
			cv.Filter = s => ((string)s).IndexOf(comboBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ComboBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				string search = comboBoxSearch.Text;
				if (!ComboBoxCollection.Contains(search))
				{
					ComboBoxCollection.Add(search);
				}

				comboBoxSearch.Text = string.Empty;
				commandSearchParts.Command.Execute(search);
			}
		}
		#endregion
	}
}
