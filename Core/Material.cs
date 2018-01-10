using System;
using System.Collections.Generic;
using System.Linq;

namespace Rendering.Core
{
    public delegate Light BRDF(
        Light incomingLight,
        UnitVector incomingVector,
        UnitVector outgoingVector,
        UnitVector surfaceNormal,
        Point surfacePosition
    );

    public sealed class Material
    {
        public static Material CreateDiffuseSpecular(ColorProbability diffuseProbabilities, ColorProbability specularProbabilities)
        {
            var drdgdb = diffuseProbabilities.SumComponents();
            var srsgsb = specularProbabilities.SumComponents();
            var pReflect = ColorProbability.MaxOfAdditions(diffuseProbabilities, specularProbabilities);
            var pDiffuse = pReflect * drdgdb / (drdgdb + srsgsb);

            return new Material(
                diffuseProbabilities,
                specularProbabilities,
                ColorProbability.Zero,
                pDiffuse,
                pReflect - pDiffuse,
                Probability.Zero,
                PhongBRDF(diffuseProbabilities, specularProbabilities),
                Light.None,
                RefractiveIndex.Vacuum
            );
        }

        public static Material CreateEmmissive(Light light)
        {
            return new Material(
                ColorProbability.Zero, ColorProbability.Zero, ColorProbability.Zero,
                Probability.Zero, Probability.Zero, Probability.Zero,
                EmmisiveBRDF(light), light, RefractiveIndex.Vacuum
            );
        }

        public static Material CreateRefractive(
            ColorProbability specularProbabilities,
            ColorProbability refractiveProbabilities,
            RefractiveIndex refractiveIndex
        )
        {
            var sr = specularProbabilities.Red;
            var sg = specularProbabilities.Green;
            var sb = specularProbabilities.Blue;

            var rr = refractiveProbabilities.Red;
            var rg = refractiveProbabilities.Green;
            var rb = refractiveProbabilities.Blue;

            var pReflect = ColorProbability.MaxOfAdditions(specularProbabilities, refractiveProbabilities);
            var pRefract = ColorProbability.ProportionOfAdditions(specularProbabilities, refractiveProbabilities);

            return new Material(
                ColorProbability.Zero, specularProbabilities, refractiveProbabilities,
                Probability.Zero, pReflect, pRefract,
                PhongBRDF(ColorProbability.Zero, specularProbabilities),
                Light.None, refractiveIndex
            );
        }

        private static BRDF EmmisiveBRDF(Light light)
        {
            return (
                Light incomingLight,
                UnitVector incomingVector,
                UnitVector outgoingVector,
                UnitVector surfaceNormal,
                Point surfacePosition
            ) => light;
        }

        private static BRDF PhongBRDF(ColorProbability diffuseProbabilities, ColorProbability specularProbabilities)
        {
            return SumBRDFs(new[] { DiffuseBRDF(diffuseProbabilities), SpecularBRDF(specularProbabilities) });
        }

        private static BRDF SumBRDFs(IEnumerable<BRDF> brdfs)
        {
            return (incomingLight, incomingVector, outgoingVector, surfaceNormal, surfacePosition) =>
            {
                var lights = brdfs.Select(brdf => brdf(incomingLight, incomingVector, outgoingVector, surfaceNormal, surfacePosition)).ToList();

                return lights.SumLights();
            };
        }

        private static BRDF DiffuseBRDF(ColorProbability colorProbability)
        {
            return (
                Light incomingLight,
                UnitVector incomingVector,
                UnitVector outgoingVector,
                UnitVector surfaceNormal,
                Point surfacePosition
            ) => incomingLight * colorProbability * Vector.DotProduct(outgoingVector, surfaceNormal);
        }

        private static BRDF SpecularBRDF(ColorProbability colorProbability)
        {
            return (
                Light incomingLight,
                UnitVector incomingVector,
                UnitVector outgoingVector,
                UnitVector surfaceNormal,
                Point surfacePosition
            ) =>
            {
                var reflectionVector = surfaceNormal * Vector.DotProduct(surfaceNormal * 2.0, incomingVector) - incomingVector;

                return incomingLight * colorProbability * Vector.DotProduct(outgoingVector, reflectionVector);
            };
        }

        public Material(
            ColorProbability diffuseProbabilities,
            ColorProbability specularProbabilities,
            ColorProbability refractiveProbabilities,
            Probability diffuseReflectionProbability,
            Probability specularReflectionProbability,
            Probability refractionProbability,

            BRDF brdf,
            Light emmissiveLight,
            RefractiveIndex refractiveIndex
        )
        {
            DiffuseProbabilities = diffuseProbabilities;
            SpecularProbabilities = specularProbabilities;
            RefractiveProbabilities = refractiveProbabilities;

            DiffuseReflectionProbability = diffuseReflectionProbability;
            SpecularReflectionProbability = specularReflectionProbability;
            RefractionProbability = refractionProbability;

            BRDF = brdf;
            EmmissiveLight = emmissiveLight;
            RefractiveIndex = refractiveIndex;
        }

        public ColorProbability DiffuseProbabilities { get; }
        public ColorProbability SpecularProbabilities { get; }
        public ColorProbability RefractiveProbabilities { get; }

        public Probability DiffuseReflectionProbability { get; }
        public Probability SpecularReflectionProbability { get; }
        public Probability RefractionProbability { get; }

        public BRDF BRDF { get; }
        public Light EmmissiveLight { get; }
        public RefractiveIndex RefractiveIndex { get; }
    }
}