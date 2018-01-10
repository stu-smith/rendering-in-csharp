using System;

namespace Rendering.Core
{
    public interface IPhotonLightSource
    {
        Tuple<Ray, Light> Emit();
    }
}
