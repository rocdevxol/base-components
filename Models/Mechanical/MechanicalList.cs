using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Mechanical
{
	[Serializable]
	public class MechanicalList : ITreeProject, INotifyPropertyChanged, ICloneable
	{
		private ObservableCollection<MechanicalComp> mechanicalComps;

		/// <summary>
		/// Список механических компонентов
		/// </summary>
		public ObservableCollection<MechanicalComp> MechanicalComps
		{
			get => mechanicalComps;
			set
			{
				if (mechanicalComps != value)
				{
					mechanicalComps = value;
					NotifyPropertyChanged();
				}
			}
		}

		private int Count => MechanicalComps.Count;

		public MechanicalList()
		{
			MechanicalComps = new ObservableCollection<MechanicalComp>();
		}

		/// <summary>
		/// Добавить в общий перечень
		/// </summary>
		/// <param name="part">Добавляемый элемент</param>
		public void Add(MechanicalComp part)
		{
			MechanicalComps.Add(part);
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
			return $"Механические компоненты [{Count}]";
		}

		public object Clone()
		{
			MechanicalList list = new MechanicalList();
			foreach (MechanicalComp part in MechanicalComps)
				list.Add((MechanicalComp)part.Clone());

			return list;
		}
	}
}
