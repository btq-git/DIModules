using Reflex.Attributes;
using UnityEngine;

namespace UIView
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class View : MonoBehaviour
    {
        [SerializeField]
        private ViewTransition customTransition;
        [Inject]
        private ViewTransition defaultTransition;

        private ViewTransition Transition => customTransition ?? defaultTransition;

        [Inject]
        private UIModule UI { get; }

        [field: SerializeField]
        public CanvasGroup CanvasGroup { get; private set; }

        public bool IsShown { get; private set; }
        public bool IsShowing { get; private set; }
        public bool IsHiding { get; private set; }
        public bool IsInTransition => IsShowing || IsHiding;

        private void Awake()
        {
            if (!IsShown && !IsShowing)
                ViewTransition.SetView(this, false);
        }

        public void UpdateActive()
        {
            
        }

        public void UpdateCurrent()
        {
            
        }

        private void OnValidate()
        {
            if (!CanvasGroup)
                CanvasGroup = GetComponent<CanvasGroup>();
        }

        // Should be called only from manager to ensure correct context
        internal void Show()
        {
            transform.SetAsLastSibling();
            OnShowAnimationStarted();
            Transition.Show(this);
        }

        public void Hide()
        {
            UI.Hide(this);
        }

        internal void HideInternal()
        {
            OnHideAnimationStarted();
            Transition.Hide(this);
        }

        protected virtual void OnShowAnimationStarted()
        {
            IsShowing = true;
            IsShown = true;
            gameObject.SetActive(true);
        }

        protected virtual void OnShowAnimationFinished()
        {
            IsShowing = false;
        }

        protected virtual void OnHideAnimationStarted()
        {
            IsHiding = true;
            IsShown = false;
        }

        protected virtual void OnHideAnimationFinished()
        {
            IsHiding = false;
            gameObject.SetActive(false);
        }
    }
}