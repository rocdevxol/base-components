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
	public class ComponentPrice : INotifyPropertyChanged
	{
		#region private variables
		private int endNumber;
		private double productPrice;
		private int startNumber;

		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("endNumber")]
		public int EndNumber
		{
			get => endNumber;
			set
			{
				if (endNumber != value)
				{
					endNumber = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("productPrice")]
		public double ProductPrice
		{
			get => productPrice;
			set
			{
				if (productPrice != value)
				{
					productPrice = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("startNumber")]
		public int StartNumber
		{
			get => startNumber;
			set
			{
				if (startNumber != value)
				{
					startNumber = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public ComponentPrice()
		{
			EndNumber = 0;
			ProductPrice = 0;
			StartNumber = 0;
		}

		public override string ToString()
		{
			return $"{StartNumber} -> {EndNumber}: {ProductPrice}";
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
