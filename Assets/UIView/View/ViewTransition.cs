using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UIView
{
    [Serializable]
    public abstract class ViewTransition : ScriptableObject
    {
        public abstract UniTask Show(View view);
        public abstract UniTask Hide(View view);
        
        public static void SetView(View view, bool enabled)
        {
            view.CanvasGroup.interactable = enabled;
            view.CanvasGroup.blocksRaycasts = enabled;
            view.CanvasGroup.gameObject.SetActive(enabled);
        }
    }
}