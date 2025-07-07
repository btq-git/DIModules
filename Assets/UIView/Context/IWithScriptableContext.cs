using UnityEngine;

namespace UIView
{
    public interface IWithScriptableContext<in T> where T : ScriptableObject
    {
        public void Set(T viewContext);
    }
}