using System;
using Rendering.Core;

namespace Rendering.Volumes
{
    public class InfinitePlane : Volume
    {
        public InfinitePlane(Material material, Point point, UnitVector normal)
            : base(material)
        {
            _point = point;
            _normal = normal;
        }

        public override bool IsPointInVolume(Point point)
        {
            return Vector.DotProduct(_normal, _point.To(point)) <= 0.0;
        }

        public override RayPosition? SurfaceIntersection(Ray ray)
        {
            var rd = ray.RayDirection;
            var ln = Vector.DotProduct(rd, _normal);

            if (ln == 0.0)
            {
                return null;
            }

            var ro = ray.RayOrigin;
            var d = Vector.DotProduct(ro.To(_point), _normal) / ln;

            if (d < 0.0)
            {
                return null;
            }

            return new RayPosition(d);
        }

        public override UnitVector SurfaceNormalAtPoint(Point point)
        {
            return _normal;
        }

        private readonly Point _point;
        private readonly UnitVector _normal;
    }
}