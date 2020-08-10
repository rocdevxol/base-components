using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;
using System.Windows;

namespace ComponentsTree
{
	public static class Serilization
	{
		/// <summary>
		/// Серилизация объекта в бинарном формате
		/// </summary>
		/// <param name="fileName">Имя файла</param>
		/// <param name="obj">Объект</param>
		public static void BinarySerilizate(string fileName, object obj)
		{
			BinaryFormatter formatter = new BinaryFormatter();

			using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, obj);
			}
		}

		/// <summary>
		/// Десериализация объекта из бинарного формата 
		/// </summary>
		/// <param name="fileName">Имя файла</param>
		/// <returns>Извлеченный объект</returns>
		public static object BinaryDeserilizate(string fileName)
		{ 
			object obj = null;
			try
			{
				BinaryFormatter formatter = new BinaryFormatter();
				using (FileStream fs = new FileStream(fileName, FileMode.Open))
				{
					obj = (Models.Projects.Project)formatter.Deserialize(fs);
				}
			}
			catch //(Exception ex)
			{
				MessageBox.Show("Файл содержит ошибку, проверьте правильность файла", "Открытие файла");
			}
			return obj;
		}


		/// <summary>
		/// Серилизация объекта в бинарном формате
		/// </summary>
		/// <param name="fileName">Имя файла</param>
		/// <param name="obj">Объект</param>
		public static void JsonSerilizate(string fileName, object obj)
		{
			string json = new JavaScriptSerializer().Serialize(obj);
			
			using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
			{
				StreamWriter streamWriter = new StreamWriter(fs);
				streamWriter.WriteLine(json);
				streamWriter.Flush();
				streamWriter.Close();
			}
			object res = CheckSaveJsonDeserilizate(fileName);
			if (obj != null && res == null)
				MessageBox.Show("Сохранение файла произошло с ошибкой, пересохраните повторно", "Сохранить");
		}

		/// <summary>
		/// Десериализация объекта из бинарного формата 
		/// </summary>
		/// <param name="fileName">Имя файла</param>
		/// <returns>Извлеченный объект</returns>
		public static object JsonDeserilizate(string fileName)
		{
			object obj = null;
			try
			{
				string json = File.ReadAllText(fileName);
				obj = new JavaScriptSerializer().Deserialize<Models.Projects.ProjectJson>(json);
			}
			catch //(ArgumentException ex)
			{
				MessageBox.Show("Файл содержит ошибку, проверьте правильность файла", "Открытие файла");
			}
			return obj;
		}


		private static object CheckSaveJsonDeserilizate(string fileName)
		{
			object obj = null;
			try
			{
				string json = File.ReadAllText(fileName);
				obj = new JavaScriptSerializer().Deserialize<Models.Projects.ProjectJson>(json);
			}
			catch //(ArgumentException ex)
			{
				//MessageBox.Show("Сохранение файла произошло с ошибкой, пересохраните повторно", "Сохранить");
			}
			return obj;
		}
	}
}
