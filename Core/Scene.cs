using System;
using System.Collections.Generic;
using System.Linq;

namespace Rendering.Core
{
    public sealed class Scene
    {
        public Scene(IEnumerable<Volume> volumes)
        {
            if (volumes == null)
            {
                throw new ArgumentNullException(nameof(volumes));
            }
            Volumes = volumes.ToList();
        }

        public IReadOnlyList<Volume> Volumes { get; }
    }
}