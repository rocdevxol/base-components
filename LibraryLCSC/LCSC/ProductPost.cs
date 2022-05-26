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
	public class ProductPost : INotifyPropertyChanged
	{
		#region private variables
		private int currentPage;
		private int pageSize;
		private List<int> catalogIdList;
		private Dictionary<string, string> paramNameValueMap;
		private List<string> brandIdList;
		private bool isStock;
		private bool isEnvironmet;
		private bool isHot;
		private bool isDiscount;
		private List<string> encapValueList;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("currentPage")]
		public int CurrentPage
		{
			get => currentPage;
			set
			{
				if (currentPage != value)
				{
					currentPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summarypageSize
		[JsonProperty("pageSize")]
		public int PageSize
		{
			get => pageSize;
			set
			{
				if (pageSize != value)
				{
					pageSize = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("catalogIdList")]
		public List<int> CatalogIdList
		{
			get => catalogIdList;
			set
			{
				if (catalogIdList != value)
				{
					catalogIdList = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("paramNameValueMap")]
		public Dictionary<string, string> ParamNameValueMap
		{
			get => paramNameValueMap;
			set
			{
				if (paramNameValueMap != value)
				{
					paramNameValueMap = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("brandIdList")]
		public List<string> BrandIdList
		{
			get => brandIdList;
			set
			{
				if (brandIdList != value)
				{
					brandIdList = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isStock")]
		public bool IsStock
		{
			get => isStock;
			set
			{
				if (isStock != value)
				{
					isStock = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isEnvironmet")]
		public bool IsEnvironmet
		{
			get => isEnvironmet;
			set
			{
				if (isEnvironmet != value)
				{
					isEnvironmet = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isHot")]
		public bool IsHot
		{
			get => isHot;
			set
			{
				if (isHot != value)
				{
					isHot = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isDiscount")]
		public bool IsDiscount
		{
			get => isDiscount;
			set
			{
				if (isDiscount != value)
				{
					isDiscount = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("encapValueList")]
		public List<string> EncapValueList
		{
			get => encapValueList;
			set
			{
				if (encapValueList != value)
				{
					encapValueList = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public ProductPost(int page, int pageSize, int catalogId)
		{
			CurrentPage = page;
			PageSize = pageSize;
			CatalogIdList = new List<int>();
			CatalogIdList.Add(catalogId);
			ParamNameValueMap = new Dictionary<string, string>();
			BrandIdList = new List<string>();
			IsStock = false;
			IsEnvironmet = false;
			IsHot = false;
			IsDiscount = false;
			EncapValueList = new List<string>();
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
