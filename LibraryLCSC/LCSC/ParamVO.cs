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
	public class ParamVO : INotifyPropertyChanged
	{
		#region private variables
		private string paramCode;
		private string paramName;
		private string paramNameEn;
		private string paramValue;
		private string paramValueEn;
		private double? paramValueEnForSearch;
		private bool? isMain;
		private bool? sortNumber;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("paramCode")]
		public string ParamCode
		{
			get => paramCode;
			set
			{
				if (paramCode != value)
				{
					paramCode = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("paramName")]
		public string ParamName
		{
			get => paramName;
			set
			{
				if (paramName != value)
				{
					paramName = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("paramNameEn")]
		public string ParamNameEn
		{
			get => paramNameEn;
			set
			{
				if (paramNameEn != value)
				{
					paramNameEn = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("paramValue")]
		public string ParamValue
		{
			get => paramValue;
			set
			{
				if (paramValue != value)
				{
					paramValue = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("paramValueEn")]
		public string ParamValueEn
		{
			get => paramValueEn;
			set
			{
				if (paramValueEn != value)
				{
					paramValueEn = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("paramValueEnForSearch")]
		public double? ParamValueEnForSearch
		{
			get => paramValueEnForSearch;
			set
			{
				if (paramValueEnForSearch != value)
				{
					paramValueEnForSearch = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isMain")]
		public bool? IsMain
		{
			get => isMain;
			set
			{
				if (isMain != value)
				{
					isMain = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sortNumber")]
		public bool? SortNumber
		{
			get => sortNumber;
			set
			{
				if (sortNumber != value)
				{
					sortNumber = value;
					NotifyPropertyChanged();
				}
			}
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		public ParamVO()
		{
			ParamCode = null;
			ParamName = null;
			ParamNameEn = null;
			ParamValue = null;
			ParamValueEn = null;
			ParamValueEnForSearch = null;
			IsMain = null;
			SortNumber = null;
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
