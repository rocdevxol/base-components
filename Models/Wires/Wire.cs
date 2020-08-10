using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Wires
{
	[Serializable]
	public class Wire : INotifyPropertyChanged, ICloneable
	{
		private string name;
		private string description;
		private double length;
		private double crosssection;
		private int count;
		private string function;
		private string ending1;
		private string ending2;

		/// <summary>
		/// Наименование провода
		/// </summary>
		public string Name
		{
			get => name;
			set
			{
				if (name != value.Trim())
				{
					name = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Описание провода
		/// </summary>
		public string Description
		{
			get => description;
			set
			{
				if (description != value.Trim())
				{
					description = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Длина провода
		/// </summary>
		public double Length
		{
			get => length;
			set
			{
				if (length != value)
				{
					length = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Сечение провода
		/// </summary>
		public double CrossSection
		{
			get => crosssection;
			set
			{
				if (crosssection != value)
				{
					crosssection = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Количество проводов в системе
		/// </summary>
		public int Count
		{
			get => count;
			set
			{
				if (count != value)
				{
					count = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Назначение провода
		/// </summary>
		public string Function
		{
			get => function;
			set
			{
				if (function != value.Trim())
				{
					function = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Концевик на проводе с одной стороны
		/// </summary>
		public string Ending1
		{
			get => ending1;
			set
			{
				if (ending1 != value.Trim())
				{
					ending1 = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Концевик на проводе с другой стороны
		/// </summary>
		public string Ending2
		{
			get => ending2;
			set
			{
				if (ending2 != value.Trim())
				{
					ending2 = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		#region Events
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

		public override string ToString()
		{
			return $"{Description}, {CrossSection} [{Length} : {Count}]";
		}

		public object Clone()
		{
			Wire wire = new Wire
			{
				Name = Name,
				Description = Description,
				Length = Length,
				CrossSection = CrossSection,
				Count = Count,
				Function = Function,
				Ending1 = Ending1,
				Ending2 = Ending2
			};

			return wire;
		}
	}
}
