using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UIView
{
    public sealed class NoViewTransition : ViewTransition
    {
        public override UniTask Show(View view)
        {
            SetView(view, true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide(View view)
        {
            SetView(view, false);
            return UniTask.CompletedTask;
        }
    }
}