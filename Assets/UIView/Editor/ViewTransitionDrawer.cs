using Core.Editor;
using UnityEditor;

namespace UIView.Editor
{
    [CustomPropertyDrawer(typeof(ViewTransition), true)]
    public class ViewTransitionDrawer : ScriptableSubAssetDropdownDrawer<ViewTransition> { }
}