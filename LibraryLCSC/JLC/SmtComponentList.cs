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
	public class SmtComponentList : INotifyPropertyChanged
	{
		#region private variables
		private int code;
		private List<IDataSmtComponentList> data;
		private string message;
		#endregion

		#region public variables
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("code")]
		public int Code
		{
			get => code;
			set
			{
				if (code != value)
				{
					code = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("data")]
		public List<IDataSmtComponentList> Data
		{
			get => data;
			set
			{
				if (data != value)
				{
					data = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("message")]
		public string Message
		{
			get => message;
			set
			{
				if (message != value)
				{
					message = value;
					NotifyPropertyChanged();
				}
			}
		}

		#endregion

		public SmtComponentList()
		{
			Code = 0;
			Data = new List<IDataSmtComponentList>();
			Data.Add(new ComponentInfo());
			Data.Add(new ChildSort());
			Message = null;
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
