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

        [Inject]
        private void Construct(BestPlayersRequest bestPlayersRequest)
        {
            _bestPlayersRequest = bestPlayersRequest;
        }
        
        private void Start()
        {
            StartCoroutine(_bestPlayersRequest.GetCoroutine(ProcessBestPlayersRequestResult));
        }

        private void ProcessBestPlayersRequestResult(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                usernamesText.text = "usernames" + "\n" + "server is not available";
                totalScoresText.text = "total scores" + "\n" + "server is not available";
                return;
            }
            
            usernamesText.text = "usernames" + "\n" + "\n";
            totalScoresText.text = "total scores" + "\n" + "\n";

            var playersData = JsonConvert.DeserializeObject<List<BestPlayerData>>(jsonData);
            foreach (var playerData in playersData)
            {
                usernamesText.text += playerData.name + "\n" + "----------------" + "\n";
                totalScoresText.text += playerData.totalScore + "\n" + "----------------" + "\n";
            }
        }
    }
}