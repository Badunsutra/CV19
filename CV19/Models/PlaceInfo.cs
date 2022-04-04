using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CV19.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }
        public virtual Point Location { get; set; }
        public IEnumerable<ConfirmedCount> Counts { get; set; }
    }


}
