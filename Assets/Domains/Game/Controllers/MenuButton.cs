using Cysharp.Threading.Tasks;
using Project.Systems;
using UnityEngine;
using Zenject;

namespace Domains.Game
{
    public class MenuButton : MonoBehaviour
    {
        [Inject] private SceneSwitchingSystem _sceneSwitchingSystem;

        public void BackToStartScreen()
        {
            _sceneSwitchingSystem.LoadSceneAsync(SceneID.StartScene).Forget();
        }
    }
}