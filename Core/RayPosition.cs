using System;

namespace Rendering.Core
{
    public struct RayPosition
    {
        public RayPosition(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}