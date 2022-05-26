using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryLCSC.LCSC;
using LibraryLCSC.JLC;

namespace LibraryLCSC
{
	public class LCSCDownload
	{
		private const string CATEGORY = "https://wwwapi.lcsc.com/v1/home/category";
		private const string PRODUCT_CODE = "https://wwwapi.lcsc.com/v1/products/detail?product_code={0}";
		private const string PRODUCT_LIST = "https://wwwapi.lcsc.com/v1/products/list";

		private const string JLC = "https://jlcpcb.com/shoppingCart/smtGood/selectSmtComponentList";

		private static string GetRequest(string url)
		{
			try
			{
				HttpWebRequest request = HttpWebRequest.CreateHttp(url);
				request.Method = "GET";
				request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36";
				request.ContentType = "application/json;charset=utf-8";

				WebResponse response = request.GetResponse();
				Stream dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
				string data = reader.ReadToEnd();
				dataStream.Close();
				reader.Close();
					response.Close();
				return data;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return null;
		}

		private static string PostRequest(string url, string json_data)
		{
			try
			{
				byte[] data_send = Encoding.UTF8.GetBytes(json_data);
				
				HttpWebRequest request = HttpWebRequest.CreateHttp(url);
				request.Method = "POST";
				request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36";
				request.ContentType = "application/json;charset=utf-8";
				request.ContentLength = data_send.Length;
				request.Timeout = 10000;
				using (Stream stream = request.GetRequestStream())
				{
					stream.Write(data_send, 0, data_send.Length);
				}

				WebResponse response = request.GetResponse();
				Stream dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
				string data = reader.ReadToEnd();
				return data;
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message);
			}

			return null;
		}

		public static List<Catalog> DownloadCatalogs()
		{
			string data = GetRequest("https://wwwapi.lcsc.com/v1/home/category");
			List<Catalog> catalogs = JsonConvert.DeserializeObject<List<Catalog>>(data);
			return catalogs;
		}

		public static Product DownloadProduct(string productCode)
		{
			Product product = null;
			try
			{
				string data = GetRequest(string.Format(PRODUCT_CODE, productCode));
				if (!string.IsNullOrEmpty(data) && data.Length < 10)
					return null;
				product = JsonConvert.DeserializeObject<Product>(data);
			}
			catch (JsonSerializationException ex)
			{
				_ = MessageBox.Show(ex.Message);
			}
			return product;
		}

		public static List<Product> DownloadPageProducts(int pageNumber, int countOnPage, int catalog, ref int totalPages)
		{
			ProductPost post = new ProductPost(pageNumber, countOnPage, catalog);
			string json_data = JsonConvert.SerializeObject(post);
			string data = "";
			while (string.IsNullOrEmpty(data))
				data = PostRequest(PRODUCT_LIST, json_data);
			PageProducts products = JsonConvert.DeserializeObject<PageProducts>(data);
			totalPages = products.TotalPage;
			return products.ProductList;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="code">LCSC code</param>
		/// <returns>true - Is base component, false - is extended component, null - not can soldering</returns>
		public static bool? GetBaseComponentJLCPCB(string code)
		{
			SmtComponentListPost post = new SmtComponentListPost(code);
			string json_data = JsonConvert.SerializeObject(post);
			string data = PostRequest(JLC, json_data);
			string[] sorted = data.Split(new string[] { "\"componentPageInfo\":",  ",\"sortAndCountVoList\":"}, StringSplitOptions.None);
			ComponentInfo smt = JsonConvert.DeserializeObject<ComponentInfo>(sorted[1]);
			
			foreach (Component component in smt.List)
			{
				if (component.ComponentCode.Equals(code))
				{
					if (component.ComponentLibraryType.Equals("base"))
						return true;
					else if (component.ComponentLibraryType.Equals("expand"))
						return false;
				}
			}
			return null;
		}
	}
}
