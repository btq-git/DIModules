using System.Collections.Generic;
using UnityEngine;

namespace BTQ.Core.Application
{
    [CreateAssetMenu(menuName = "BTQ/Module/List", order = -1)]
    public class ApplicationModulesList : ScriptableObject
    {
        [SerializeField]
        public List<ModuleConfig> modules;
    }
}