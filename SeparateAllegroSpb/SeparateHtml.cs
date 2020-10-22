using HtmlAgilityPack;
using Models.Components;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace SeparateAllegroSpb
{
	public class SeparateHtml
	{
		public ObservableCollection<Component> ImportHtmlComponents(string fileName)
		{
			ObservableCollection<Component> list = new ObservableCollection<Component>();
			try
			{
				string html = new StreamReader(fileName).ReadToEnd();

				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(html);
				HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//tr");
				nodes.Remove(0);
				for (int i = 0; i < nodes.Count; i++)
				{
					HtmlNode node = nodes[i];
					HtmlNode[] items = node.ChildNodes.Where(x => x.Name == "td").ToArray();

					Component item = new Component(items[0].InnerText); // инициализация с учетом RefDes

					Package package = new Package(items[4].InnerText);
					string value = items[2].InnerText;
					if (SmdType(items[4].InnerText))
					{
						package = new Package(GetPackage(items[4].InnerText));
						value = GetValue(items[2].InnerText, items[4].InnerText);
					}
					item.Description = value;

					SubComponent subComponent = new SubComponent(value)
					{
						Package = package
					};

					item.Names.Add(subComponent);

					if (items[6].InnerText == "&nbsp;")
					{
						continue;
					}

					list.Add(item);
				}
			}
			catch(IOException ex)
			{
				MessageBox.Show("Файл открыт другим приложением", "Каталог компонентов", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return list;
		}

		private bool SmdType(string package)
		{
			try
			{
				string[] separate = package.Split(new char[] { '_', 'M' });
				if (separate.Length > 2)
				{
					return false;
				}

				if (int.Parse(separate[1]) == 0)
				{
					return false;
				}

				if (separate[0] == "C" || separate[0] == "R" || separate[0] == "L")
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private string GetValue(string oldvalue, string package)
		{
			//string value = oldvalue;
			string[] separate = package.Split(new char[] { '_', 'M' });
			string value = string.Format("{0}{1}    {2}", separate[0], separate[1], oldvalue);

			return value;
		}

		private string GetPackage(string package)
		{
			string[] separate = package.Split(new char[] { '_', 'M' });
			return string.Format("SMD{0}", separate[1]);
		}

	}
}
