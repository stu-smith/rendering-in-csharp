using System;
using System.Collections.Generic;
using System.Linq;

namespace Rendering.Core
{
    public struct Light
    {
        public Light(double r, double g, double b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public static Light None => new Light();

        public static Light operator +(Light la, Light lb)
        {
            return new Light(la.Red + lb.Red, la.Green + lb.Green, la.Blue + lb.Blue);
        }

        public static Light operator *(Light la, ColorProbability cb)
        {
            return new Light(la.Red * cb.Red.Value, la.Green * cb.Green.Value, la.Blue * cb.Blue.Value);
        }

        public static Light operator *(Light la, double db)
        {
            return new Light(la.Red * db, la.Green * db, la.Blue * db);
        }

        public double Red { get; }
        public double Green { get; }
        public double Blue { get; }
    }

    public static class LightExtension
    {
        public static Light SumLights(this IEnumerable<Light> lights)
        {
            return lights.Aggregate((la, lb) => la + lb);
        }
    }
}