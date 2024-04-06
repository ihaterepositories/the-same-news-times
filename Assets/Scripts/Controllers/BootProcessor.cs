using System;
using System.Collections.Generic;
using Loaders;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Controllers
{
    public class BootProcessor : MonoBehaviour
    {
        [SerializeField] private Text loadingText;
        [SerializeField] private GameObject pressKeyToContinueText;
        [SerializeField] private List<AssetReference> prefabReferences;
        
        private PrefabsLoader _prefabsLoader;
        
        [Inject]
        private void Construct(PrefabsLoader prefabsLoader)
        {
            _prefabsLoader = prefabsLoader;
            _prefabsLoader.LoadPrefabs(prefabReferences);
        }

        private void Start()
        {
            pressKeyToContinueText.SetActive(false);
        }

        private void OnEnable()
        {
            _prefabsLoader.OnLoadingProgressChanged += SetLoadingText;
            _prefabsLoader.OnAllPrefabsLoaded += EnableNextSceneLoading;
        }

        private void OnDisable()
        {
            _prefabsLoader.OnLoadingProgressChanged -= SetLoadingText;
            _prefabsLoader.OnAllPrefabsLoaded -= EnableNextSceneLoading;
        }

        private void SetLoadingText(float progress)
        {
            loadingText.text = $"loading some amazing pixel stuff... {progress * 100}%";
        }
        
        private void EnableNextSceneLoading()
        {
            pressKeyToContinueText.SetActive(true);
        }
    }
}