using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UIView;
using UnityEngine;

namespace _Testing.Scripts
{
    public class Tesst : MonoBehaviour
    {
        [Inject]
        private UIModule ui;

        private async void Start()
        {
            await UniTask.Delay(1000, true);
            ui.ShowWithoutContext<TestView>();
        }
    }
}