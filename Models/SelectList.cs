using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models
{
	[Serializable]
	public class SelectList<T> : ITreeProject, INotifyPropertyChanged, ICloneable
	{

		private ObservableCollection<T> collection;

		public ObservableCollection<T> Collection
		{
			get => collection;
			set
			{
				if (collection != value)
				{
					collection = value;
					NotifyPropertyChanged();
				}
			}
		}

		private int Count => Collection.Count;

		public SelectList()
		{
			Collection = new ObservableCollection<T>();
		}

		/// <summary>
		/// Добавить в общий перечень
		/// </summary>
		/// <param name="part">Добавляемый элемент</param>
		public void Add(T part)
		{
			Collection.Add(part);
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
			SelectList<T> list = new SelectList<T>();

			foreach (T item in Collection)
			{
				list.Add(ObjectCopier.Clone<T>(item));
			}

			return list;
		}
	}
}
