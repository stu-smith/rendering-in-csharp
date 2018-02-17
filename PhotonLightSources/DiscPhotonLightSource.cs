using System;
using Rendering.Core;

namespace Rendering.PhotonLightSources
{
    public class DiscPhotonLightSource : IPhotonLightSource
    {
        public DiscPhotonLightSource(Point pos, UnitVector normal, double radius, Light light)
        {
            _pos = pos;
            _normal = normal;
            _radius = radius;
            _light = light;
        }

        public (Ray, Light) Emit(IRandomSequence rnd)
        {
            double t1, t2;
            var rs = _radius * _radius;

            do
            {
                t1 = rnd.Double(-_radius, _radius);
                t2 = rnd.Double(-_radius, _radius);
            } while (t1 * t1 + t2 * t2 > rs);

            t1 *= _radius;
            t2 *= _radius;

            (var p1, var p2) = _normal.NormalizedPerpendiculars();
            var dir = rnd.DirectionInHemisphere(_normal);

            var discPos = _pos.Translate((p1 * t1) + (p2 * t2));

            return (new Ray(discPos, dir), _light);
        }

        private readonly Point _pos;
        private readonly UnitVector _normal;
        private readonly double _radius;
        private readonly Light _light;
    }
}