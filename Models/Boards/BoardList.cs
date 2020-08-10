using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Boards
{
	/// <summary>
	/// Перечень плат в проекте
	/// </summary>
	[Serializable]
	public class BoardList : ITreeProject, INotifyPropertyChanged, ICloneable
	{
		private ObservableCollection<Board> boards;

		public ObservableCollection<Board> Boards
		{
			get => boards;
			set
			{
				if (boards != value)
				{
					boards = value;
					NotifyPropertyChanged();
				}
			}
		}

		private int Count => Boards.Count;

		public BoardList()
		{
			Boards = new ObservableCollection<Board>();
			Boards.CollectionChanged += Boards_CollectionChanged;
		}

		private void Boards_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			NotifyPropertyChanged(nameof(Boards));
		}

		public BoardList(BoardJsonList boardJsonList)
		{
			Boards = new ObservableCollection<Board>();
			foreach (BoardJson boardJson in boardJsonList.BoardJsons)
				Boards.Add(new Board(boardJson));
		}

		/// <summary>
		/// Добавить в общий перечень
		/// </summary>
		/// <param name="part">Добавляемый элемент</param>
		public void Add(Board part)
		{
			Boards.Add(part);
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
			return $"Печатные платы [{Count}]";
		}

		public object Clone()
		{
			BoardList list = new BoardList();
			foreach (Board board in Boards)
				list.Add((Board)board.Clone());

			return list;
		}
	}
}
