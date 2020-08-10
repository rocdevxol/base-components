using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Projects
{
	[Serializable]
	public class ProjectJson : INotifyPropertyChanged, ICloneable
	{
		private string name;
		private string projectFolder;
		private Boards.BoardJsonList boardLists;
		private Mechanical.MechanicalList mechanicalLists;
		private Wires.WireList wireLists;

		/// <summary>
		/// Наименование проекта
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
		/// Директория проекта
		/// </summary>
		public string ProjectFolder
		{
			get => projectFolder;
			set
			{
				if (projectFolder != value.Trim())
				{
					projectFolder = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// Перечень плат
		/// </summary>
		public Boards.BoardJsonList BoardLists
		{
			get => boardLists;
			set
			{
				if (boardLists != value)
				{
					boardLists = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Перечень механических компонентов
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

		/// <summary>
		/// Перечень механических компонентов
		/// </summary>
		public Wires.WireList WireLists
		{
			get => wireLists;
			set
			{
				if (wireLists != value)
				{
					wireLists = value;
					NotifyPropertyChanged();
				}
			}
		}


		/// <summary>
		/// ИНициализация проекта
		/// </summary>
		/// <param name="name">Наименование проекта</param>
		public ProjectJson(Project project)
		{
			Name = project.Name;
			ProjectFolder = string.Empty;

			BoardLists = new Boards.BoardJsonList((Boards.BoardList)project.GetBoardList().Clone());
			MechanicalLists = (Mechanical.MechanicalList)project.GetMechanicalList().Clone();
			WireLists = (Wires.WireList)project.GetWireList().Clone();
		}

		public ProjectJson()
		{
			Name = string.Empty;
			ProjectFolder = string.Empty;

			BoardLists = new Boards.BoardJsonList();
			MechanicalLists = new Mechanical.MechanicalList();
			WireLists = new Wires.WireList();
		}

		/// <summary>
		/// Получить перечень плат в проекте
		/// </summary>
		/// <returns>Перечень плат</returns>
		public Boards.BoardJsonList GetBoardList()
		{
			return BoardLists;
		}

		/// <summary>
		/// Получить перечень механических компонентов в проекте
		/// </summary>
		/// <returns>Перечень механических компонентов</returns>
		public Mechanical.MechanicalList GetMechanicalList()
		{
			return MechanicalLists;
		}

		/// <summary>
		/// Получить перечень проводов
		/// </summary>
		/// <returns>Перечень проводов</returns>
		public Wires.WireList GetWireList()
		{
			return WireLists;
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
			ProjectJson p = new ProjectJson
			{
				Name = Name
			};

			p.GetBoardList().BoardJsons = GetBoardList().BoardJsons;
			p.GetMechanicalList().MechanicalComps = GetMechanicalList().MechanicalComps;
			p.GetWireList().Wires = GetWireList().Wires;

			return p;
		}
	}
}
