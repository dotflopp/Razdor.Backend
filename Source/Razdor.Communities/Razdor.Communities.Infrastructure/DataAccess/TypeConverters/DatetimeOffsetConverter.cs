using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Razdor.Communities.Infrastructure.DataAccess.TypeConverters;

public class DatetimeOffsetConverter() : ValueConverter<DateTimeOffset, string>(
    value => value.ToString("o"),
    value => DateTimeOffset.ParseExact(value, "o", CultureInfo.InvariantCulture)
);