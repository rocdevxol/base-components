using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace ComponentsTree.ShowModels
{
	public class CommandsShowModels : MarkupExtension
	{
		/// <summary>
		/// Добавить компонент/провод
		/// </summary>
		public static RoutedCommand AddComponent = new RoutedCommand();

		/// <summary>
		/// Удалить компонент/провод
		/// </summary>
		public static RoutedCommand RemoveComponent = new RoutedCommand();

		/// <summary>
		/// Изменить компонент/провод
		/// </summary>
		public static RoutedCommand ChangeComponent = new RoutedCommand();

		/// <summary>
		/// Проверить перечень компонентов
		/// </summary>
		public static RoutedCommand CheckComponents = new RoutedCommand();


		/// <summary>
		/// Экспорт перечня компонентов с указанием Позиционных обозначенией
		/// </summary>
		public static RoutedCommand ExportRefDesComponents = new RoutedCommand();

		/// <summary>
		/// Экспорт перечня компонентов BOM
		/// </summary>
		public static RoutedCommand ExportBOMComponents = new RoutedCommand();

		/// <summary>
		/// Экспорт перечня компонентов обобщенный
		/// </summary>
		public static RoutedCommand ExportJLCPosition = new RoutedCommand();

		/// <summary>
		/// Экспорт перечня компонентов обобщенный
		/// </summary>
		public static RoutedCommand ExportLCSC = new RoutedCommand();

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

	}
}
