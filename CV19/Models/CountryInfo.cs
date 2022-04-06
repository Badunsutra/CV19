using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _Location;
        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }

        public override Point Location
        {
            get
            {
                if(_Location != null)
                    return (Point)_Location;

                if (ProvinceCounts == null)
                    return default;

                var avarage_x = ProvinceCounts.Average(p => p.Location.X);
                var average_y = ProvinceCounts.Average(p => p.Location.Y);

                return (Point)(_Location = new Point(avarage_x, average_y));
            }
            set => _Location = value;
        }
    }


}
