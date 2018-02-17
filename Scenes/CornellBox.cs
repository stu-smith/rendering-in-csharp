using System;
using Rendering.Core;
using Rendering.Volumes;

namespace Rendering.Scenes
{
    public static class CornellBox
    {
        public static Scene CreateScene()
        {
            var reddish = Material.CreateDiffuseSpecular(new ColorProbability(0.50, 0.25, 0.25), new ColorProbability(0.00, 0.00, 0.00));
            var blueish = Material.CreateDiffuseSpecular(new ColorProbability(0.25, 0.25, 0.50), new ColorProbability(0.00, 0.00, 0.00));
            var greyish = Material.CreateDiffuseSpecular(new ColorProbability(0.50, 0.50, 0.50), new ColorProbability(0.00, 0.00, 0.00));

            var emissiveWhite = Material.CreateEmmissive(new Light(2500.0, 2500.0, 2500.0));
            var reflective = Material.CreateDiffuseSpecular(new ColorProbability(0.00, 0.00, 0.00), new ColorProbability(0.99, 0.99, 0.99));
            var refractive = Material.CreateRefractive(new ColorProbability(0.10, 0.10, 0.10), new ColorProbability(0.89, 0.89, 0.89), RefractiveIndex.Glass);

            Func<Point, UnitVector, Material, Volume> plane = (Point pt, UnitVector v, Material m) => new InfinitePlane(m, pt, v);
            Func<Point, double, Material, Volume> sphere = (Point pt, double r, Material m) => new Sphere(m, r).Translate(pt);
            Func<double, double, double, Point> p = (double x, double y, double z) => new Point(x, y, z);

            var scene = new Scene(new[]{
                plane(  p( 1.0, 40.8, 81.6), UnitVector.PositiveX, reddish ),
                plane(  p(99.0, 40.8, 81.6), UnitVector.NegativeX, blueish ),
                plane(  p(50.0, 40.8,  0.0), UnitVector.PositiveZ, greyish ),
                plane(  p(50.0,  0.0, 81.6), UnitVector.PositiveY, greyish ),
                plane(  p(50.0, 81.6, 81.6), UnitVector.NegativeY, greyish ),

                sphere( p(27.0, 16.5, 47.0), 16.5,                 reflective),
                sphere( p(73.0, 16.5, 78.0), 16.5,                 refractive),

                sphere( p(50.0, 81.4, 81.6),  5.0,                 emissiveWhite)
            });

            return scene;
        }
    }
}