﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Loaders
{
    public class PrefabsLoader
    {
        private readonly Dictionary<string, GameObject> _loadedPrefabs = new Dictionary<string, GameObject>();
        private int _totalPrefabsToLoad;
        private int _loadedPrefabsCount;
        
        public event Action<float> OnLoadingProgressChanged;
        public event Action OnAllPrefabsLoaded;

        public void LoadPrefabs(List<AssetReference> prefabReferences)
        {
            _totalPrefabsToLoad = prefabReferences.Count;

            foreach (var handle in prefabReferences.Select(reference => reference.LoadAssetAsync<GameObject>()))
            {
                handle.Completed += op =>
                {
                    _loadedPrefabs[op.Result.name] = op.Result;
                    _loadedPrefabsCount++;
                    OnLoadingProgressChanged?.Invoke((float)_loadedPrefabsCount / _totalPrefabsToLoad);

                    if (_loadedPrefabsCount == _totalPrefabsToLoad)
                    {
                        _loadedPrefabsCount = 0;
                        OnAllPrefabsLoaded?.Invoke();
                    }
                };
            }
        }
        
        public async Task<GameObject> LoadPrefab(AssetReference prefabReference)
        {
            var handle = prefabReference.LoadAssetAsync<GameObject>();
            await handle.Task;
            _loadedPrefabs[handle.Result.name] = handle.Result;
            return handle.Result;
        }

        public GameObject GetPrefab(string prefabName)
        {
            if (_loadedPrefabs.TryGetValue(prefabName, out var prefab))
            {
                return prefab;
            }

            Debug.LogError($"Prefab with name {prefabName} not found");
            return null;
        }

        public void ReleaseAll()
        {
            foreach (var prefab in _loadedPrefabs.Values)
            {
                Addressables.ReleaseInstance(prefab);
            }
            _loadedPrefabs.Clear();
        }
        
        public void PrintLoadedPrefabs()
        {
            foreach (var prefab in _loadedPrefabs)
            {
                Debug.Log($"Prefab ID: {prefab.Key}, Prefab Name: {prefab.Value.name}");
            }
        }
    }
}