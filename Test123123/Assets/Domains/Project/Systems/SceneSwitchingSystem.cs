using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Systems
{
    public class SceneSwitchingSystem : IInitializable
    {
        [Inject] private Dictionary<SceneID, string> _scenes;
        [Inject] private SceneSwitchingController.Factory _factory;
        
        private SceneSwitchingController _controller;

        public void Initialize()
        {
            _controller = _factory.Create();
        }
        
        public async UniTask LoadSceneAsync(SceneID sceneID)
        {
            await _controller.FadeOutAsync();
            await SceneManager.LoadSceneAsync(_scenes[sceneID]);
            await _controller.FadeInAsync();
        }
    }
}