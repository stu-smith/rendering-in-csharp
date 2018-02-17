using System;
using Rendering.Core;

namespace Rendering.Volumes
{
    public class Translated : Volume
    {
        public Translated(NonUnitVector vt, Volume volume)
            : base(volume.Material)
        {
            _nvt = -vt;
            _volume = volume;
        }

        public override RayPosition? SurfaceIntersection(Ray ray)
        {
            return _volume.SurfaceIntersection(ray.Translate(_nvt));
        }

        public override UnitVector SurfaceNormalAtPoint(Point point)
        {
            return _volume.SurfaceNormalAtPoint(point.Translate(_nvt));
        }

        public override bool IsPointInVolume(Point point)
        {
            return _volume.IsPointInVolume(point.Translate(_nvt));
        }

        private readonly NonUnitVector _nvt;
        private readonly Volume _volume;
    }

    public static class TranslatedExtension
    {
        public static Volume Translate(this Volume volume, Point p)
        {
            return new Translated(Point.Origin.To(p), volume);
        }
    }
}