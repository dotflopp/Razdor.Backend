using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Bson;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public class DatetimeOffsetConverter() : ValueConverter<DateTimeOffset, long>(
    value => value.ToUnixTimeMilliseconds(),
    value => DateTimeOffset.FromUnixTimeMilliseconds(value)
);