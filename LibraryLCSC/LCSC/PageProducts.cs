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
	public class PageProducts : INotifyPropertyChanged
	{
		#region private variables
		private int totalCount;
		private int currentPage;
		private int pageSize;
		private List<Product> productList;
		private int totalPage;
		private int lastPage;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("totalCount")]
		public int TotalCount
		{
			get => totalCount;
			set
			{
				if (totalCount != value)
				{
					totalCount = value;
					NotifyPropertyChanged();
				}
			}
		}

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
		/// </summary>
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
		[JsonProperty("productList")]
		public List<Product> ProductList
		{
			get => productList;
			set
			{
				if (productList != value)
				{
					productList = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("totalPage")]
		public int TotalPage
		{
			get => totalPage;
			set
			{
				if (totalPage != value)
				{
					totalPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("lastPage")]
		public int LastPage
		{
			get => lastPage;
			set
			{
				if (lastPage != value)
				{
					lastPage = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public PageProducts()
		{
			TotalCount = 0;
			CurrentPage = 0;
			PageSize = 0;
			ProductList = new List<Product>();
			TotalPage = 0;
			LastPage = 0;
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
