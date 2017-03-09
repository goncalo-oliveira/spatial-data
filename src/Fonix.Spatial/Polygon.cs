using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fonix.Spatial
{
    public sealed class Polygon : Geometry
    {
        public Polygon( IEnumerable<LinearRing> rings )
        {
            Rings = rings;
        }

        public IEnumerable<LinearRing> Rings { get; private set; }

        public override byte[] ToWellKnownBinary()
        {
            /// 0103000000 01000000 03000000 BF9EAF592E1BDBBF 6F445AD726844A40 BF9EAF592E1BDBBF
            ///         00000001  - number of rings
            ///         00000003  - number of points in ring
            /// BF9EAF592E1BDBBF  - point 1
            /// 6F445AD726844A40  - point 2
            /// BF9EAF592E1BDBBF  - point 3

            int numberOfRings = Rings.Count();
            int numberOfPoints = Rings.Sum( x => x.Points.Count() );
            WellKnownBinary wkb = WellKnownBinary.Allocate( GeometryType.Polygon, 9 + ( numberOfRings * 4 ) + ( numberOfPoints * 8 ) );

            wkb.AddRange( BitConverter.GetBytes( numberOfRings ) );

            foreach ( var ring in Rings )
            {
                wkb.AddRange( BitConverter.GetBytes( ring.Points.Count() ) );

                foreach ( var point in ring.Points )
                {
                    wkb.AddRange( BitConverter.GetBytes( point.Longitude ) );
                    wkb.AddRange( BitConverter.GetBytes( point.Latitude ) );
                }
            }

            return ( wkb.ToArray() );
        }

        public override string ToWellKnownText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append( "POLYGON(" );

            foreach ( var ring in Rings )
            {
                sb.Append( "(" );
                sb.Append( string.Join( ", ", ring.Points.Select( x => string.Format( CultureInfo.InvariantCulture, "{1} {0}", x.Latitude, x.Longitude ) ) ) );
                sb.Append( ")" );
            }
            sb.Append( ")" );

            return ( sb.ToString() );
        }

        public override string ToString()
        {
            return ( ToWellKnownText() );
        }
    }
}
