using System;
using System.Collections;
using Animations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

namespace Loaders
{
    public class ScenesLoader
    {
        private SceneLoadingAnimation _sceneLoadingAnimation;
        public event Action OnSceneLoaded; 
        
        [Inject]
        private void Construct(SceneLoadingAnimation sceneLoadingAnimation)
        {
            _sceneLoadingAnimation = sceneLoadingAnimation;
        }
        
        public IEnumerator LoadSceneCoroutine(string sceneAddress, bool isAnimated)
        {
            if (isAnimated) { _sceneLoadingAnimation.Increase(); }
            
            yield return new WaitForSeconds(1f);
            
            LoadScene(sceneAddress);
        }
        
        private async void LoadScene(string sceneAddress)
        {
            UnloadUnusedScenes();
            
            var handle = Addressables.LoadSceneAsync(sceneAddress);
            await handle.Task;
            
            var loadedScene = handle.Result.Scene;
            SceneManager.SetActiveScene(loadedScene);
            
            OnSceneLoaded?.Invoke();
        }
        
        private void UnloadUnusedScenes()
        {
            for (var i = SceneManager.sceneCount - 1; i >= 0; i--)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.isLoaded && scene != SceneManager.GetActiveScene())
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
}
