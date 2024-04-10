using System;
using System.Collections;
using Animations;
using Loaders;
using Models;
using UI.TextControllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.InGameControllers
{
    public class LevelFinisher : MonoBehaviour
    {
        [SerializeField] private InfoText gameOverText;
        [SerializeField] private ScoresCounter scoresCounter;
        [SerializeField] private Text pressAnyKeyText;

        private PrefabsLoader _prefabsLoader;
        private Player _player;
        private SceneLoadingAnimation _sceneLoadingAnimation;

        public static event Action OnLevelFinished;
        public static event Action OnReadyToStartNewLevel;
        public static event Action OnGameFinished;

        [Inject]
        private void Construct(PrefabsLoader prefabsLoader, Player player, SceneLoadingAnimation sceneLoadingAnimation)
        {
            _prefabsLoader = prefabsLoader;
            _player = player;
            _sceneLoadingAnimation = sceneLoadingAnimation;
        }
        
        private void Awake()
        {
            pressAnyKeyText.gameObject.SetActive(false);
        }

        private void Update()
        {
            EnableGameStopping();
        }

        private void OnEnable()
        {
            ScoresCounter.OnPinkScoreUpdated += FinishLevel;
            Inventory.OnLifeSaverUsed += FinishLevel;
            Player.OnDestroyedByEnemy += FinishGame;
            AfkDetector.OnPlayerIsAfk += FinishGame;
        }

        private void OnDisable()
        {
            ScoresCounter.OnPinkScoreUpdated -= FinishLevel;
            Inventory.OnLifeSaverUsed -= FinishLevel;
            Player.OnDestroyedByEnemy -= FinishGame;
            AfkDetector.OnPlayerIsAfk -= FinishGame;
        }

        private void EnableGameStopping()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FinishGame();
            }
        }

        private void FinishLevel()
        {
            StartCoroutine(FinishLevelCoroutine());
        }

        private IEnumerator FinishLevelCoroutine()
        {
            _sceneLoadingAnimation.Increase(0.2f);
            _player.ClearTrailRender();
            yield return new WaitForSeconds(0.2f);
            OnLevelFinished?.Invoke();
            yield return new WaitForSeconds(1.5f);
            OnReadyToStartNewLevel?.Invoke();
        }

        private void FinishGame()
        {
            StartCoroutine(FinishGameCoroutine());
        }

        private IEnumerator FinishGameCoroutine()
        {
            _sceneLoadingAnimation.Increase(0.2f);
            _player.ClearTrailRender();
            yield return new WaitForSeconds(1f);
            OnGameFinished?.Invoke();
            _prefabsLoader.ReleaseAll();
            gameOverText.SetText(scoresCounter.GetCurrentGameScore());
            scoresCounter.CalculateTotalTimeSpent();
            scoresCounter.UpdateOnlineBestScore();
            StartCoroutine(ActivateExitButtonCoroutine());
        }

        private IEnumerator ActivateExitButtonCoroutine()
        {
            yield return new WaitForSeconds(3f);
            pressAnyKeyText.gameObject.SetActive(true);
        }
    }
}
