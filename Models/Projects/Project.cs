using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Projects
{
	/// <summary>
	/// Проект с электрическими платами
	/// </summary>
	[Serializable]
	public class Project : INotifyPropertyChanged, ICloneable
	{
		private string name;
		private string fullNameProject;
		private ObservableCollection<ITreeProject> parts;

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
		/// Полное название проекта
		/// </summary>
		public string FullNameProject
		{
			get => fullNameProject;
			set
			{
				if (fullNameProject != value.Trim())
				{
					fullNameProject = value.Trim();
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Директория проекта
		/// </summary>
		public string ProjectFolder
		{
			get
			{
				System.IO.FileInfo fif = new System.IO.FileInfo(FullNameProject);
				return fif.Directory.FullName;
			}
		}

		/// <summary>
		/// Перечень составляющих
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

		/// <summary>
		/// ИНициализация проекта
		/// </summary>
		/// <param name="name">Наименование проекта</param>
		public Project(string name)
		{
			Name = name;
			FullNameProject = String.Empty;
			Parts = new ObservableCollection<ITreeProject>
			{
				new Boards.BoardList(),
				new Mechanical.MechanicalList(),
				new Wires.WireList()
			};
			/*
			(Parts[0] as Boards.BoardList).Add(new Boards.Board("Печатная плата1"));
			(Parts[1] as Mechanical.MechanicalList).Add(new Mechanical.MechanicalComp());
			(Parts[2] as Wires.WireList).Add(new Wires.Wire());
			*/
		}

		public Project() : this(string.Empty)
		{

		}

		public Project(ProjectJson projectJson)
		{
			Name = projectJson.Name;
			FullNameProject = projectJson.ProjectFolder;
			Parts = new ObservableCollection<ITreeProject>
			{
				new Boards.BoardList((Boards.BoardJsonList)projectJson.GetBoardList().Clone()),
				(Mechanical.MechanicalList)projectJson.GetMechanicalList().Clone(),
				(Wires.WireList)projectJson.GetWireList().Clone()
			};

		}

		/// <summary>
		/// Получить перечень плат в проекте
		/// </summary>
		/// <returns>Перечень плат</returns>
		public Boards.BoardList GetBoardList()
		{
			return Parts[0] as Boards.BoardList;
		}

		/// <summary>
		/// Получить перечень механических компонентов в проекте
		/// </summary>
		/// <returns>Перечень механических компонентов</returns>
		public Mechanical.MechanicalList GetMechanicalList()
		{
			return Parts[1] as Mechanical.MechanicalList;
		}

		/// <summary>
		/// Получить перечень проводов
		/// </summary>
		/// <returns>Перечень проводов</returns>
		public Wires.WireList GetWireList()
		{
			return Parts[2] as Wires.WireList;
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
			Project p = new Project(Name);

			p.GetBoardList().Boards = new ObservableCollection<Boards.Board>(GetBoardList().Boards);
			p.GetMechanicalList().MechanicalComps = new ObservableCollection<Mechanical.MechanicalComp>(GetMechanicalList().MechanicalComps);
			p.GetWireList().Wires = new ObservableCollection<Wires.Wire>(GetWireList().Wires);

			return p;
		}
	}
}
