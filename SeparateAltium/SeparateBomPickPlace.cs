using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Components;

namespace SeparateAltium
{
	/// <summary>
	/// 
	/// </summary>
	public class SeparateBomPickPlace
	{
		private enum SmdNamed
		{
			Unknown,
			Resistor,
			Capacitor,
			Inductance
		}

		/// <summary>
		/// Информация из файла о местоположении компонентов
		/// </summary>
		public Dictionary<string, Position> pickPlace { get; set; }

		public ObservableCollection<Component> components { get; set; }

		public Dictionary<string, Position> ImportPickPlace(string fileName)
		{
			pickPlace = new Dictionary<string, Position>();

			StreamReader reader = new StreamReader(fileName);
			string result;
			do
			{
				result = reader.ReadLine();
			} while (!result.Contains("Designator Center-X(mm) Center-Y(mm) Layer       Rotation"));

			while (!reader.EndOfStream)
			{
				result = reader.ReadLine();
				string[] param = ReplaceSpaces(result).Split(new char[] { ' ' });
				Position position = new Position();
				position.PositionX = ConvertToDouble(param[1]);
				position.PositionY = ConvertToDouble(param[2]);
				position.Angle = ConvertToDouble(param[4]);
				if (param[3] == "TopLayer")
					position.Mirror = false;
				else
					position.Mirror = true;
				pickPlace.Add(param[0], position);
			}

			//NCE80H12
			//IRF9N50S
			//

			return pickPlace;
		}

		/// <summary>
		/// Входной формат данных:
		/// Designator	Comment	Description	Footprint	LibRef	LCSC	Part Number
		/// </summary>
		/// <param name="fileName"></param>
		public ObservableCollection<Component> ImportBom(string fileName)
		{
			if (pickPlace == null) return null;
			if (pickPlace.Count == 0) return null;

			components = new ObservableCollection<Component>();

			StreamReader reader = new StreamReader(fileName);
			string result;
			do
			{
				result = reader.ReadLine();
			} while (!result.Contains("Designator	Comment	Description	Footprint	LibRef	LCSC	Part Number"));

			while (!reader.EndOfStream)
			{
				
				string[] param = reader.ReadLine().Split(new char[] { '\t' });
				if (param.Length == 1) continue;
				string[] designator = param[0].Replace("\""," ").Trim().Split(new char[] { ',' });

				string comment = param[1].Replace("\"", " ").Trim();
				string description = param[2].Replace("\"", " ").Trim();
				string footprint = param[3].Replace("\"", " ").Trim();
				string libref = param[4].Replace("\"", " ").Trim();
				string lcsc = param[5].Replace("\"", " ").Trim();
				string partNumber = param[6].Replace("\"", " ").Trim();


				Package package = new Package(footprint);
				SmdNamed named = SmdType(footprint);
				if (named != SmdNamed.Unknown)
				{
					switch (named)
					{	
						case SmdNamed.Resistor:
							package = new Package(GetPackage(footprint));
							comment = GetValueResistor(comment, footprint);
							description = "Тонкопленочные резисторы – для поверхностного монтажа";
							break;
						case SmdNamed.Capacitor:
							package = new Package(GetPackage(footprint));
							comment = GetValueCapacitor(comment, footprint); 
							description = "Многослойные керамические конденсаторы - поверхностного монтажа";
							break;
						case SmdNamed.Inductance:
							package = new Package(GetPackage(footprint));
							comment = GetValueInductance(comment, footprint);
							break;
						default:
							break;
					}
				}

				foreach (string refdes in designator)
				{
					Component component = new Component(refdes.Trim());
					pickPlace.TryGetValue(refdes.Trim(), out Position position);
					component.Position = position;
					component.TypeComponent = description;
					component.Soldering = true;
					component.Count = 1;
					component.Description = comment;
					component.EquivalentName = comment;
					if (partNumber == string.Empty)
						partNumber = comment;
					component.Names.Add(new SubComponent(partNumber));
					component.Names[0].LCSC = lcsc;
					component.Names[0].Package = package;
					components.Add(component);
				}
			}
			return components;
		}


		private string ReplaceSpaces(string value)
		{
			while (value.Contains("  "))
			{
				value = value.Replace("  ", " ");
			}
			value = value.Trim();
			return value;
		}

		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		private double ConvertToDouble(string S)
		{
			if (S == string.Empty || S == "&nbsp;") return 0;
			try
			{
				if (S.IndexOf('.') != -1) S = S.Replace(".", ",");
				return double.Parse(S);
			}
			catch
			{
				if (S.IndexOf(',') != -1) S = S.Replace(",", ".");
				return double.Parse(S);
			}
		}

		private SmdNamed SmdType(string package)
		{
			try
			{
				string[] separate = package.Split(new char[] { '_' });

				if (int.Parse(separate[1]) == 0)
				{
					return SmdNamed.Unknown;
				}

				if (separate[0] == "C")
					return SmdNamed.Capacitor;
				if (separate[0] == "R")
					return SmdNamed.Resistor;
				if (separate[0] == "L")
					return SmdNamed.Inductance;
			}
			catch
			{
				return SmdNamed.Unknown;
			}
			return SmdNamed.Unknown;
		}

		private string GetValueCapacitor(string oldvalue, string package)
		{
			//string value = oldvalue;
			string[] separate = package.Split(new char[] { '_' });
			//string value = string.Format("{0}{1}    {2}", separate[0], separate[1], oldvalue);
			string value = string.Format("{0}    {1}", separate[1], oldvalue);

			return value;
		}

		private string GetValueResistor(string oldvalue, string package)
		{
			string[] separate = package.Split(new char[] { '_' });
			//string value = string.Format("{0}{1}    {2}", separate[0], separate[1], oldvalue);
			string value = string.Format("{0}    {1}Ω", separate[1], oldvalue);

			return value;
		}

		private string GetValueInductance(string oldvalue, string package)
		{
			string[] separate = package.Split(new char[] { '_' });
			//string value = string.Format("{0}{1}    {2}", separate[0], separate[1], oldvalue);
			string value = string.Format("{0}    {1}", separate[1], oldvalue);

			return value;
		}

		private string GetPackage(string package)
		{
			string[] separate = package.Split(new char[] { '_' });
			return string.Format("SMD{0}", separate[1]);
		}
	}
}
