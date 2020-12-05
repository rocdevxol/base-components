using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Models.Components
{
	/// <summary>
	/// Компоненты применяемые в проектах
	/// </summary>
	[Serializable]
	public class Component : INotifyPropertyChanged, IComparable<Component>, ICloneable
	{
		private string refdes;
		private bool soldering;
		private string typeComponent;
		private string description;
		private int count;
		private double price;
		private ObservableCollection<SubComponent> names;

		/// <summary>
		/// Позиционное обозначение компонента
		/// </summary>
		public string RefDes
		{
			get => refdes;
			set
			{
				if (refdes != value.Trim())
				{
					refdes = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Монтаж в печатную плату
		/// </summary>
		public bool Soldering
		{
			get => soldering;
			set
			{
				if (soldering != value)
				{
					soldering = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Тип компонента (резистор, конденсатор, ...)
		/// </summary>
		public string TypeComponent
		{
			get => typeComponent;
			set
			{
				if (value == null) value = string.Empty;
				if (typeComponent != value.Trim())
				{
					typeComponent = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Описание компонента (удобо восприимчивое)
		/// </summary>
		public string Description
		{
			get => description;
			set
			{
				if (value == null) value = string.Empty;
				if (description != value.Trim())
				{
					description = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Количество компонентов в плате
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
		/// Стоимость компонента
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

		/// <summary>
		/// Покупное наименование
		/// </summary>
		public ObservableCollection<SubComponent> Names
		{
			get => names;
			set
			{
				if (names != value)
				{
					names = value;
					NotifyPropertyChanged();
				}
			}
		}

		public Component(string refDes)
		{
			RefDes = refDes;
			Soldering = true;
			Count = 1;
			Price = 0;
			Names = new ObservableCollection<SubComponent>();
			Names.CollectionChanged += Names_CollectionChanged;
		}

		public Component() : this(string.Empty)
		{

		}

		private void Names_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			NotifyPropertyChanged(nameof(Names));
		}

		public void UpdateCount(int addCount)
		{
			Count += addCount;
		}

		public void UpdateRefDes(string refDes)
		{
			RefDes = string.Format($"{RefDes}, {refDes}");
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
			StringBuilder format = new StringBuilder();
			format.Append($"{RefDes} : ");
			format.Append($"Описание=\"{Description}\"\n");
			for (int i = 0; i < Names.Count; i++)
			{
				format.Append(string.Format(" {0}::{1}\n",i, Names[i]));
				if (i != Names.Count - 1)
				{
					format.Append(", ");
				}
			}
			return format.ToString();
		}

		public int CompareTo(Component obj)
		{
			int result = Description.CompareTo(obj.Description);
			if (result != 0) return result;
			if (Names.Count == obj.Names.Count)
			{
				for (int i = 0; i < Names.Count; i++)
				{
					result += Names[i].CompareTo(obj.Names[i]);
				}
			}
			else return -1;
			return result;
		}

		public object Clone()
		{
			Component component = new Component(RefDes)
			{
				Soldering = Soldering,
				TypeComponent = TypeComponent,
				Description = Description,
				Count = Count,
				Price = Price
			};
			foreach (SubComponent sub in Names)
				component.Names.Add((SubComponent)sub.Clone());
			return component;
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
		
			search = search.Trim();
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
			string search1 = search.Substring(1);
			search1 = search1.Trim();
			if (RefDes != null && RefDes.Equals(search1)) return true;
			else if (Description != null && Description.Equals(search1)) return true;
			else if (TypeComponent != null && TypeComponent.Equals(search1)) return true;
			else if (Price.ToString().Equals(search1)) return true;

			foreach (SubComponent subComponent in Names)
				if (subComponent.FindElement(search)) return true;

			return false;
		}

		/// <summary>
		/// Поиск по частичному совпадению элемента со строкой
		/// </summary>
		/// <param name="search">строка которую ищем</param>
		/// <returns></returns>
		private bool FindContainsElement(string search)
		{
			if (RefDes != null && RefDes.Contains(search)) return true;
			else if (Description != null && Description.Contains(search)) return true;
			else if (TypeComponent != null && TypeComponent.Contains(search)) return true;
			else if (Price.ToString().Contains(search)) return true;

			foreach (SubComponent subComponent in Names)
				if (subComponent.FindElement(search)) return true;

			return false;
		}
		#endregion
	}

	public class ComponentRefdesCompare : IComparer<Component>
	{
		private string getStringPart(string input)
		{
			StringBuilder format = new StringBuilder();
			foreach (char c in input)
			{
				if (char.IsLetter(c))
					format.Append(c);
			}
			return format.ToString();
		}

		private int getIntPart(string input)
		{
			StringBuilder format = new StringBuilder();
			foreach (char c in input)
			{
				if (char.IsDigit(c))
					format.Append(c);
			}
			string result = format.ToString();
			return int.Parse(result != String.Empty ? result : "0");
		}


		private int CompareHighPart(string x, string y)
		{
			string strX = getStringPart(x);
			string strY = getStringPart(y);
			int intX = getIntPart(x);
			int intY = getIntPart(y);

			int result = strX.CompareTo(strY);
			if (result != 0) return result;

			result = intX.CompareTo(intY);

			return result;
		}

		private int CompareLowPart(string x, string y)
		{
			int intX = getIntPart(x);
			int intY = getIntPart(y);

			return intX.CompareTo(intY);
		}

		public int Compare(Component x, Component y)
		{
			string[] refDesX = x.RefDes.Split(new char[] { '.' });
			string[] refDesY = y.RefDes.Split(new char[] { '.' });
			
			int result = CompareHighPart(refDesX[0], refDesY[0]);
			if (result != 0) return result;
			if (refDesX.Length == 1 && refDesY.Length == 2)
				result = CompareLowPart("0", refDesY[1]);
			else if (refDesX.Length == 2 && refDesY.Length == 1) 
				result = CompareLowPart(refDesX[1], "0");
			else if (refDesX.Length == 2 && refDesY.Length == 2)
				result = CompareLowPart(refDesX[1], refDesY[1]);
			return result;
		}
	}
}