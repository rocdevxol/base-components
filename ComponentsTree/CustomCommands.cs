using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace ComponentsTree
{
	public class CustomCommands : MarkupExtension
	{
		#region Меню "файл"
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


		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

	}
}
