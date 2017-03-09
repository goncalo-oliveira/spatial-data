using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonix.Spatial
{
    public abstract class Geometry
    {
        protected Geometry()
        { }

        /// <summary>
        /// Gets the current geometry as well-known binary (WKB)
        /// </summary>
        /// <returns>A byte array containing the well-known binary representation</returns>
        public abstract byte[] ToWellKnownBinary();

        /// <summary>
        /// Gets the current geometry as well-known text (WKT)
        /// </summary>
        /// <returns>A human-readable string containing the well-known text representation</returns>
        public abstract string ToWellKnownText();

        // TODO: need a parser
    }
}
