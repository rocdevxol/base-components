using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLCSC
{
	public static class LibraryLCSC
	{

		/// <summary>
		/// Возвращает информацию из библиотеки компонентов, если в библиотеке отсутствует компонент, то скачивает из интернета
		/// </summary>
		/// <param name="lcsc">Код компонента на LCSC</param>
		/// <returns>null - если компонент не найден, LCSC.Product - если есть в базе или интернете</returns>
		public static LCSC.Product GetInformationProduct(string lcsc)
		{
			LCSC.Product product = LCSCDownload.DownloadProduct(lcsc);
			return product;
		}
	}
}
