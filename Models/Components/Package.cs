using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Components
{
	/// <summary>
	/// Корпус компонента
	/// </summary>
	[Serializable]

	public class Package : INotifyPropertyChanged, IComparable<Package>, ICloneable
	{
		private string name;
		private int numPins;
		private PackageType packageType;
		private List<Package> packages;

		/// <summary>
		/// Название корпуса компонента
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
		/// Колчиество выводов на корпусе
		/// </summary>
		public int NumPins
		{
			get => numPins;
			set
			{
				if (numPins != value)
				{
					numPins = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Тип монтажа
		/// </summary>
		public PackageType PackageType
		{
			get => packageType;
			set
			{
				if (packageType != value)
				{
					packageType = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// Перечень корпусов заменяемых этим корпусом
		/// </summary>
		public List<Package> Packages
		{
			get => packages;
			set
			{
				if (packages != value)
				{
					packages = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Инициализация корпуса
		/// </summary>
		/// <param name="name">Название корпуса</param>
		public Package(string name)
		{
			Name = name;
			PackageType = PackageType.Unknown;
			NumPins = 1;
			Packages = new List<Package>();
		}

		/// <summary>
		/// Используется для установки зависимости корпусов
		/// </summary>
		/// <param name="p"></param>
		public Package(Package p)
		{
			Name = p.Name;
			NumPins = p.NumPins;

			Packages = new List<Package>();
		}

		public Package() : this(string.Empty)
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
			return $"{Name}:{PackageType}:{NumPins}";
		}

		public int CompareTo(Package obj)
		{
			int result = Name.CompareTo(obj.Name);
			result += NumPins.CompareTo(obj.NumPins);
			return result;
		}

		/// <summary>
		/// Проверка на пустое значение или нулевое составляющей компонента
		/// </summary>
		/// <returns>true - составляющая компонента заполнена, false - компонент пустой, null - отсутствует</returns>
		public bool? IsFulled()
		{
			bool result = false;
			if (this == null) return null;

			if (Name != string.Empty) result = true;

			return result;
		}


		public object Clone()
		{
			Package package = new Package(Name)
			{
				NumPins = NumPins,
				PackageType = PackageType
			};
			

			foreach (Package p in Packages)
				package.Packages.Add((Package)p.Clone());
			return package;
		}
	}
}
