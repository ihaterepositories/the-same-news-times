using System.Collections;
using DataModels;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;

namespace Requests
{
    public class UpdateBestRecordRequest
    {
        public IEnumerator UpdateRecord(BestRecordData bestRecordData, System.Action<StatusCode, string> callback)
        {
            var apiUrl = "http://localhost:5139/api/BestRecord/Update";

            var jsonData = JsonUtility.ToJson(bestRecordData);

            using var request = new UnityWebRequest(apiUrl, "PUT");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(StatusCode.Success, "Successfully updated record.");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                callback?.Invoke(StatusCode.Failure, "Failed to update record. Protocol Error.");
            }
            else if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                callback?.Invoke(StatusCode.Failure, "Failed to update record. Connection Error.");
            }
        }

    }
}