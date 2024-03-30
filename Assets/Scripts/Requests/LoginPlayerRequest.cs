using System.Collections;
using DataModels;
using UnityEditor.PackageManager;
using UnityEngine.Networking;

namespace Requests
{
    public class LoginPlayerRequest
    {
        public IEnumerator PostRequestCoroutine(PlayerLoginData playerLoginData, System.Action<StatusCode, string> callback)
        {
            var url = "http://localhost:5139/api/Player/Login";
            url += "?username=" + playerLoginData.username + "&password=" + playerLoginData.password;

            using var request = UnityWebRequest.Get(url);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(StatusCode.Success, request.downloadHandler.text);
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                callback?.Invoke(StatusCode.Failure, "Wrong username or password!");
            }
            else if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                callback?.Invoke(StatusCode.Failure, "Server is not available, but you can play offline!");
            }
        }
    }
}