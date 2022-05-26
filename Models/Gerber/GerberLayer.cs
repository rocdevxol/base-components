using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Models.Gerber
{
	[Serializable]
	public class GerberLayer : INotifyPropertyChanged, ICloneable
	{
		#region variables

		private string name;
		private string content;
		private string extension;

		/// <summary>
		/// Название слоя
		/// </summary>
		public string Name
		{
			get => name;
			set
			{
				if (name != value)
				{
					name = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Содержимое gerber файла
		/// </summary>
		public string Content
		{
			get => content;
			set
			{
				if (content != value)
				{
					content = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Расширение файла
		/// </summary>
		public string Extension
		{
			get => extension;
			set
			{
				if (extension != value)
				{
					extension = value;
					NotifyPropertyChanged();
				}
			}
		}

		#endregion

		public GerberLayer(string name, string extension)
		{
			Name = name;
			Content = string.Empty;
			Extension = extension;
		}

		public GerberLayer() : this(string.Empty, string.Empty) { }

		#region Events
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

		public object Clone()
		{
			GerberLayer layer = new GerberLayer
			{
				Name = Name,
				Content = Content,
				Extension = Extension
			};
			return layer;
		}

		public override string ToString()
		{
			string isContented = Content.Length > 0 ? "+" : "-";
			return $"{Name}, {Extension}, [{isContented}]";
		}
	}
}
