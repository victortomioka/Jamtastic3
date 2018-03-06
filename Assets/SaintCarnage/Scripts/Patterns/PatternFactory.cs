using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage
{
    public enum Patterns
    {
        Linear,
        Cone,
        ConeRandomSpread
    }

    public static class PatternFactory
    {
        public static IPattern Get(Patterns patterns)
        {
            switch (patterns)
            {
                case Patterns.Linear:
                default:
                    return new LinearPattern();

                case Patterns.Cone:
                    return new ConePattern();

                case Patterns.ConeRandomSpread:
                    return new ConeRandomSpreadPattern();
            }
        }
    }
}