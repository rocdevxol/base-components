using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Boards
{
	[Serializable]
	public class BoardJson : INotifyPropertyChanged, ICloneable
	{
		private string name;
		private string description;
		private string decimalNumber;// децимальный номер
		private int count;
		private bool enableToCalc;
		private Components.ComponentList componentLists;
		private Mechanical.MechanicalList mechanicalLists;

		/// <summary>
		/// Наименование печатной платы
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
		/// Описание печатной платы
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
		/// Децимальный номер на плату
		/// </summary>
		public string DecimalNumber
		{
			get => decimalNumber;
			set
			{
				if (decimalNumber != value.Trim())
				{
					decimalNumber = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Количество печатных плат в проекте
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
		/// Брать плату в расчет
		/// </summary>
		public bool EnableToCalc
		{
			get => enableToCalc;
			set
			{
				if (enableToCalc != value)
				{
					enableToCalc = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// Перечень компонентов в печатной плате
		/// </summary>
		public Components.ComponentList ComponentLists
		{
			get => componentLists;
			set
			{
				if (componentLists != value)
				{
					componentLists = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Перечень механических компонентов в печатной плате
		/// </summary>
		public Mechanical.MechanicalList MechanicalLists
		{
			get => mechanicalLists;
			set
			{
				if (mechanicalLists != value)
				{
					mechanicalLists = value;
					NotifyPropertyChanged();
				}
			}
		}

		public BoardJson(Board board)
		{
			Name = board.Name;
			Description = board.Description;
			DecimalNumber = board.DecimalNumber;
			Count = board.Count;
			EnableToCalc = board.EnableToCalc;

			ComponentLists = (Components.ComponentList)board.GetComponentList().Clone();
			MechanicalLists = (Mechanical.MechanicalList)board.GetMechanicalList().Clone();
		}

		public BoardJson()
		{
			Name = string.Empty;
			Description = string.Empty;
			DecimalNumber = string.Empty;
			Count = 1;
			EnableToCalc = true;

			ComponentLists = new Components.ComponentList();
			MechanicalLists = new Mechanical.MechanicalList();
		}

		/// <summary>
		/// Получить перечень компонентов
		/// </summary>
		/// <returns>Перечень компонентов</returns>
		public Components.ComponentList GetComponentList()
		{
			return ComponentLists;
		}

		/// <summary>
		/// Получить перечень механических компонентов в печатной плате
		/// </summary>
		/// <returns>Перечень механических компонентов</returns>
		public Mechanical.MechanicalList GetMechanicalList()
		{
			return MechanicalLists;
		}

		/// <summary>
		/// Количество компонентов в печатной плате 
		/// </summary>
		public int GetCountComponents()
		{
			return GetComponentList().Components.Count + GetMechanicalList().MechanicalComps.Count;
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
			if (DecimalNumber == String.Empty)
				return $"{Name} [{Count}]";
			else
				return $"{Name} (Д/Н: {DecimalNumber}) [{Count}]";
		}

		public object Clone()
		{
			BoardJson board = new BoardJson
			{
				Name = Name,
				Description = Description,
				DecimalNumber = DecimalNumber,
				Count = Count,
				EnableToCalc = EnableToCalc
			};

			board.GetComponentList().Components = new ObservableCollection<Components.Component>(GetComponentList().Components);
			board.GetMechanicalList().MechanicalComps = new ObservableCollection<Mechanical.MechanicalComp>(GetMechanicalList().MechanicalComps);

			return board;
		}
	}

}
