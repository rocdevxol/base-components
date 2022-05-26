using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace LibraryLCSC.JLC
{
	[Serializable]
	internal class ComponentInfo : INotifyPropertyChanged, IDataSmtComponentList
	{
		#region private variables
		private int? endRow;
		private bool? hasNextPage;
		private bool? hasPreviousPage;
		private bool? isFirstPage;
		private bool? isLastPage;
		private List<Component> list;
		private int? navigateFirstPage;
		private int? navigateLastPage;
		private int? navigatePages;
		private int? navigatepageNums;
		private int? nextPage;
		private int? pageNum;
		private int? pageSize;
		private int? pages;
		private int? prePage;
		private int? size;
		private int? startRow;
		private int? total;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("endRow")]
		public int? EndRow
		{
			get => endRow;
			set
			{
				if (endRow != value)
				{
					endRow = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("hasNextPage")]
		public bool? HasNextPage
		{
			get => hasNextPage;
			set
			{
				if (hasNextPage != value)
				{
					hasNextPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("hasPreviousPage")]
		public bool? HasPreviousPage
		{
			get => hasPreviousPage;
			set
			{
				if (hasPreviousPage != value)
				{
					hasPreviousPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isFirstPage")]
		public bool? IsFirstPage
		{
			get => isFirstPage;
			set
			{
				if (isFirstPage != value)
				{
					isFirstPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isLastPage")]
		public bool? IsLastPage
		{
			get => isLastPage;
			set
			{
				if (isLastPage != value)
				{
					isLastPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("list")]
		public List<Component> List
		{
			get => list;
			set
			{
				if (list != value)
				{
					list = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("navigateFirstPage")]
		public int? NavigateFirstPage
		{
			get => navigateFirstPage;
			set
			{
				if (navigateFirstPage != value)
				{
					navigateFirstPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("navigateLastPage")]
		public int? NavigateLastPage
		{
			get => navigateLastPage;
			set
			{
				if (navigateLastPage != value)
				{
					navigateLastPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("navigatePages")]
		public int? NavigatePages
		{
			get => navigatePages;
			set
			{
				if (navigatePages != value)
				{
					navigatePages = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("navigatepageNums")]
		public int? NavigatepageNums
		{
			get => navigatepageNums;
			set
			{
				if (navigatepageNums != value)
				{
					navigatepageNums = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("nextPage")]
		public int? NextPage
		{
			get => nextPage;
			set
			{
				if (nextPage != value)
				{
					nextPage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("pageNum")]
		public int? PageNum
		{
			get => pageNum;
			set
			{
				if (pageNum != value)
				{
					pageNum = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("pageSize")]
		public int? PageSize
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
		[JsonProperty("pages")]
		public int? Pages
		{
			get => pages;
			set
			{
				if (pages != value)
				{
					pages = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("prePage")]
		public int? PrePage
		{
			get => prePage;
			set
			{
				if (prePage != value)
				{
					prePage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("size")]
		public int? Size
		{
			get => size;
			set
			{
				if (size != value)
				{
					size = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("startRow")]
		public int? StartRow
		{
			get => startRow;
			set
			{
				if (startRow != value)
				{
					startRow = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("total")]
		public int? Total
		{
			get => total;
			set
			{
				if (total != value)
				{
					total = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public ComponentInfo()
		{
			EndRow = null;
			HasNextPage = null;
			HasPreviousPage = null;
			IsFirstPage = null;
			IsLastPage = null;
			List = new List<Component>();
			NavigateFirstPage = null;
			NavigateLastPage = null;
			NavigatePages = null;
			NavigatepageNums = null;
			NextPage = null;
			PageNum = null;
			PageSize = null;
			Pages = null;
			PrePage = null;
			Size = null;
			StartRow = null;
			Total = null;
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
