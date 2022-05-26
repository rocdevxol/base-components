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
	public class ProductPrice : INotifyPropertyChanged
	{
		#region private variables
		private int? ladder;
		private double? usdPrice;
		private double? currencyPrice;
		private string currencySymbol;
		private double? discountRate;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("ladder")]
		public int? Ladder
		{
			get => ladder;
			set
			{
				if (ladder != value)
				{
					ladder = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("usdPrice")]
		public double? UsdPrice
		{
			get => usdPrice;
			set
			{
				if (usdPrice != value)
				{
					usdPrice = value;
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

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("currencySymbol")]
		public string CurrencySymbol
		{
			get => currencySymbol;
			set
			{
				if (currencySymbol != value)
				{
					currencySymbol = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("discountRate")]
		public double? DiscountRate
		{
			get => discountRate;
			set
			{
				if (discountRate != value)
				{
					discountRate = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public ProductPrice()
		{
			Ladder = null;
			UsdPrice = null;
			CurrencyPrice = null;
			CurrencySymbol = null;
			DiscountRate = null;
		}

		public override string ToString()
		{
			if (DiscountRate != null)
				return $"{Ladder}: {CurrencyPrice}, Discount Rate: {DiscountRate}";
			else
				return $"{Ladder}: {CurrencyPrice}";
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
