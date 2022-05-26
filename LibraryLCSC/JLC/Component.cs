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
	public class Component : INotifyPropertyChanged
	{
		#region private variables
		private int canPresaleNumber;
		private string componentBrandEn;
		private string componentCode;
		private int componentId;
		private string componentImageUrl;
		private string componentLibraryType;
		private string componentModelEn;
		private List<ComponentPrice> componentPrices;
		
		private string componentSource;
		private string componentSpecificationEn;
		private string componentTypeEn;
		private string dataManualUrl;
		private string describe;
		private string erpComponentName;
		private string firstSortAccessId;
		private string lcscGoodsUrl;
		private string minImage;
		private string secondSortAccessId;
		private int stockCount;
		#endregion

		#region product variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("canPresaleNumber")]
		public int CanPresaleNumber
		{
			get => canPresaleNumber;
			set
			{
				if (canPresaleNumber != value)
				{
					canPresaleNumber = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentCode")]
		public string ComponentCode
		{
			get => componentCode;
			set
			{
				if (componentCode != value)
				{
					componentCode = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentModelEn")]
		public string ComponentModelEn
		{
			get => componentModelEn;
			set
			{
				if (componentModelEn != value)
				{
					componentModelEn = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentId")]
		public int ComponentId
		{
			get => componentId;
			set
			{
				if (componentId != value)
				{
					componentId = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentLibraryType")]
		public string ComponentLibraryType
		{
			get => componentLibraryType;
			set
			{
				if (componentLibraryType != value)
				{
					componentLibraryType = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentSource")]
		public string ComponentSource
		{
			get => componentSource;
			set
			{
				if (componentSource != value)
				{
					componentSource = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("firstSortAccessId")]
		public string FirstSortAccessId
		{
			get => firstSortAccessId;
			set
			{
				if (firstSortAccessId != value)
				{
					firstSortAccessId = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentBrandEn")]
		public string ComponentBrandEn
		{
			get => componentBrandEn;
			set
			{
				if (componentBrandEn != value)
				{
					componentBrandEn = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentSpecificationEn")]
		public string ComponentSpecificationEn
		{
			get => componentSpecificationEn;
			set
			{
				if (componentSpecificationEn != value)
				{
					componentSpecificationEn = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentTypeEn")]
		public string ComponentTypeEn
		{
			get => componentTypeEn;
			set
			{
				if (componentTypeEn != value)
				{
					componentTypeEn = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("secondSortAccessId")]
		public string SecondSortAccessId
		{
			get => secondSortAccessId;
			set
			{
				if (secondSortAccessId != value)
				{
					secondSortAccessId = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("stockCount")]
		public int StockCount
		{
			get => stockCount;
			set
			{
				if (stockCount != value)
				{
					stockCount = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentPrices")]
		public List<ComponentPrice> ComponentPrices
		{
			get => componentPrices;
			set
			{
				if (componentPrices != value)
				{
					componentPrices = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("componentImageUrl")]
		public string ComponentImageUrl
		{
			get => componentImageUrl;
			set
			{
				if (componentImageUrl != value)
				{
					componentImageUrl = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("minImage")]
		public string MinImage
		{
			get => minImage;
			set
			{
				if (minImage != value)
				{
					minImage = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("dataManualUrl")]
		public string DataManualUrl
		{
			get => dataManualUrl;
			set
			{
				if (dataManualUrl != value)
				{
					dataManualUrl = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("describe")]
		public string Describe
		{
			get => describe;
			set
			{
				if (describe != value)
				{
					describe = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("erpComponentName")]
		public string ErpComponentName
		{
			get => erpComponentName;
			set
			{
				if (erpComponentName != value)
				{
					erpComponentName = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("lcscGoodsUrl")]
		public string LcscGoodsUrl
		{
			get => lcscGoodsUrl;
			set
			{
				if (lcscGoodsUrl != value)
				{
					lcscGoodsUrl = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public Component()
		{
			CanPresaleNumber = 0;
			ComponentBrandEn = null;
			ComponentCode = null;
			ComponentId = 0;
			ComponentImageUrl = null;
			ComponentLibraryType = null;
			ComponentModelEn = null;
			ComponentPrices = new List<ComponentPrice>();
			ComponentSource = null;
			ComponentSpecificationEn = null;
			ComponentTypeEn = null;			
			DataManualUrl = null;
			Describe = null;
			ErpComponentName = null;
			FirstSortAccessId = null;
			LcscGoodsUrl = null;
			MinImage = null;
			SecondSortAccessId = null;
			StockCount = 0;
		}

		public override string ToString()
		{
			return $"{ComponentCode} ({ComponentTypeEn}) : {Describe}";
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
