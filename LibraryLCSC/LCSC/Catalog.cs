using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLCSC.LCSC
{
	[Serializable]
	public class Catalog : INotifyPropertyChanged
	{
		#region private variables
		private int catalogId;
		private string catalogNameEn;
		private int productNum;
		private List<Catalog> childCatelogs;
		private List<Product> products;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("catalogId")]
		public int CatalogId
		{
			get => catalogId;
			set
			{
				if (catalogId != value)
				{
					catalogId = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("catalogNameEn")]
		public string CatalogNameEn
		{
			get => catalogNameEn;
			set
			{
				if (catalogNameEn != value)
				{
					catalogNameEn = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productNum")]
		public int ProductNum
		{
			get => productNum;
			set
			{
				if (productNum != value)
				{
					productNum = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("childCatelogs")]
		public List<Catalog> ChildCatelogs
		{
			get => childCatelogs;
			set
			{
				if (childCatelogs != value)
				{
					childCatelogs = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("products")]
		public List<Product> Products
		{
			get => products;
			set
			{
				if (products != value)
				{
					products = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion


		public Catalog()
		{
			CatalogId = 0;
			CatalogNameEn = "";
			ProductNum = 0;
			ChildCatelogs = new List<Catalog>();
			Products = new List<Product>();	
		}

		public override string ToString()
		{
			if (childCatelogs.Count > 0)
				return $"{CatalogId}: {CatalogNameEn}, [{ProductNum}], Child Catalogs: {ChildCatelogs.Count}";
			else
				return $"{CatalogId}: {CatalogNameEn}, [{ProductNum}]";
		}


		#region Events
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
