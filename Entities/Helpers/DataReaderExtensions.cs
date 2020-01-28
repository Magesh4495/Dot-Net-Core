using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Entity.Helpers
{
    internal static class DataReaderExtensions
    {
        /// <summary>
        /// Gets the value of specified type of a specified field/column name.
        /// </summary>
        /// <typeparam name="T">Expected type of the value.</typeparam>
        /// <param name="reader">An instance of the <see cref="IDataReader"/> class.</param>
        /// <param name="key">Name of the field/column.</param>
        /// <returns>The value of the field in the specified type.</returns>
        internal static T GetValue<T>(this IDataReader reader, string key)
        {
            if (reader == null || reader[key] == null || reader[key] is DBNull)
            {
                return default(T);
            }

            return (T)reader[key];
        }

        /// <summary>
        /// Gets the collection of items from a specified column that returns an xml string.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="reader">An instance of the <see cref="IDataReader"/> class.</param>
        /// <param name="key">The column name.</param>
        /// <returns>A collection of items of the specified type.</returns>
        //internal static Collection<T> GetCollection<T>(this IDataReader reader, string key)
        //{
        //    if (reader == null || reader[key] == null || reader[key] is DBNull)
        //    {
        //        return new Collection<T>();
        //    }

        //    var xml = (string)reader[key];
        //    return SqlXmlHelper.FromSqlXml<Collection<T>>(ReplaceStringDates(xml));
        //}

        /// <summary>
        /// Replaces all instances of string dates in an input string to a date format 
        /// required by the XML Serializer when de-serializing xml values from the database.
        /// Ex. 2013-04-22 10:57:20.310 will be replaced with 2013-04-22T10:57:20.310 (the character T was added).
        /// </summary>
        /// <param name="input">A string where search and replace will be made.</param>
        /// <returns>A string with the replaced dates.</returns>
        private static string ReplaceStringDates(string input)
        {
            const string Pattern = @"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})(?<space>\s{1})(?<hour>\d{2}):(?<min>\d{2}):(?<sec>\d{2}).(?<millisec>\d{3})";
            const string Replacement = "${year}-${month}-${day}T${hour}:${min}:${sec}.${millisec}";
            return Regex.Replace(input, Pattern, Replacement);
        }
    }
}
