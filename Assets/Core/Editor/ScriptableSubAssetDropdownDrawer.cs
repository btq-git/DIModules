#if UNITY_EDITOR
using System;
using System.Linq;
using BTQ.Core.Utils;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    // Used to get Reflection-based dropdown for scriptables, that are automatically added as sub assets
    // Remember to add [CustomPropertyDrawer(typeof(T), true)]
    public abstract class ScriptableSubAssetDropdownDrawer<T> : PropertyDrawer where T : UnityEngine.Object
    {
        private bool foldout = false;
        private string[] typeOptions;
        private Type[] transitionTypes;

        private void GetTypeIfNecessary()
        {
            transitionTypes ??= AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .ToArray();
            typeOptions ??= new[] { "Default" }.Concat(transitionTypes.Select(t => t.Name)).ToArray();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var transition = property.objectReferenceValue as T;
            GetTypeIfNecessary();

            EditorGUI.BeginProperty(position, label, property);

            float lineHeight = EditorGUIUtility.singleLineHeight;
            Rect lineRect = new(position.x, position.y, position.width, lineHeight);

            // Foldout toggle (only if transition is assigned)
            if (transition != null)
            {
                var foldoutRect = new Rect(lineRect.x, lineRect.y, 18, lineHeight);
                foldout = EditorGUI.Foldout(foldoutRect, foldout, GUIContent.none, true);
            }

            // Dropdown with label inline
            int currentIndex = transition == null
                ? 0
                : 1 + Array.FindIndex(transitionTypes, t => t == transition.GetType());

            int newIndex = EditorGUI.Popup(lineRect, label.text, currentIndex, typeOptions);

            // If changed, create or replace sub asset
            if (newIndex != currentIndex)
            {
                var parentSO = GetParentScriptableObject(property);
                if (parentSO != null)
                {
                    if (newIndex == 0)
                    {
                        SubAssetUtils.RemoveSubAssets<T>(parentSO);
                        property.objectReferenceValue = null;
                    }
                    else
                    {
                        var typeToCreate = transitionTypes[newIndex - 1];
                        var instance = SubAssetUtils.EnsureUniqueSubAssetBaseClass<T>(typeToCreate, parentSO);
                        property.objectReferenceValue = instance;
                    }
                }
            }

            // Foldout body for settings
            if (transition != null && foldout)
            {
                var editor = UnityEditor.Editor.CreateEditor(transition);

                EditorGUI.indentLevel++;
                GUILayout.BeginVertical(EditorStyles.helpBox);
                editor.OnInspectorGUI();
                GUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        private static ScriptableObject GetParentScriptableObject(SerializedProperty property)
        {
            var target = property.serializedObject.targetObject;
            return target as ScriptableObject;
        }
    }
}
#endif