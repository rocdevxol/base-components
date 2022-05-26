using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLCSC.JLC
{
	[Serializable]
	public class SmtComponentListPost : INotifyPropertyChanged
	{
		#region private variables
		private List<string> componentAttributes;
		private int currentPage;
		private string firstSortName;
		private string keyword;
		private int pageSize;
		private string searchSource;
		private string secondeSortName;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentAttributes")]
		public List<string> ComponentAttributes
		{
			get => componentAttributes;
			set
			{
				if (componentAttributes != value)
				{
					componentAttributes = value;
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
		[JsonProperty("firstSortName")]
		public string FirstSortName
		{
			get => firstSortName;
			set
			{
				if (firstSortName != value)
				{
					firstSortName = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("keyword")]
		public string Keyword
		{
			get => keyword;
			set
			{
				if (keyword != value)
				{
					keyword = value;
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
		[JsonProperty("searchSource")]
		public string SearchSource
		{
			get => searchSource;
			set
			{
				if (searchSource != value)
				{
					searchSource = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("secondeSortName")]
		public string SecondeSortName
		{
			get => secondeSortName;
			set
			{
				if (secondeSortName != value)
				{
					secondeSortName = value;
					NotifyPropertyChanged();
				}
			}
		}

		#endregion

		public SmtComponentListPost(string code)
		{
			ComponentAttributes = new List<string>();
			CurrentPage = 1;
			FirstSortName = "";
			Keyword = code;
			pageSize = 100;
			SearchSource = "search";
			SecondeSortName = "";
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
