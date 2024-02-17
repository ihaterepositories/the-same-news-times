using System;
using System.Collections;
using AnimationsScripts;
using DG.Tweening;
using Spawners;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class StartLevelController : MonoBehaviour
    {
        [SerializeField] private LevelSpawner levelSpawner;
        [SerializeField] private InfoText mazeInfoText;
        [SerializeField] private InfoText levelDescriptionText;
        [SerializeField] private InfoText levelRarityText;

        public static event Action OnAllSpawned;

        private void Awake()
        {
            levelDescriptionText.GetComponent<Text>().DOFade(0f, 0f);
        }

        private void Start()
        {
            levelSpawner.Spawn();
            SetMazeInfoText();
            SetLevelDescriptionText();
            SetLevelRarityText();
            OnAllSpawned?.Invoke();
        }

        private void OnEnable()
        {
            FinishLevelController.OnReadyToStartNewLevel += InitializeLevel;
        }

        private void OnDisable()
        {
            FinishLevelController.OnReadyToStartNewLevel -= InitializeLevel;
        }

        private void InitializeLevel()
        {
            levelSpawner.Spawn();
            SetMazeInfoText();
            SetLevelDescriptionText();
            SetLevelRarityText();
            levelDescriptionText.GetComponent<Text>().DOFade(1f, 0.5f);
            StartCoroutine(StartingLevelCoroutine());
        }

        private IEnumerator StartingLevelCoroutine()
        {
            yield return new WaitForSeconds(2.5f);
            levelDescriptionText.GetComponent<Text>().DOFade(0f, 0.15f);
            CircleAnimation.Instance.Decrease(0.2f);
            OnAllSpawned?.Invoke();
        }

        private void SetMazeInfoText()
        {
            mazeInfoText.SetText(
                $"maze size: {levelSpawner.MazeWidth}x{levelSpawner.MazeHeight}  /" +
                $"  cycles: {levelSpawner.MazeCyclesCount}  /" +
                $"  green treasures: {levelSpawner.MazeGreenScoresCount}"
            );
        }

        private void SetLevelDescriptionText()
        {
            levelDescriptionText.SetText(levelSpawner.LevelDescription);
        }

        private void SetLevelRarityText()
        {
            levelRarityText.SetText(levelSpawner.RarityDescription);

            switch (levelSpawner.RarityDescription)
            {
                case "Legendary": levelRarityText.SetColor(RarityColors.Legendary); break;
                case "Epic": levelRarityText.SetColor(RarityColors.Epic); break;
                case "Rare": levelRarityText.SetColor(RarityColors.Rare); break;
                case "Default": levelRarityText.SetColor(RarityColors.Default); break;
                default: levelRarityText.SetColor(Color.red); break;
            }
        }
    }
}
