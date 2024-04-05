using System;
using System.Collections;
using AnimationControllers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class ScenesLoader
    {
        public event Action OnSceneLoaded; 
        
        public IEnumerator LoadSceneCoroutine(string sceneAddress, bool isAnimated)
        {
            if (isAnimated) { CircleAnimation.Instance.Increase(); }
            
            yield return new WaitForSeconds(1f);
            
            LoadScene(sceneAddress);
        }
        
        private async void LoadScene(string sceneAddress)
        {
            var handle = Addressables.LoadSceneAsync(sceneAddress);

            await handle.Task;
            
            var loadedScene = handle.Result.Scene;
            SceneManager.SetActiveScene(loadedScene);
            
            OnSceneLoaded?.Invoke();
        }
    }
}
