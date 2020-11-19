using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Components
{
	/// <summary>
	/// Покупное наименование компонента
	/// </summary>
	[Serializable]
	public class SubComponent : INotifyPropertyChanged, IComparable<SubComponent>, ICloneable
	{
		private string name;
		private Package package;
		private Developer developer;
		private Distributor distributor;
		private double price;

		/// <summary>
		/// Покупное наименование компонента
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
		/// Корпус компонента
		/// </summary>
		public Package Package
		{
			get => package;
			set
			{
				if (package != value)
				{
					package = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Фирма разработчик компонента
		/// </summary>
		public Developer Developer
		{
			get => developer;
			set
			{
				if (developer != value)
				{
					developer = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Фирма поставщик компонента
		/// </summary>
		public Distributor Distributor
		{
			get => distributor;
			set
			{
				if (distributor != value)
				{
					distributor = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Стоимость компонентов
		/// </summary>
		public double Price
		{
			get => price;
			set
			{
				if (price != value)
				{
					price = value;
					NotifyPropertyChanged();
				}
			}
		}

		public SubComponent(string name)
		{
			Name = name;
			Developer = new Developer(string.Empty);
			Distributor = new Distributor(string.Empty);
			Package = new Package(string.Empty);
			Price = 0;
		}

		public SubComponent() : this(string.Empty)
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
			return $"[{Name} : Корпус={Package}]";
		}

		public int CompareTo(SubComponent obj)
		{
			int result = Name.CompareTo(obj.Name);
			result += Package.CompareTo(obj.Package);
			result += Price.CompareTo(obj.Price);
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

			if (!string.IsNullOrEmpty(Name)) result = true;
			if (Package.IsFulled() == true) result = true;

			return result;
		}

		public object Clone()
		{
			SubComponent sub = new SubComponent(Name)
			{
				Package = (Package)Package?.Clone(),
				Developer = (Developer)Developer?.Clone(),
				Distributor = (Distributor)Distributor?.Clone(),
				Price = Price
			};
			return sub;
		}

		#region Поиск элементов в списке
		/// <summary>
		/// Поиск элемента, содержащий строку
		/// </summary>
		/// <param name="search">строка которую ищем, если первый символ = '@', то ищем полное совпадение этой строки</param>
		/// <returns></returns>
		public bool FindElement(string search)
		{
			bool result;
			if (search[0] == '@')
				result = FindFullContainsElement(search);
			else
				result = FindContainsElement(search);
			return result;
		}

		/// <summary>
		/// Поиск по полному совпадению элемента со строкой
		/// </summary>
		/// <param name="search">строка которую ищем</param>
		/// <returns></returns>
		private bool FindFullContainsElement(string search)
		{
			if (Name.Equals(search)) return true;
			else if (Package.Name.Equals(search)) return true;
			else if (Developer.Name.Equals(search)) return true;
			else if (Distributor.Name.Equals(search)) return true;
			return false;
		}

		/// <summary>
		/// Поиск по частичному совпадению элемента со строкой
		/// </summary>
		/// <param name="search">строка которую ищем</param>
		/// <returns></returns>
		private bool FindContainsElement(string search)
		{
			if (Name.Contains(search)) return true;
			else if (Package.Name.Contains(search)) return true;
			else if (Developer.Name.Contains(search)) return true;
			else if (Distributor.Name.Contains(search)) return true;
			return false;
		}
		#endregion

	}
}
