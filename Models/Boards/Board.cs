using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Boards
{
	/// <summary>
	/// Печатные платы применяемые в проектах
	/// </summary>
	[Serializable]
	public class Board : INotifyPropertyChanged, ICloneable
	{
		private string name;
		private string description;
		private string decimalNumber;// децимальный номер
		private int count;
		private bool enableToCalc;
		private ObservableCollection<ITreeProject> parts;

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
		public ObservableCollection<ITreeProject> Parts
		{
			get => parts;
			set
			{
				if (parts != value)
				{
					parts = value;
					NotifyPropertyChanged();
				}
			}
		}

		public Board(string name, string description)
		{
			Name = name;
			Description = description;
			DecimalNumber = string.Empty;
			Count = 1;
			EnableToCalc = true;

			Parts = new ObservableCollection<ITreeProject>
			{
				new Components.ComponentList(),
				new Mechanical.MechanicalList(),
				new Gerber.Gerber()
			};
		}

		public Board() : this(string.Empty, string.Empty)
		{

		}

		public Board(BoardJson json)
		{
			Name = json.Name;
			Description = json.Description;
			DecimalNumber = json.DecimalNumber;
			Count = json.Count;
			EnableToCalc = json.EnableToCalc;

			Parts = new ObservableCollection<ITreeProject>
			{
				(Components.ComponentList)json.GetComponentList().Clone(),
				(Mechanical.MechanicalList)json.GetMechanicalList().Clone(),
				(Gerber.Gerber)json.GetGerberList().Clone()
			};
		}

		/// <summary>
		/// Получить перечень компонентов
		/// </summary>
		/// <returns>Перечень компонентов</returns>
		public Components.ComponentList GetComponentList()
		{
			return Parts[0] as Components.ComponentList;
		}

		/// <summary>
		/// Получить перечень механических компонентов в печатной плате
		/// </summary>
		/// <returns>Перечень механических компонентов</returns>
		public Mechanical.MechanicalList GetMechanicalList()
		{
			return Parts[1] as Mechanical.MechanicalList;
		}

		/// <summary>
		/// Получить список Gerber файлов
		/// </summary>
		/// <returns>Список Gerber файлов</returns>
		public Gerber.Gerber GetGerberList()
		{
			return Parts[2] as Gerber.Gerber;
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
			if (DecimalNumber == string.Empty)
			{
				return $"{Name} [{Count}]";
			}
			else
			{
				return $"{Name} (Д/Н: {DecimalNumber}) [{Count}]";
			}
		}

		public object Clone()
		{
			Board board = new Board(Name, Description)
			{
				Count = Count,
				Description = Description,
				DecimalNumber = DecimalNumber,
				EnableToCalc = EnableToCalc
			};

			board.GetComponentList().Components = new ObservableCollection<Components.Component>(GetComponentList().Components);
			board.GetMechanicalList().MechanicalComps = new ObservableCollection<Mechanical.MechanicalComp>(GetMechanicalList().MechanicalComps);

			return board;
		}
	}
}
