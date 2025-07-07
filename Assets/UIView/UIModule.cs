using System;
using System.Collections.Generic;
using BTQ.Core.Application;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UIView.WithContext;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIView
{
    public class UIModule : ModuleBase<UIModuleConfig>
    {
        private Dictionary<Type, View> instantiatedViews = new();
        private List<View> createdDynamicViews = new();
        
        private List<View> shownViews = new ();
        
        private Scene uiScene;
        private Transform viewsParent;
        
        public UIModule(UIModuleConfig config, Container container) : base(config)
        {
            uiScene = SceneManager.CreateScene("UI");
            
            var parent = GameObject.Instantiate(config.canvas);
            SceneManager.MoveGameObjectToScene(parent.gameObject, uiScene);
            viewsParent = parent.transform;
            
            foreach (var viewPrefab in config.views)
            {
                instantiatedViews.Add(viewPrefab.GetType(), CreateView(viewPrefab, container));
            }
        }

        public T ShowWithoutContext<T>() where T : View, IWithoutContext
        {
            var view = GetCreatedView<T>();
            if (view == null)
                return null;
            SetViewVisibility(view, true);
            return view;
        }

        public T ShowWithScriptable<T, TData>(TData data) where T : View, IWithScriptableContext<TData>
            where TData : ScriptableObject
        {
            var view = GetCreatedView<T>();
            if (view == null)
                return null;
            view.Set(data);
            SetViewVisibility(view, true);
            return view;
        }

        public T ShowWithData<T, TData>(TData data) where T : View, IWithDynamicContext<TData>
        {
            var view = GetCreatedView<T>();
            if (view == null)
                return null;
            view.Set(data);
            SetViewVisibility(view, true);
            return view;
        }

        public void Hide<T>() where T :View
        {
            var view = shownViews.Find(view => view.GetType() == typeof(T));
            if (view == null)
                return;
            
            Hide(view);
        }

        public void Hide(View view)
        {
            SetViewVisibility(view, false);
        }

        public void HideCurrent()
        {
            if (shownViews.Count > 0)
                Hide(shownViews[^1]);
        }

        private void SetViewVisibility(View view, bool visible)
        {
            if (visible)
            {
                if (!shownViews.Contains(view))
                {
                    view.Show();
                    shownViews.Add(view);
                }
                view.transform.SetAsLastSibling();
            }
            else
            {
                if (shownViews.Contains(view))
                {
                    view.HideInternal();
                    shownViews.Remove(view);
                }
            }
        }
        
        private T GetCreatedView<T>() where T : View
        {
            if (instantiatedViews.TryGetValue(typeof(T), out var instantiatedView))
            {
                return instantiatedView as T;
            }
            
            return null;
        }

        private View CreateView(View viewPrefab, Container container)
        {
            var newView = GameObject.Instantiate(viewPrefab, Vector3.zero, Quaternion.identity, viewsParent);
            newView.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            newView.gameObject.SetActive(false);
            GameObjectInjector.InjectRecursive(newView.gameObject, container);
            return newView;
        }
    }
}