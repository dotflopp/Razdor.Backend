using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Razdor.Communities.Infrastructure.DataAccess.TypeConverters;

public class DatetimeOffsetConverter() : ValueConverter<DateTimeOffset, string>(
    value => value.ToString("u"),
    value => DateTimeOffset.ParseExact(value, "u", CultureInfo.InvariantCulture)
);