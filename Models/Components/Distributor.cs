using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Components
{
	/// <summary>
	/// Фирма поставщик компонентов
	/// </summary>
	[Serializable]
	public class Distributor : INotifyPropertyChanged, ICloneable
	{
		private string name;

		/// <summary>
		/// Название фирмы поставщика
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
		/// Инициализация фирмы поставщика
		/// </summary>
		/// <param name="name">Наименование фирмы поставщика</param>
		public Distributor(string name)
		{
			Name = name;
		}
		
		public Distributor() : this(string.Empty)
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
			Distributor distributor = new Distributor(Name);
			return distributor;

		}
	}
}
