using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExportExcel
{
	public static class ExcelExportTotalComps
	{
		private static object[,] Data
		{
			get;
			set;
		}
		private static Thread excelThread;

		public static void ExportDataToExcel(object[,] data)
		{
			Data = data;
			excelThread = new Thread(new ThreadStart(Export_excel));
			excelThread.Start();
		}


		public static object[,] CreateDataToExport(ObservableCollection<Models.Components.Component> list)
		{
			int countNames = -1;
			foreach (Models.Components.Component component in list)
			{
				if (component.Names.Count > countNames)
					countNames = component.Names.Count;
			}

			object[,] data = new object[list.Count + 1, 3 + 2 * countNames];
			for (int i = 1; i <= list.Count; i++)
			{
				data[i, 0] = list[i - 1].TypeComponent;
				data[i, 1] = list[i - 1].Description;
				data[i, 2] = list[i - 1].Count;
				for (int j = 0; j < list[i - 1].Names.Count; j++)
				{
					data[i, 3 + j * 2] = list[i - 1].Names[j].Name;
					data[i, 4 + j * 2] = list[i - 1].Names[j].Package.Name;
				}
			}

			data[0, 0] = "Тип компонента";
			data[0, 1] = "Описание";
			data[0, 2] = "Количество";
			for (int j = 0; j < countNames; j++)
			{
				data[0, 3 + j * 2] = string.Format("Наименование {0}", j + 1);
				data[0, 4 + j * 2] = string.Format("Корпус {0}", j + 1);
			}
			
			return data;
		}

		private static void Export_excel()
		{
			dynamic excel = Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application", string.Empty);

			excel.ScreenUpdating = false;
			dynamic workbook = excel.workbooks;
			workbook.Add();

			dynamic worksheet = excel.ActiveSheet;

			const int left = 1;
			const int top = 1;
			int height = Data.GetLength(0);
			int width = Data.GetLength(1);
			int bottom = top + height - 1;
			int right = left + width - 1;

			if (height == 0 || width == 0)
			{
				return;
			}

			dynamic rg = worksheet.Range[worksheet.Cells[top, left], worksheet.Cells[bottom, right]];

			rg.Value = Data;

			// Set borders
			for (int i = 1; i <= 4; i++)
			{
				rg.Borders[i].LineStyle = 1;
			}

			// Set auto columns width
			rg.EntireColumn.AutoFit();

			// Set header view
			dynamic rgHeader = worksheet.Range[worksheet.Cells[top, left], worksheet.Cells[top, right]];
			rgHeader.Font.Bold = true;
			//rgHeader.Interior.Color = 189 * (int)Math.Pow(16, 4) + 129 * (int)Math.Pow(16, 2) + 78; // #4E81BD
			rgHeader.Interior.Color = 247 * (int)Math.Pow(16, 4) + 235 * (int)Math.Pow(16, 2) + 221; // #4E81BD

			// Show excel app
			excel.ScreenUpdating = true;
			excel.Visible = true;

			Marshal.ReleaseComObject(rg);
			Marshal.ReleaseComObject(rgHeader);
			Marshal.ReleaseComObject(worksheet);
			Marshal.ReleaseComObject(workbook);
			Marshal.ReleaseComObject(excel);

			GC.Collect();
			Data = null;
		}

		public static void ExportData(Excel.Worksheet sheet, ObservableCollection<Models.Components.Component> list)
		{
			Data = CreateDataToExport(list);

			const int left = 1;
			const int top = 1;
			int height = Data.GetLength(0);
			int width = Data.GetLength(1);
			int bottom = top + height - 1;
			int right = left + width - 1;

			if (height == 0 || width == 0)
			{
				return;
			}

			dynamic rg = sheet.Range[sheet.Cells[top, left], sheet.Cells[bottom, right]];
			rg.Value = Data;

			for (int i = 1; i <= 4; i++)
				rg.Borders[i].LineStyle = 1;

			// Set auto columns width
			rg.EntireColumn.AutoFit();

			// Set header view
			dynamic rgHeader = sheet.Range[sheet.Cells[top, left], sheet.Cells[top, right]];
			rgHeader.Font.Bold = true;
			//rgHeader.Interior.Color = 189 * (int)Math.Pow(16, 4) + 129 * (int)Math.Pow(16, 2) + 78; // #4E81BD
			rgHeader.Interior.Color = 247 * (int)Math.Pow(16, 4) + 235 * (int)Math.Pow(16, 2) + 221; // #4E81BD

		}

	}
}
