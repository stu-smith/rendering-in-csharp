using System;

namespace Rendering.Core
{
    public struct RefractiveIndex
    {
        public static RefractiveIndex Vacuum
        {
            get
            {
                return new RefractiveIndex(1.0);
            }
        }

        public static RefractiveIndex Air
        {
            get
            {
                return new RefractiveIndex(1.000293);
            }
        }

        public static RefractiveIndex Water
        {
            get
            {
                return new RefractiveIndex(1.333);
            }
        }

        public static RefractiveIndex Glass
        {
            get
            {
                return new RefractiveIndex(1.52);
            }
        }

        public RefractiveIndex(double value)
        {
            if (value < 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            Value = value;
        }
        public Double Value { get; }
    }
}