using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fonix.Spatial
{
    public sealed class LineString : Geometry
    {
        public LineString( IEnumerable<Point> points )
        {
            Points = points;
        }

        public IEnumerable<Point> Points { get; private set; }

        public override byte[] ToWellKnownBinary()
        {
            /// 0102000000 03000000 BF9EAF592E1BDBBF 6F445AD726844A40 BF9EAF592E1BDBBF
            ///         03000000  - number of points
            /// BF9EAF592E1BDBBF  - point 1
            /// 6F445AD726844A40  - point 2
            /// BF9EAF592E1BDBBF  - point 3

            int numberOfPoints = Points.Count();
            WellKnownBinary wkb = WellKnownBinary.Allocate( GeometryType.LineString, 9 + ( numberOfPoints * 8 ) );

            wkb.AddRange( BitConverter.GetBytes( numberOfPoints ) );

            foreach ( var point in Points )
            {
                wkb.AddRange( BitConverter.GetBytes( point.Longitude ) );
                wkb.AddRange( BitConverter.GetBytes( point.Latitude ) );
            }

            return ( wkb.ToArray() );
        }

        public override string ToWellKnownText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append( "LINESTRING(" );
            sb.Append( string.Join( ", ", Points.Select( x => string.Format( CultureInfo.InvariantCulture, "{1} {0}", x.Latitude, x.Longitude ) ) ) );
            sb.Append( ")" );

            return ( sb.ToString() );
        }

        public override string ToString()
        {
            return ( ToWellKnownText() );
        }
    }
}
