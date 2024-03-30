using DataModels;
using Requests;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class BestPlayerText : MonoBehaviour
    {
        [SerializeField] private Text bestPlayerText;
        private BestPlayerRequest _bestPlayerRequest;
        
        [Inject]
        private void Construct(BestPlayerRequest bestPlayerRequest)
        {
            _bestPlayerRequest = bestPlayerRequest;
        }
        
        private void Start()
        {
            StartCoroutine(_bestPlayerRequest.GetCoroutine(ProcessBestPlayerRequestResult));
        }
        
        private void ProcessBestPlayerRequestResult(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                bestPlayerText.text = string.Empty;
                return;
            }
            
            var playerData = JsonUtility.FromJson<BestPlayerData>(jsonData);
            bestPlayerText.text = "best player: " + playerData.totalScore + " of total score," + " username: " + playerData.name;
        }
    }
}
