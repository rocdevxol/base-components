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
	public class Product : INotifyPropertyChanged
	{
		#region private variables
		private int productId;
		private string productCode;
		private string productModel;
		private int? parentCatalogId;
		private string parentCatalogCode;
		private string parentCatalogName;
		private int? catalogId;
		private string catalogCode;
		private string catalogName;
		private int? brandId;
		private string brandNameEn;
		private string encapStandard;
		private bool? productLibraryType;
		private int? split;
		private string productUnit;
		private string minPacketUnit;
		private int? minBuyNumber;
		private int? maxBuyNumber;
		private int? minPacketNumber;
		private bool? isEnvironment;
		private bool? allHotLevel;      // ?
		private bool? isHot;
		private bool? isPreSale;
		private bool? isOnSale;
		private bool? isDiscount;
		private bool? isReel;
		private int? reelPrice;         // ?
		private double? dollarLadderPrice;
		private int? stockNumber;
		private int? stockSz;
		private int? stockJs;
		private string productLadderPrice;
		private string ladderDiscountRate;
		private List<ProductPrice> productPriceList;
		private List<string> productImages;
		private string productImageUrl;
		private string productImageUrlBig;
		private bool? productModelHighlight;            //?
		private bool? productCodeHighlight;            //?
		private string smtAloneNumberSz;
		private string smtAloneNumberJs;
		private string pdfUrl;
		private string productDescEn;
		private string productIntroEn;
		private List<ParamVO> paramVOList;
		private string productArrange;
		private double? productWeight;
		private double? foreignWeight;
		private bool? isForeignOnSale;
		private bool? isAutoOffsale;            //?
		private bool? isHasBattery;
		private string pdfLinkUrl;
		private string title;
		private double? weight;
		private int? brandCategory;
		private List<Video> videos;
		private List<ProductRecommend> recommend;
		private bool? isFavorite;
		private int? subscribeQuantity;
		private List<ProductRecommend> recommendRight;
		#endregion

		#region product variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productId")]
		public int ProductId
		{
			get => productId;
			set
			{
				if (productId != value)
				{
					productId = value;
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
		[JsonProperty("parentCatalogId")]
		public int? ParentCatalogId
		{
			get => parentCatalogId;
			set
			{
				if (parentCatalogId != value)
				{
					parentCatalogId = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("parentCatalogCode")]
		public string ParentCatalogCode
		{
			get => parentCatalogCode;
			set
			{
				if (parentCatalogCode != value)
				{
					parentCatalogCode = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("parentCatalogName")]
		public string ParentCatalogName
		{
			get => parentCatalogName;
			set
			{
				if (parentCatalogName != value)
				{
					parentCatalogName = value;
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
		[JsonProperty("catalogCode")]
		public string CatalogCode
		{
			get => catalogCode;
			set
			{
				if (catalogCode != value)
				{
					catalogCode = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("catalogName")]
		public string CatalogName
		{
			get => catalogName;
			set
			{
				if (catalogName != value)
				{
					catalogName = value;
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
		[JsonProperty("encapStandard")]
		public string EncapStandard
		{
			get => encapStandard;
			set
			{
				if (encapStandard != value)
				{
					encapStandard = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productLibraryType")]
		public bool? ProductLibraryType
		{
			get => productLibraryType;
			set
			{
				if (productLibraryType != value)
				{
					productLibraryType = value;
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
		[JsonProperty("productUnit")]
		public string ProductUnit
		{
			get => productUnit;
			set
			{
				if (productUnit != value)
				{
					productUnit = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("minPacketUnit")]
		public string MinPacketUnit
		{
			get => minPacketUnit;
			set
			{
				if (minPacketUnit != value)
				{
					minPacketUnit = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("minBuyNumber")]
		public int? MinBuyNumber
		{
			get => minBuyNumber;
			set
			{
				if (minBuyNumber != value)
				{
					minBuyNumber = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("maxBuyNumber")]
		public int? MaxBuyNumber
		{
			get => maxBuyNumber;
			set
			{
				if (maxBuyNumber != value)
				{
					maxBuyNumber = value;
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
		[JsonProperty("isEnvironment")]
		public bool? IsEnvironment
		{
			get => isEnvironment;
			set
			{
				if (isEnvironment != value)
				{
					isEnvironment = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("allHotLevel")]
		public bool? AllHotLevel      // ?
		{
			get => allHotLevel;
			set
			{
				if (allHotLevel != value)
				{
					allHotLevel = value;
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
		[JsonProperty("isPreSale")]
		public bool? IsPreSale
		{
			get => isPreSale;
			set
			{
				if (isPreSale != value)
				{
					isPreSale = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isOnSale")]
		public bool? IsOnSale
		{
			get => isOnSale;
			set
			{
				if (isOnSale != value)
				{
					isOnSale = value;
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
		[JsonProperty("isReel")]
		public bool? IsReel
		{
			get => isReel;
			set
			{
				if (isReel != value)
				{
					isReel = value;
					NotifyPropertyChanged();
				}
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("reelPrice")]
		public int? ReelPrice         // ?
		{
			get => reelPrice;
			set
			{
				if (reelPrice != value)
				{
					reelPrice = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("dollarLadderPrice")]
		public double? DollarLadderPrice
		{
			get => dollarLadderPrice;
			set
			{
				if (dollarLadderPrice != value)
				{
					dollarLadderPrice = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("stockNumber")]
		public int? StockNumber
		{
			get => stockNumber;
			set
			{
				if (stockNumber != value)
				{
					stockNumber = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("stockSz")]
		public int? StockSz
		{
			get => stockSz;
			set
			{
				if (stockSz != value)
				{
					stockSz = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("stockJs")]
		public int? StockJs
		{
			get => stockJs;
			set
			{
				if (stockJs != value)
				{
					stockJs = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productLadderPrice")]
		public string ProductLadderPrice
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
		[JsonProperty("productPriceList")]
		public List<ProductPrice> ProductPriceList
		{
			get => productPriceList;
			set
			{
				if (productPriceList != value)
				{
					productPriceList = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productImages")]
		public List<string> ProductImages
		{
			get => productImages;
			set
			{
				if (productImages != value)
				{
					productImages = value;
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
		[JsonProperty("productImageUrlBig")]
		public string ProductImageUrlBig
		{
			get => productImageUrlBig;
			set
			{
				if (productImageUrlBig != value)
				{
					productImageUrlBig = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productModelHighlight")]
		public bool? ProductModelHighlight
		{
			get => productModelHighlight;
			set
			{
				if (productModelHighlight != value)
				{
					productModelHighlight = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productCodeHighlight")]
		public bool? ProductCodeHighlight
		{
			get => productCodeHighlight;
			set
			{
				if (productCodeHighlight != value)
				{
					productCodeHighlight = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("smtAloneNumberSz")]
		public string SmtAloneNumberSz
		{
			get => smtAloneNumberSz;
			set
			{
				if (smtAloneNumberSz != value)
				{
					smtAloneNumberSz = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("smtAloneNumberJs")]
		public string SmtAloneNumberJs
		{
			get => smtAloneNumberJs;
			set
			{
				if (smtAloneNumberJs != value)
				{
					smtAloneNumberJs = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("pdfUrl")]
		public string PdfUrl
		{
			get => pdfUrl;
			set
			{
				if (pdfUrl != value)
				{
					pdfUrl = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productDescEn")]
		public string ProductDescEn
		{
			get => productDescEn;
			set
			{
				if (productDescEn != value)
				{
					productDescEn = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productIntroEn")]
		public string ProductIntroEn
		{
			get => productIntroEn;
			set
			{
				if (productIntroEn != value)
				{
					productIntroEn = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("paramVOList")]
		public List<ParamVO> ParamVOList
		{
			get => paramVOList;
			set
			{
				if (paramVOList != value)
				{
					paramVOList = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productArrange")]
		public string ProductArrange
		{
			get => productArrange;
			set
			{
				if (productArrange != value)
				{
					productArrange = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productWeight")]
		public double? ProductWeight
		{
			get => productWeight;
			set
			{
				if (productWeight != value)
				{
					productWeight = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("foreignWeight")]
		public double? ForeignWeight
		{
			get => foreignWeight;
			set
			{
				if (foreignWeight != value)
				{
					foreignWeight = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isForeignOnSale")]
		public bool? IsForeignOnSale
		{
			get => isForeignOnSale;
			set
			{
				if (isForeignOnSale != value)
				{
					isForeignOnSale = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isAutoOffsale")]
		public bool? IsAutoOffsale
		{
			get => isAutoOffsale;
			set
			{
				if (isAutoOffsale != value)
				{
					isAutoOffsale = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isHasBattery")]
		public bool? IsHasBattery
		{
			get => isHasBattery;
			set
			{
				if (isHasBattery != value)
				{
					isHasBattery = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("pdfLinkUrl")]
		public string PdfLinkUrl
		{
			get => pdfLinkUrl;
			set
			{
				if (pdfLinkUrl != value)
				{
					pdfLinkUrl = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("title")]
		public string Title
		{
			get => title;
			set
			{
				if (title != value)
				{
					title = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("weight")]
		public double? Weight
		{
			get => weight;
			set
			{
				if (weight != value)
				{
					weight = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("brandCategory")]
		public int? BrandCategory
		{
			get => brandCategory;
			set
			{
				if (brandCategory != value)
				{
					brandCategory = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("videos")]
		public List<Video> Videos
		{
			get => videos;
			set
			{
				if (videos != value)
				{
					videos = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("recommend")]
		public List<ProductRecommend> Recommend
		{
			get => recommend;
			set
			{
				if (recommend != value)
				{
					recommend = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isFavorite")]
		public bool? IsFavorite
		{
			get => isFavorite;
			set
			{
				if (isFavorite != value)
				{
					isFavorite = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("subscribeQuantity")]
		public int? SubscribeQuantity
		{
			get => subscribeQuantity;
			set
			{
				if (subscribeQuantity != value)
				{
					subscribeQuantity = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("recommendRight")]
		public List<ProductRecommend> RecommendRight
		{
			get => recommendRight;
			set
			{
				if (recommendRight != value)
				{
					recommendRight = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public Product()
		{
			ProductId = 0;
			ProductCode = null;
			ProductModel = null;
			ParentCatalogId = null;
			ParentCatalogCode = null;
			ParentCatalogName = null;
			CatalogId = null;
			CatalogCode = null;
			CatalogName = null;
			BrandId = null;
			BrandNameEn = null;
			EncapStandard = null;
			ProductLibraryType = null;
			Split = null;
			ProductUnit = null;
			MinPacketUnit = null;
			MinBuyNumber = null;
			MaxBuyNumber = null;
			MinPacketNumber = null;
			IsEnvironment = null;
			AllHotLevel = null;      // ?
			IsHot = null;
			IsPreSale = null;
			IsOnSale = null;
			IsDiscount = null;
			IsReel = null;
			ReelPrice = null;         // ?
			DollarLadderPrice = null;
			StockNumber = null;
			StockSz = null;
			StockJs = null;
			ProductLadderPrice = null;
			LadderDiscountRate = null;
			ProductPriceList = new List<ProductPrice>();
			ProductImages = new List<string>();
			ProductImageUrl = null;
			ProductImageUrlBig = null;
			ProductModelHighlight = null;            //?
			ProductCodeHighlight = null;            //?
			SmtAloneNumberSz = null;
			SmtAloneNumberJs = null;
			PdfUrl = null;
			ProductDescEn = null;
			ProductIntroEn = null;
			ParamVOList = new List<ParamVO>();
			ProductArrange = null;
			ProductWeight = null;
			ForeignWeight = null;
			IsForeignOnSale = null;
			IsAutoOffsale = null;            //?
			IsHasBattery = null;
			PdfLinkUrl = null;
			Title = null;
			Weight = null;
			BrandCategory = null;
			Videos = new List<Video>();
			Recommend = new List<ProductRecommend>();
			IsFavorite = null;
			SubscribeQuantity = null;
			RecommendRight = new List<ProductRecommend>();
		}

		public override string ToString()
		{
			if (string.IsNullOrEmpty(Title))
				return $"{ProductCode}: {BrandNameEn} {ProductModel}";
			else
				return $"{ProductCode}: {Title}";
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
