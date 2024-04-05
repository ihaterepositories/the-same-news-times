using System.Collections.Generic;
using DataModels;
using Newtonsoft.Json;
using Requests;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class BestPlayersText : MonoBehaviour
    {
        [SerializeField] private Text usernamesText;
        [SerializeField] private Text totalScoresText;
        
        private BestPlayersRequest _bestPlayersRequest;
        private ScoresFormatter _scoresFormatter;

        [Inject]
        private void Construct(BestPlayersRequest bestPlayersRequest, ScoresFormatter scoresFormatter)
        {
            _bestPlayersRequest = bestPlayersRequest;
            _scoresFormatter = scoresFormatter;
        }
        
        private void Start()
        {
            StartCoroutine(_bestPlayersRequest.GetCoroutine(ProcessBestPlayersRequestResult));
        }

        private void ProcessBestPlayersRequestResult(string jsonData)
        {
            usernamesText.text = "username" + "\n" + "\n";
            totalScoresText.text = "total score" + "\n" + "\n";
            
            if (string.IsNullOrEmpty(jsonData))
            {
                for (var i = 0; i < 5; i++)
                {
                    usernamesText.text += "not available" + "\n" + "----------------" + "\n";
                    totalScoresText.text += "not available" + "\n" + "----------------" + "\n";
                }
                return;
            }

            var playersData = JsonConvert.DeserializeObject<List<BestPlayerData>>(jsonData);
            foreach (var playerData in playersData)
            {
                usernamesText.text += playerData.name + "\n" + "----------------" + "\n";
                totalScoresText.text += _scoresFormatter.FormatNumber(playerData.totalScore) + "\n" + "----------------" + "\n";
            }
        }
    }
}