using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Movies.Api.Data.ValueConverters;

public class DateTimeToChar8Converter() : ValueConverter<DateTime, string>(
	datetime => datetime.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
	stringValue => DateTime.ParseExact(stringValue, "yyyyMMdd", CultureInfo.InvariantCulture));