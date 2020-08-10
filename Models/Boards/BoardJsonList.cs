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
	public class BoardJsonList : ITreeProject, INotifyPropertyChanged, ICloneable
	{
		private ObservableCollection<BoardJson> boardJsons;

		public ObservableCollection<BoardJson> BoardJsons
		{
			get => boardJsons;
			set
			{
				if (boardJsons != value)
				{
					boardJsons = value;
					NotifyPropertyChanged();
				}
			}
		}

		private int Count => BoardJsons.Count;

		public BoardJsonList()
		{
			BoardJsons = new ObservableCollection<BoardJson>();
		}

		public BoardJsonList(BoardList boardList)
		{
			BoardJsons = new ObservableCollection<BoardJson>();
			foreach (Board board in boardList.Boards)
				BoardJsons.Add(new BoardJson(board));
		}
		/// <summary>
		/// Добавить в общий перечень
		/// </summary>
		/// <param name="part">Добавляемый элемент</param>
		public void Add(Board part)
		{
			BoardJsons.Add(new BoardJson(part));
		}

		public void Add(BoardJson part)
		{
			BoardJsons.Add(part);
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
			BoardJsonList list = new BoardJsonList();
			foreach (BoardJson board in BoardJsons)
				list.Add((BoardJson)board.Clone());

			return list;
		}
	}
}
