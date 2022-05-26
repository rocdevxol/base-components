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
	public class ChildSort : INotifyPropertyChanged, IDataSmtComponentList
	{
		#region private variables
		private List<ChildSort> childSortList;
		private int? componentCount;
		private int? componentSortKeyId;
		private int? grade;
		private int? parentId;
		private string sortImgUrl;
		private string sortName;
		private string sortUuid;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("childSortList")]
		public List<ChildSort> ChildSortList
		{
			get => childSortList;
			set
			{
				if (childSortList != value)
				{
					childSortList = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentCount")]
		public int? ComponentCount
		{
			get => componentCount;
			set
			{
				if (componentCount != value)
				{
					componentCount = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentSortKeyId")]
		public int? ComponentSortKeyId
		{
			get => componentSortKeyId;
			set
			{
				if (componentSortKeyId != value)
				{
					componentSortKeyId = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("grade")]
		public int? Grade
		{
			get => grade;
			set
			{
				if (grade != value)
				{
					grade = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("parentId")]
		public int? ParentId
		{
			get => parentId;
			set
			{
				if (parentId != value)
				{
					parentId = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sortImgUrl")]
		public string SortImgUrl
		{
			get => sortImgUrl;
			set
			{
				if (sortImgUrl != value)
				{
					sortImgUrl = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sortName")]
		public string SortName
		{
			get => sortName;
			set
			{
				if (sortName != value)
				{
					sortName = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sortUuid")]
		public string SortUuid
		{
			get => sortUuid;
			set
			{
				if (sortUuid != value)
				{
					sortUuid = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public ChildSort()
		{
			ChildSortList = new List<ChildSort>();
			ComponentCount = null;
			ComponentSortKeyId = null;
			Grade = null;
			ParentId = null;
			SortImgUrl = null;
			SortName = null;
			SortUuid = null;
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
