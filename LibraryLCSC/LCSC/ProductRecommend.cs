using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LibraryLCSC.LCSC
{
	[Serializable]
	public class ProductRecommend : INotifyPropertyChanged
	{
		#region private variables
		private double? size;		// ?
		private string productCode;
		private string catalogNameEn;
		private string productImageUrl;
		private string brandNameEn;
		private string productModel;
		private int? productLadder;
		private double? productLadderPrice;
		private int? split;
		private string ladderDiscountRate;
		private int? minPacketNumber;
		private int? brandId;
		private int? catalogId;
		private bool? isDiscount;
		private bool? isHot;
		private double? currencyPrice;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("size")]
		public double? Size
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
		[JsonProperty("productCode")]
		public string ProductCode
		{
			get => productCode;
			set
			{
				if (productCode != value)
				{
					productCode = value;
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
		[JsonProperty("productImageUrl")]
		public string ProductImageUrl
		{
			get => productImageUrl;
			set
			{
				if (productImageUrl != value)
				{
					productImageUrl = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("brandNameEn")]
		public string BrandNameEn
		{
			get => brandNameEn;
			set
			{
				if (brandNameEn != value)
				{
					brandNameEn = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productModel")]
		public string ProductModel
		{
			get => productModel;
			set
			{
				if (productModel != value)
				{
					productModel = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productLadder")]
		public int? ProductLadder
		{
			get => productLadder;
			set
			{
				if (productLadder != value)
				{
					productLadder = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productLadderPrice")]
		public double? ProductLadderPrice
		{
			get => productLadderPrice;
			set
			{
				if (productLadderPrice != value)
				{
					productLadderPrice = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("split")]
		public int? Split
		{
			get => split;
			set
			{
				if (split != value)
				{
					split = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("ladderDiscountRate")]
		public string LadderDiscountRate
		{
			get => ladderDiscountRate;
			set
			{
				if (ladderDiscountRate != value)
				{
					ladderDiscountRate = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("minPacketNumber")]
		public int? MinPacketNumber
		{
			get => minPacketNumber;
			set
			{
				if (minPacketNumber != value)
				{
					minPacketNumber = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("brandId")]
		public int? BrandId
		{
			get => brandId;
			set
			{
				if (brandId != value)
				{
					brandId = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("catalogId")]
		public int? CatalogId
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
		[JsonProperty("isDiscount")]
		public bool? IsDiscount
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
		[JsonProperty("isHot")]
		public bool? IsHot
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
		[JsonProperty("currencyPrice")]
		public double? CurrencyPrice
		{
			get => currencyPrice;
			set
			{
				if (currencyPrice != value)
				{
					currencyPrice = value;
					NotifyPropertyChanged();
				}
			}
		}

		#endregion

		public ProductRecommend()
		{
			Size = null;       // ?
			ProductCode = null;
			CatalogNameEn = null;
			ProductImageUrl = null;
			BrandNameEn = null;
			ProductModel = null;
			ProductLadder = null;
			ProductLadderPrice = null;
			Split = null;
			LadderDiscountRate = null;
			MinPacketNumber = null;
			BrandId = null;
			CatalogId = null;
			IsDiscount = null;
			IsHot = null;
			CurrencyPrice = null;
		}

		public override string ToString()
		{
			return $"{ProductCode}: {BrandNameEn} {ProductModel}";
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
