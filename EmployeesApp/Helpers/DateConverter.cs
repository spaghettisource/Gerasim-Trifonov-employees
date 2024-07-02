using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;

public class DateConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        text = text.Trim();

        if (string.IsNullOrEmpty(text) || text.ToLower() == "null")
            return DateTime.Now;

        string[] formats = { "yyyy-MM-dd", "dd-MM-yyyy", "MM/dd/yyyy" };

        foreach (var format in formats)
        {
            if (DateTime.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;
        }

        throw new TypeConverterException(this, memberMapData, text, row.Context, $"Cannot convert '{text}' to DateTime");
    }
}
