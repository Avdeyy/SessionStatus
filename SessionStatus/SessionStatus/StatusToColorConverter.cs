using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SessionStatus
{
    internal class StatusToColorConverter : IValueConverter
    {
        private string status;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                status = value.ToString();
                if (status.Equals("Зачет"))
                {
                    return "#3bc92e";
                }
                else if (status.Equals("Незачет"))
                {
                    return "#c92222";
                }
                else return "#412919";        
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
