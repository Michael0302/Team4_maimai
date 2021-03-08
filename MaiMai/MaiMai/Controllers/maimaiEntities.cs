using System.Collections.Generic;

namespace MaiMai.Controllers
{
    internal class maimaiEntities
    {
        public IEnumerable<object> Order { get; internal set; }
        public IEnumerable<object> Member { get; internal set; }
        public IEnumerable<object> OrderDetail { get; internal set; }
    }
}