using System.Collections.Generic;
using UnityEngine;

namespace BTQ.Core.Extensions
{
    public static class TransformExtensions
    {
        public static IEnumerable<Transform> Children(this Transform parent)
        {
            foreach (Transform child in parent)
            {
                yield return child;
            }
        }
    }
}