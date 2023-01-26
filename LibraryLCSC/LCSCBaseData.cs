using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryLCSC
{
	public class LCSCBaseData
	{
		private const string FolderStoring = "D:\\LCSC\\";

		private static List<LCSC.Catalog> Catalogs { get; set; }

		public static int TotalCountCatalogs;
		public static int TotalReadCatalogs;
		public static int TotalCountPages;
		public static int TotalReadPages;

		private static Thread threadDownload;

		public static bool IsCanceled;
		public static bool IsUpdate;


		#region Download Library

		public static void DownloadLibrary()
		{
			Catalogs = new List<LCSC.Catalog>();
			TotalCountCatalogs = 0;
			TotalReadCatalogs = 0;
			TotalCountPages = 0;
			TotalReadPages = 0;
			IsUpdate = false;

			threadDownload = new Thread(new ThreadStart(download_catalog));
			threadDownload.Start();
		}

		public static void UpdateLibrary()
		{
			Catalogs = new List<LCSC.Catalog>();
			TotalCountCatalogs = 0;
			TotalReadCatalogs = 0;
			TotalCountPages = 0;
			TotalReadPages = 0;
			IsUpdate = true;

			threadDownload = new Thread(new ThreadStart(download_catalog));
			threadDownload.Start();
		}

		private static void download_catalog()
		{
			Catalogs = LCSCDownload.DownloadCatalogs();
			foreach (LCSC.Catalog catalog in Catalogs)
				GetCountCatalogs(catalog);

			foreach (LCSC.Catalog catalog in Catalogs)
			{
				if (IsCanceled)
					return;
				DownloadCatalog(catalog);
			}
		}

		private static void GetCountCatalogs(LCSC.Catalog catalog)
		{
			if (catalog.ChildCatelogs == null || catalog.ChildCatelogs.Count == 0)
			{
				TotalCountCatalogs++;
				return;
			}

			foreach (LCSC.Catalog c in catalog.ChildCatelogs)
				GetCountCatalogs(c);

		}

		private static void DownloadCatalog(LCSC.Catalog catalog)
		{
			if (catalog.ChildCatelogs == null || catalog.ChildCatelogs.Count == 0)
			{
				LCSC.Catalog c = null;
				if (IsUpdate == true)
					c = ReadCatalog(catalog.CatalogId);
				if (c != null && c.CatalogId == catalog.CatalogId)
					return;

				catalog.Products = DownloadProductListFromCatalog(catalog.CatalogId);
				
				StoredCatalog(catalog);
				catalog.Products = null;
				
				TotalReadCatalogs++;
				return;
			}

			foreach (LCSC.Catalog c in catalog.ChildCatelogs)
			{
				DownloadCatalog(c);
				if (IsCanceled)
					return;
			}
		}

		private static List<LCSC.Product> DownloadProductListFromCatalog(int catalogId)
		{
			int totalPages = 0;
			List<LCSC.Product> products = new List<LCSC.Product>();
			if (IsCanceled)
				return null;
			products.AddRange(LCSCDownload.DownloadPageProducts(1, 500, catalogId, ref totalPages));
			TotalCountPages = totalPages;
			TotalReadPages = 1;
			for (int pageNum = 2; pageNum < totalPages; pageNum++)
			{
				products.AddRange(LCSCDownload.DownloadPageProducts(pageNum, 500, catalogId, ref totalPages));
				TotalReadPages = pageNum;
				if (IsCanceled)
					return products;

			}

			return products;
		}

		#endregion

		#region Store Library
		/// <summary>
		/// Загрузка библиотеки из локального файла
		/// </summary>
		public static LCSC.Catalog ReadCatalog(int catalogId)
		{
			object obj = null;
			try
			{
				BinaryFormatter formatter = new BinaryFormatter();
				string fileName = string.Format($"{FolderStoring}Catalog_{catalogId}.lib");
				using (FileStream fs = new FileStream(fileName, FileMode.Open))
				{
					obj = (LCSC.Catalog)formatter.Deserialize(fs);
				}
			}
			catch (Exception)
			{

			}
			if (obj == null)
				return null;
			return obj as LCSC.Catalog;
		}


		/// <summary>
		/// Загрузка библиотеки из локального файла
		/// </summary>
		public static void ReadLibrary()
		{
			object obj = null;
			try
			{
				BinaryFormatter formatter = new BinaryFormatter();
				string fileName = string.Format($"{FolderStoring}LibraryLCSC.lib");
				using (FileStream fs = new FileStream(fileName, FileMode.Open))
				{
					obj = (List<LCSC.Catalog>)formatter.Deserialize(fs);
				}
			}
			catch (Exception)
			{

			}
			if (obj != null)
				Catalogs = obj as List<LCSC.Catalog>;
		}

		/// <summary>
		/// Загрузка библиотеки из локального файла Json
		/// </summary>
		public static void ReadLibraryJson()
		{
			string fileName = string.Format($"{FolderStoring}LibraryLCSC.json");
			string json = File.ReadAllText(fileName);
			List<LCSC.Catalog> obj = JsonConvert.DeserializeObject<List<LCSC.Catalog>>(json);
			if (obj != null)
				Catalogs = obj;
		}

		/// <summary>
		/// Сохранение библиотеки в локальный файл
		/// System.Runtime.Serialization.SerializationException: 'The internal array cannot expand to greater than Int32.MaxValue elements.'
		/// </summary>
		public static void StoredCatalog(LCSC.Catalog catalog)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			string fileName = string.Format($"{FolderStoring}Catalog_{catalog.CatalogId}.lib");
			using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, catalog);
			}
		}

		/// <summary>
		/// Сохранение библиотеки в локальный файл
		/// </summary>
		public static void StoredLibrary()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			string fileName = string.Format($"{FolderStoring}LibraryLCSC.lib");
			using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, Catalogs);
			}
		}

		/// <summary>
		/// Сохранение библиотеки в локальный файл JSON формат
		/// </summary>
		public static void StoredLibraryJson()
		{
			string json = JsonConvert.SerializeObject(Catalogs);
			string fileName = string.Format($"{FolderStoring}LibraryLCSC.json");
			using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
			{
				StreamWriter streamWriter = new StreamWriter(fs);
				streamWriter.WriteLine(json);
				streamWriter.Flush();
				streamWriter.Close();
			}
		}

		#endregion

		/// <summary>
		/// Получение информации по компоненту из библиотеки
		/// </summary>
		/// <param name="lcsc">Код компонента на LCSC</param>
		public static void GetComponent(string lcsc)
		{

		}
	}
}
