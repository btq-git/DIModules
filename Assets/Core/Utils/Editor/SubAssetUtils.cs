using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using System;
using UnityEditor;
#endif

namespace BTQ.Core.Utils
{
#if UNITY_EDITOR
    public static class SubAssetUtils
    {
        public static T CreateSubAsset<T>(Object parent, bool markDirty = true) where T : Object
        {
            return CreateSubAsset(typeof(T), parent, markDirty) as T;
        }

        public static Object CreateSubAsset(Type type, Object parent, bool markDirty = true)
        {
            string path = AssetDatabase.GetAssetPath(parent);

            var newAsset = ScriptableObject.CreateInstance(type);
            newAsset.name = type.Name;
            AssetDatabase.AddObjectToAsset(newAsset, parent);
            if (markDirty)
            {
                AssetDatabase.ImportAsset(path);
                EditorUtility.SetDirty(parent);
            }

            return newAsset;
        }

        public static T EnsureUniqueSubAsset<T>(ScriptableObject parent, string name) where T : Object
        {
            var allSubAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(parent));
            var subAssetsOfType = allSubAssets.OfType<T>().ToList();

            if (subAssetsOfType.Count == 0)
            {
                return CreateSubAsset<T>(parent);
            }
            else
            {
                while (subAssetsOfType.Count > 1)
                {
                    Object.DestroyImmediate(subAssetsOfType[^1], true);
                    subAssetsOfType.RemoveAt(subAssetsOfType.Count - 1);
                }

                return subAssetsOfType[0];
            }
        }

        public static Object EnsureUniqueSubAsset(Type type, ScriptableObject parent, string name)
        {
            var allSubAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(parent));
            var subAssetsOfType = allSubAssets.Where(asset => type.IsAssignableFrom(asset.GetType())).ToList();

            if (subAssetsOfType.Count == 0)
            {
                return CreateSubAsset(type, parent);
            }
            else
            {
                while (subAssetsOfType.Count > 1) Object.DestroyImmediate(subAssetsOfType[^1], true);

                return subAssetsOfType[0];
            }
        }

        public static Object EnsureUniqueSubAssetBaseClass<TBase>(Type type, ScriptableObject parent,
            string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = type.Name;

            var allSubAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(parent));
            var assetsWithInterface =
                allSubAssets.Where(asset => typeof(TBase).IsAssignableFrom(asset.GetType())).ToList();

            for (int i = assetsWithInterface.Count - 1; i >= 0; i--)
            {
                if (!type.IsAssignableFrom(assetsWithInterface[i].GetType()))
                {
                    Object.DestroyImmediate(assetsWithInterface[i], true);
                    assetsWithInterface.RemoveAt(i);
                }
            }

            if (assetsWithInterface.Count == 0)
            {
                return CreateSubAsset(type, parent);
            }
            else
            {
                if (assetsWithInterface.Count > 1)
                    EditorUtility.SetDirty(parent);

                while (assetsWithInterface.Count > 1) Object.DestroyImmediate(assetsWithInterface[^1], true);

                return assetsWithInterface[0];
            }
        }

        public static void RemoveSubAssets<T>(ScriptableObject parent, bool save = true) where T : Object
        {
            string path = AssetDatabase.GetAssetPath(parent);
            var subAssets = AssetDatabase.LoadAllAssetsAtPath(path);
            foreach (var asset in subAssets)
            {
                if (asset is T && asset != parent)
                    Object.DestroyImmediate(asset, true);
            }

            if (save)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.ImportAsset(path);
            }
        }
    }
#endif
}