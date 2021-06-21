using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Gerber
{
	[Serializable]
	public class Gerber : ITreeProject, INotifyPropertyChanged, ICloneable
	{
		#region variables
		private int numLayers;
		private ObservableCollection<GerberLayer> layers;


		/// <summary>
		/// Колиество слоев в печатной плате
		/// </summary>
		public int NumLayers
		{
			get => numLayers;
			set
			{
				if (numLayers != value)
				{
					numLayers = value;
					NotifyPropertyChanged();
				}
			}
		}

		public ObservableCollection<GerberLayer> Layers
		{
			get => layers;
			set
			{
				if (layers != value)
				{
					layers = value;
					NotifyPropertyChanged();
				}
			}
		}

		#endregion


		/// <summary>
		/// ИНициализация Гербер слоев
		/// </summary>
		/// <param name="numLayers">Количество сигнальных слоев на плате</param>
		public Gerber(int numLayers)
		{
			NumLayers = numLayers;
			Layers = new ObservableCollection<GerberLayer>
			{
				new GerberLayer("Top Layer", "GTL"),
				new GerberLayer("Bottom Layer", "GBL"),
				new GerberLayer("Top Soldermask", "GTS"),
				new GerberLayer("Bottom Soldermask", "GBS"),
				new GerberLayer("Top Silkscreen", "GTO"),
				new GerberLayer("Bottom Silkscreen", "GBO"),
				new GerberLayer("Board Outline", "GKO")
			};

			if (NumLayers > 2)
			{
				for (int i = 3; i <= NumLayers; i++)
				{
					Layers.Add(new GerberLayer($"Internal {i} Layer", $"G{i - 1}L"));
				}
			}

		}

		public Gerber() : this(2)
		{
		}

		/// <summary>
		/// Добавление нового сигнального слоя в список
		/// </summary>
		/// <param name="gerber">Содержимое гербер файла</param>
		public void AddLayer(string gerber)
		{
			NumLayers++;
			GerberLayer layer = new GerberLayer($"Internal {NumLayers} Layer", $"G{NumLayers - 1}L")
			{
				Content = gerber
			};
			Layers.Add(layer);
		}

		#region Events
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion


		public object Clone()
		{
			Gerber gerber = new Gerber(NumLayers);
			gerber.Layers.Clear();
			foreach (GerberLayer layer in Layers)
			{
				gerber.Layers.Add((GerberLayer)layer.Clone());
			}

			return gerber;
		}

		public override string ToString()
		{
			return $"GERBER файлы";
		}
	}
}
