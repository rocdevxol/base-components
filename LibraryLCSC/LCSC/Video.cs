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
	public class Video : INotifyPropertyChanged
	{
		#region private variables
		private string url;
		private string cover;
		private string detail;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("url")]
		public string Url
		{
			get => url;
			set
			{
				if (url != value)
				{
					url = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("cover")]
		public string Cover
		{
			get => cover;
			set
			{
				if (cover != value)
				{
					cover = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("detail")]
		public string Detail
		{
			get => detail;
			set
			{
				if (detail != value)
				{
					detail = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		public Video()
		{
			Url = null;
			Cover = null;
			Detail = null;
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
