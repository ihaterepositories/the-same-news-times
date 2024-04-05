using System;
using System.Collections;
using Animations;
using Constants;
using DG.Tweening;
using Spawners;
using UI;
using UI.TextControllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.InGameControllers
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private InfoText mazeInfoText;
        [SerializeField] private InfoText levelDescriptionText;
        [SerializeField] private InfoText levelRarityText;

        private LevelSpawner _levelSpawner;
        
        public static event Action OnAllSpawned;

        [Inject]
        private void Construct(LevelSpawner levelSpawner)
        {
            _levelSpawner = levelSpawner;
        }
        
        private void Awake()
        {
            levelDescriptionText.GetComponent<Text>().DOFade(0f, 0f);
        }

        private void Start()
        {
            _levelSpawner.Spawn();
            SetMazeInfoText();
            SetLevelDescriptionText();
            SetLevelRarityText();
            OnAllSpawned?.Invoke();
        }

        private void OnEnable()
        {
            LevelFinisher.OnReadyToStartNewLevel += InitializeLevel;
        }

        private void OnDisable()
        {
            LevelFinisher.OnReadyToStartNewLevel -= InitializeLevel;
        }

        private void InitializeLevel()
        {
            _levelSpawner.Spawn();
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
                $"maze size: {_levelSpawner.MazeWidth}x{_levelSpawner.MazeHeight}  /" +
                $"  cycles: {_levelSpawner.MazeCyclesCount}  /" +
                $"  green treasures: {_levelSpawner.MazeGreenScoresCount}"
            );
        }

        private void SetLevelDescriptionText()
        {
            levelDescriptionText.SetText(_levelSpawner.LevelDescription);
        }

        private void SetLevelRarityText()
        {
            levelRarityText.SetText(_levelSpawner.RarityDescription);

            switch (_levelSpawner.RarityDescription)
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
