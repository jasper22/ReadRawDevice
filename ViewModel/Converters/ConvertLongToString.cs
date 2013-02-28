
namespace ReadRawDevice.Gui.ViewModel.Converters
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Windows.Data;

    /// <summary>
    /// Object will convert from <c>long</c>/<c>Int64</c> to string
    /// </summary>
    [ValueConversion(typeof(long), typeof(string))]
    public class ConvertLongToString : IValueConverter
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertLongToString"/> class.
        /// </summary>
        public ConvertLongToString()
        {
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            Int64 data = Int64.Parse(value.ToString(), System.Globalization.CultureInfo.InvariantCulture);

            return data.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
