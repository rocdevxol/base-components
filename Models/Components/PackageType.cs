using System.ComponentModel;

namespace Models.Components
{
	[TypeConverter(typeof(Converters.EnumDescriptionTypeConverter))]
	public enum PackageType
	{
		[Description("не определен")]
		/// <summary>
		/// Тип монтажа не определен
		/// </summary>
		Unknown,
		[Description("SMD/SMT")]
		/// <summary>
		/// SMD монтаж однослойный
		/// </summary>
		SMD_SMT,
		[Description("THT")]
		/// <summary>
		/// Выводной монтаж, через отверстие
		/// </summary>
		THT
	}
}
