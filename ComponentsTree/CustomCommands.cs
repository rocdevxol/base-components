﻿using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace ComponentsTree
{
	public class CustomCommands : MarkupExtension
	{
		#region Меню "Файл"
		/// <summary>
		/// Создать проект
		/// </summary>
		public static RoutedCommand CreateProject = new RoutedCommand();

		/// <summary>
		/// Открыть проект
		/// </summary>
		public static RoutedCommand OpenProject = new RoutedCommand();

		/// <summary>
		/// Сохранить проект
		/// </summary>
		public static RoutedCommand SaveProject = new RoutedCommand();

		/// <summary>
		/// Сохранить проект с новым именем
		/// </summary>
		public static RoutedCommand SaveProjectAs = new RoutedCommand();

		/// <summary>
		/// Закрыть проект
		/// </summary>
		public static RoutedCommand CloseProject = new RoutedCommand();

		/// <summary>
		/// Экспорт полнного перечня компонентов в Excel
		/// </summary>
		public static RoutedCommand ExportProjectExcel = new RoutedCommand();

		#endregion

		#region Меню "Проект"
		/// <summary>
		/// Добавить плату
		/// </summary>
		public static RoutedCommand AddBoard = new RoutedCommand();

		/// <summary>
		///  Удалить плату
		/// </summary>
		public static RoutedCommand RemoveBoard = new RoutedCommand();

		/// <summary>
		/// Импорт платы
		/// </summary>
		public static RoutedCommand ImportBoard = new RoutedCommand();

		/// <summary>
		/// Импорт перечня компонентов Allegro
		/// </summary>
		public static RoutedCommand ImportComponentAllegro = new RoutedCommand();

		/// <summary>
		/// Импорт перечня компонентов Altium
		/// </summary>
		public static RoutedCommand ImportComponentAltium = new RoutedCommand();
		#endregion

		#region Контекстное меню 

		#region Проект
		/// <summary>
		/// Переименовать проект
		/// </summary>
		public static RoutedCommand RenameProject = new RoutedCommand();
		#endregion
	
		#region Плата
		/// <summary>
		/// Параметры платы
		/// </summary>
		public static RoutedCommand ParametersBoard = new RoutedCommand();

		/// <summary>
		/// Просмотр перечня компонентов
		/// </summary>
		public static RoutedCommand ShowComponentsList = new RoutedCommand();

		/// <summary>
		/// Просмотр перечня механических компонентов
		/// </summary>
		public static RoutedCommand ShowMechanicalList = new RoutedCommand();

		/// <summary>
		/// Добавить/Заменить гербер слой
		/// </summary>
		public static RoutedCommand AddChangeGerberLayer = new RoutedCommand();

		/// <summary>
		/// Добавить новый гербер слой
		/// </summary>
		public static RoutedCommand NewGerberLayer = new RoutedCommand();

		/// <summary>
		/// Просмотр слоя
		/// </summary>
		public static RoutedCommand ShowGerberLayer = new RoutedCommand();

		/// <summary>
		/// Просмотр всех слоев
		/// </summary>
		public static RoutedCommand ShowAllGerberLayers = new RoutedCommand();

		/// <summary>
		/// Экспорт гербер слоев
		/// </summary>
		public static RoutedCommand ExportGerberLayers = new RoutedCommand();


		#endregion

		#endregion

		/// <summary>
		/// Поиск компонентов в перечне
		/// </summary>
		public static RoutedCommand SearchParts = new RoutedCommand();

		/// <summary>
		/// Обновление компонентов
		/// Для сравнения, для удаления пустых позиций
		/// </summary>
		public static RoutedCommand RefreshParts = new RoutedCommand();

		#region LCSC
		/// <summary>
		/// Загрузка библиотеки компонентов LCSC
		/// </summary>
		public static RoutedCommand DownloadLibraryLCSC = new RoutedCommand();

		/// <summary>
		/// Обновление информации по компоненту из библиотеки
		/// </summary>
		public static RoutedCommand UpdateComponentLCSCLibrary = new RoutedCommand();
		#endregion


		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

	}
}
