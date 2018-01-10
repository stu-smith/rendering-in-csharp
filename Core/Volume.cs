using System;

namespace Rendering.Core
{
    public abstract class Volume
    {
        protected Volume(Material material)
        {
            Material = material ?? throw new ArgumentNullException(nameof(material));
        }

        public Material Material { get; }

        public abstract RayPosition? SurfaceIntersection(Ray ray);

        public abstract UnitVector SurfaceNormalAtPoint(Point point);

        public abstract bool IsPointInVolume(Point point);
    }
}