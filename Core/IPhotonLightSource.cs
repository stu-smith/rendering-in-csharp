using System;

namespace Rendering.Core
{
    public interface IPhotonLightSource
    {
        (Ray, Light) Emit(IRandomSequence rnd);
    }
}
