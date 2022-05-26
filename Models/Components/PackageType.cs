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
		Unknown = 0,
		[Description("SMD/SMT")]
		/// <summary>
		/// SMD монтаж однослойный
		/// </summary>
		SMD_SMT = 1,
		[Description("THT")]
		/// <summary>
		/// Выводной монтаж, через отверстие
		/// </summary>
		THT = 2,
		[Description("Без монтажа")]
		/// <summary>
		/// Контакты разъемов, фурнитура и пр., что не запаивается в плату, но присутствует в сборке
		/// </summary>
		NO_SOLDERING = 3
	}
}
