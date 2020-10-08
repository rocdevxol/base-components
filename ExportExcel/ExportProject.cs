using System;
using System.Threading;
using System.Windows;

namespace ExportExcel
{
	public static class ExportProject
	{
		private static Thread excelComponentList;

		private static Models.Projects.Project Project;
		private static Models.Boards.Board Board;

		/// <summary>
		/// Экспорт перечня электронных компонентов с проекта
		/// </summary>
		/// <param name="project">Рабочий проект</param>
		public static void ExportComponentList(Models.Projects.Project project)
		{
			Project = project;
			excelComponentList = new Thread(new ThreadStart(Export_project_component_list));
			excelComponentList.Start();
		}

		/// <summary>
		/// Экспорт перечня электронных компонентов с печатной платы
		/// </summary>
		/// <param name="board">Печатная плата</param>
		public static void ExportComponentList(Models.Boards.Board board)
		{
			Board = board;
			excelComponentList = new Thread(new ThreadStart(Export_board_component_list));
			excelComponentList.Start();
		}

		#region процессы электронных компонентов
		/// <summary>
		/// Экспорт перечня электронных компонентов, со всего проекта
		/// </summary>
		private static void Export_project_component_list()
		{
			try
			{
				ExcelPrepare excel = new ExcelPrepare(string.Format("Перечень компонентов {0}", Project.Name));
				Models.ReportComponents report = new Models.ReportComponents();

				foreach (Models.Boards.Board board in Project.GetBoardList().Boards)
				{
					if (!string.IsNullOrEmpty(board.DecimalNumber))
						excel.AddSheet(string.Format("{0} - Компоненты", board.Name));
					else
						excel.AddSheet(string.Format("{0} [{1}] - Компоненты", board.Name, board.DecimalNumber));
					ExcelRefDesBoard.ExportData(excel.ExcelWorkSheet, board.GetComponentList().Components);
					report.AddComponents(board);
				}

				report.UpdateReport();
				excel.AddSheet("Перечень для закупки");
				ExcelExportTotalComps.ExportData(excel.ExcelWorkSheet, report.Report);

				excel.Update();
				excel.VisibleExcel(true);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Экспорт в Excel", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		/// <summary>
		/// Экспорт перечня электронных компонентов, с печатной платы
		/// </summary>
		private static void Export_board_component_list()
		{
			ExcelPrepare excel = new ExcelPrepare(Board.Name);
			Models.ReportComponents report = new Models.ReportComponents();

			excel.AddSheet(string.Format("{0} - Комоненты", Board.Name));
			ExcelRefDesBoard.ExportData(excel.ExcelWorkSheet, Board.GetComponentList().Components);
			report.AddComponents(Board);

			report.UpdateReport();
			excel.AddSheet("Перечень для закупки");
			ExcelExportTotalComps.ExportData(excel.ExcelWorkSheet, report.Report);

			excel.Update();
			excel.VisibleExcel(true);
		}
		#endregion
	}
}
