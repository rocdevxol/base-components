using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExportExcel
{
	/// <summary>
	/// read http://csharp.net-informations.com/excel/csharp-create-excel.htm
	/// </summary>
	public class ExcelPrepare
	{
		public static string Folder = string.Empty;

		public Excel.Application ExcelApp { get; set; }

		public Excel.Workbook ExcelWorkBook { get; set; }

		public Excel.Worksheet ExcelWorkSheet { get; set; }

		private readonly object misValue = System.Reflection.Missing.Value;

		public ExcelPrepare(string name)
		{
			ExcelApp = new Excel.Application();

			if (ExcelApp == null)
			{
				_ = MessageBox.Show("Экспорт в Excel", "Excel не установлен", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			ExcelWorkBook = ExcelApp.Workbooks.Add(misValue);
			//SaveFile(name);
		}

		public void AddSheet(string name)
		{
			if (ExcelWorkSheet != null)
			{
				ExcelWorkBook.Worksheets.Add(misValue, ExcelWorkSheet, 1, misValue);
			}

			int count = ExcelWorkBook.Worksheets.Count;
			ExcelWorkSheet = (Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(count);
			if (name != string.Empty)
			{
				ExcelWorkSheet.Name = name;
			}
		}

		public void Update()
		{
			ExcelApp.ScreenUpdating = true;
		}

		public void VisibleExcel(bool visible)
		{
			ExcelApp.Visible = visible;
		}


		public void SaveFile(string name)
		{
			if (Folder == string.Empty)
			{
				// TODO Добавить выбор директории
			}
			string fullName = $"{Folder}\\{name}";

			ExcelWorkBook.SaveAs(fullName, misValue, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
		}

		public void CloseWorkBook()
		{
			ExcelWorkBook.Close(true, misValue, misValue);
		}

		public void QuitExcel()
		{
			ExcelApp.Quit();
		}
	}
}
