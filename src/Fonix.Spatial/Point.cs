using System;
using System.Globalization;
using System.Linq;

namespace Fonix.Spatial
{
    public sealed class Point : Geometry
    {
        public Point( double latitude, double longitude )
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public override byte[] ToWellKnownBinary()
        {
            /// 0101000000 BF9EAF592E1BDBBF 6F445AD726844A40
            /// BF9EAF592E1BDBBF  - longitude
            /// 6F445AD726844A40  - latitude

            WellKnownBinary wkb = WellKnownBinary.Allocate( GeometryType.Point, 21 );

            wkb.AddRange( BitConverter.GetBytes( Longitude ) );
            wkb.AddRange( BitConverter.GetBytes( Latitude ) );

            return ( wkb.ToArray() );
        }

        public override string ToWellKnownText()
        {
            return ( string.Format( CultureInfo.InvariantCulture, "POINT({1} {0})", Latitude, Longitude ) );
        }

        public override string ToString()
        {
            return ( ToWellKnownText() );
        }

        public static Point FromWellKnownBinary( byte[] wkb )
        {
            byte order = wkb[ 0 ];
            int geometryType = BitConverter.ToInt32( wkb, 1 );

            if ( geometryType != (int)GeometryType.Point )
            {
                // invalid geometry type
                // TODO: maybe throw exception instead?
                return ( null );
            }

            double latitude = BitConverter.ToDouble( wkb, 5 );
            double longitude = BitConverter.ToDouble( wkb, 13 );

            return ( new Point( latitude, longitude ) );
        }
    }
}
