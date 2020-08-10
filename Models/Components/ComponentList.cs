using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Components
{
	/// <summary>
	/// Перечень компонентов в печатной плате
	/// </summary>
	[Serializable]
	public class ComponentList : ITreeProject, INotifyPropertyChanged, ICloneable
	{
		private ObservableCollection<Component> components;

		/// <summary>
		/// Перечень компонентов
		/// </summary>
		public ObservableCollection<Component> Components
		{
			get => components;
			set
			{
				if (components != value)
				{
					components = value;
					NotifyPropertyChanged();
					NotifyPropertyChanged("Count");
				}
			}
		}

		private int Count => Components.Count;

		public ComponentList()
		{
			Components = new ObservableCollection<Component>();
		}


		/// <summary>
		/// Добавить в общий перечень
		/// </summary>
		/// <param name="part">Добавляемый элемент</param>
		public void Add(Component part)
		{
			Components.Add(part);
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
			return $"Перечень компонентов [{Count}]";
		}

		public object Clone()
		{
			ComponentList list = new ComponentList();
			foreach (Component component in Components)
				list.Add((Component)component.Clone());

			return list;
		}
	}

}
