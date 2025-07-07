using System;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace UIView
{
    [Serializable]
    public sealed class FadeViewTransition : ViewTransition
    {
        [SerializeField]
        private float duration = 0.2f;
        
        [SerializeField]
        private Ease ease;

        public override UniTask Show(View view)
        {
            CanvasGroup canvasGroup = view.CanvasGroup;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            return LMotion.Create(canvasGroup.alpha, 1f, duration)
                .WithEase(ease)
                .WithScheduler(MotionScheduler.PreLateUpdateIgnoreTimeScale)
                .BindToAlpha(canvasGroup)
                .AddTo(view)
                .ToUniTask();
        }

        public override UniTask Hide(View view)
        {
            CanvasGroup canvasGroup = view.CanvasGroup;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            
            return LMotion.Create(canvasGroup.alpha, 1f, duration)
                .WithEase(ease)
                .WithScheduler(MotionScheduler.PreLateUpdateIgnoreTimeScale)
                .BindToAlpha(canvasGroup)
                .AddTo(view)
                .ToUniTask();
        }
    }
}