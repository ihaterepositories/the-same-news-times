using System.Collections;
using DataModels;
using Enums;
using UnityEngine;
using UnityEngine.Networking;

namespace Requests
{
    public class RegisterPlayerRequest
    {
        public IEnumerator PostRequestCoroutine(PlayerRegistrationData playerRegistrationData, System.Action<StatusCode, string> callback)
        {
            var url = "http://localhost:5139/api/Player/Register";
            
            var jsonData = JsonUtility.ToJson(playerRegistrationData);
            
            using var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(StatusCode.Success, request.downloadHandler.text);
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                callback?.Invoke(StatusCode.Failure, "Player with this username already exists!");
            }
            else if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                callback?.Invoke(StatusCode.Failure, "Server is not available, but you can play offline!");
            }
        }
    }
}
