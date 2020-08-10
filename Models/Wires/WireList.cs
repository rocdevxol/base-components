using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Wires
{
	/// <summary>
	/// Перечень проводов в системе
	/// </summary>
	[Serializable]
	public class WireList : ITreeProject, INotifyPropertyChanged, ICloneable
	{
		private ObservableCollection<Wire> wires;

		/// <summary>
		/// Список механических компонентов
		/// </summary>
		public ObservableCollection<Wire> Wires
		{
			get => wires;
			set
			{
				if (wires != value)
				{
					wires = value;
					NotifyPropertyChanged();
				}
			}
		}

		private int Count => Wires.Count;

		public WireList()
		{
			Wires = new ObservableCollection<Wire>();
		}

		/// <summary>
		/// Добавить в общий перечень
		/// </summary>
		/// <param name="part">Добавляемый элемент</param>
		public void Add(Wire part)
		{
			Wires.Add(part);
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
			return $"Провода [{Count}]";
		}

		public object Clone()
		{
			WireList list = new WireList();
			foreach (Wire part in Wires)
				list.Add((Wire)part.Clone());
			return list;
		}
	}
}
