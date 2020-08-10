using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Components
{
	/// <summary>
	/// Фирма разработчик компонента
	/// </summary>
	[Serializable]
	public class Developer : INotifyPropertyChanged, ICloneable
	{
		private string name;

		/// <summary>
		/// Название фирмы производителя
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
		/// Инициализация фирмы производителя
		/// </summary>
		/// <param name="name">Наименование фирмы производителя</param>
		public Developer(string name)
		{
			Name = name;
		}
		
		public Developer() : this(string.Empty)
		{

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
			return $"{Name}";
		}

		public object Clone()
		{
			Developer developer = new Developer(Name);
			return developer;
		}
	}
}
