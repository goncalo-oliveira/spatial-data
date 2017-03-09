using System.Collections;
using System.Collections.Generic;

namespace Fonix.Spatial
{
    public sealed class LinearRing : IEnumerable<Point>
    {
        public LinearRing( IEnumerable<Point> points )
        {
            Points = points;
        }

        public IEnumerable<Point> Points { get; private set; }

        #region IEnumerable

        public IEnumerator<Point> GetEnumerator()
        {
            return ( Points.GetEnumerator() );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( Points.GetEnumerator() );
        }

        #endregion
    }
}
