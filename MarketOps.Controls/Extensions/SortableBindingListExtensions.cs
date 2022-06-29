using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// Extensions for SortableBindingList.
    /// </summary>
    internal static class SortableBindingListExtensions
    {
        public static BindingList<T> ToSortableBindingList<T>(this IEnumerable<T> source) => 
            new SortableBindingList<T>(source.ToList());
    }
}
