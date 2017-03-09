using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonix.Spatial
{
    public enum GeometryType : int
    {
        Unknown = 0,
        Point,
        LineString,
        Polygon,
        MultiPoint,
        MultiLineString,
        MultiPolygon
    }
}
