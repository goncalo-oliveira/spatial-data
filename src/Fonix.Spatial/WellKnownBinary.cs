using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonix.Spatial
{
    internal sealed class WellKnownBinary : IEnumerable<byte>, IEnumerable
    {
        private readonly List<byte> buffer;

        public const byte BigEndian = 0x00;
        public const byte LittleEndian = 0x01;

        /// TODO: customizable byte order
        /// currently, the byte order is determined by the architecture in the running computer
        /// ( check Allocate static method below )
        /// it would be nice to give support to change this, no matter the architecture of the running computer
        /// this would require to swap the default BitConverter for something else

        public WellKnownBinary()
        {
            buffer = new List<byte>();
        }

        public WellKnownBinary( int capacity )
        {
            buffer = new List<byte>( capacity );
        }

        public void Add( byte item )
        {
            buffer.Add( item );
        }

        public void AddRange( IEnumerable<byte> collection )
        {
            buffer.AddRange( collection );
        }

        #region IEnumerable

        public IEnumerator<byte> GetEnumerator()
        {
            return ( buffer.GetEnumerator() );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( buffer.GetEnumerator() );
        }

        #endregion

        /// <summary>
        /// Creates a well-known binary buffer, pre-populated with the byte order and geometry type
        /// </summary>
        /// <param name="geometryType">Geometry type to write into the buffer</param>
        /// <returns>A WellKnownBinary instance, pre-populated with the byte order and geometry type</returns>
        public static WellKnownBinary Allocate( GeometryType geometryType )
        {
            return ( Allocate( geometryType, 0 ) );
        }

        /// <summary>
        /// Creates a well-known binary buffer, pre-populated with the byte order and geometry type
        /// </summary>
        /// <param name="geometryType">Geometry type to write into the buffer</param>
        /// <param name="capacity">The number of bytes that the new instance can initially store</param>
        /// <returns>A WellKnownBinary instance, pre-populated with the byte order and geometry type</returns>
        public static WellKnownBinary Allocate( GeometryType geometryType, int capacity )
        {
            /// 01                - refers to the byte order; 0x01 is for little endian
            /// 01000000          - geometry type; 0x01 is a Point
            /// 

            WellKnownBinary wkb = new WellKnownBinary( capacity );

            wkb.Add( BitConverter.IsLittleEndian ? LittleEndian : BigEndian );
            wkb.AddRange( BitConverter.GetBytes( (int)geometryType ) );

            return ( wkb );
        }
    }
}
