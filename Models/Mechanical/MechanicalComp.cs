using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Mechanical
{
	/// <summary>
	/// Информация о механическом компоненте
	/// </summary>
	[Serializable]
	public class MechanicalComp : INotifyPropertyChanged, ICloneable
	{
		private string name;
		private string description;
		private int count;

		/// <summary>
		/// Наименование механического компонента
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
		/// Описание механического компонента
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
		/// Количество механических компонентов
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
			return $"{Description} [{Count}]";
		}

		public object Clone()
		{
			MechanicalComp comp = new MechanicalComp
			{
				Name = Name,
				Description = Description,
				Count = Count
			};

			return comp;
		}
	}
}
