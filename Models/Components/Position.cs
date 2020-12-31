using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Models.Components
{
	/// <summary>
	/// Компоненты применяемые в проектах
	/// </summary>
	[Serializable]
	public class Position : INotifyPropertyChanged, ICloneable
	{
		private double positionX;
		private double positionY;
		private double angle;
		private bool mirror;

		/// <summary>
		/// X координата положения компонента
		/// </summary>
		public double PositionX
		{
			get => positionX;
			set
			{
				if (positionX != value)
				{
					positionX = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Y координата положения компонента
		/// </summary>
		public double PositionY
		{
			get => positionY;
			set
			{
				if (positionY != value)
				{
					positionY = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Угол поворота компонента на плате
		/// </summary>
		public double Angle
		{
			get => angle;
			set
			{
				if (angle != value)
				{
					angle = value;
					NotifyPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Угол поворота компонента на плате
		/// </summary>
		public bool Mirror
		{
			get => mirror;
			set
			{
				if (mirror != value)
				{
					mirror = value;
					NotifyPropertyChanged();
				}
			}
		}

		public Position(double X, double Y, double angle, bool mirror)
		{
			PositionX = X;
			PositionY = Y;
			Angle = angle;
			Mirror = mirror;
		}

		public Position() : this(0, 0, 0, false)
		{

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
			return new Position(PositionX, PositionY, Angle, Mirror);
		}
	}
}
