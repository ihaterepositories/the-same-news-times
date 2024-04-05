using DataModels;
using Enums;
using Requests;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers
{
    public class AuthorizationProcessor : MonoBehaviour
    {
        [SerializeField] private InputField nameInputField;
        [SerializeField] private InputField emailInputField;
        [SerializeField] private InputField passwordInputField;
        [SerializeField] private Text requestStatusText;
        [SerializeField] private InfoText loggedInUserText;
        
        private RegisterPlayerRequest _registerPlayerRequest;
        private LoginPlayerRequest _loginPlayerRequest;

        [Inject]
        private void Construct(RegisterPlayerRequest registerPlayerRequest, LoginPlayerRequest loginPlayerRequest)
        {
            _registerPlayerRequest = registerPlayerRequest;
            _loginPlayerRequest = loginPlayerRequest;
        }
        
        private void Start()
        {
            loggedInUserText.SetText(
                "Last logged in user: " + PlayerPrefs.GetString("username", "you are not logged in"));
        }

        public void SendRegistrationRequest()
        {
            var playerRegistrationData = new PlayerRegistrationData
            {
                id = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                name = nameInputField.text,
                email = emailInputField.text,
                password = passwordInputField.text,
                bestRecordId = "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            };
            
            if (string.IsNullOrEmpty(playerRegistrationData.name) || 
                string.IsNullOrEmpty(playerRegistrationData.email) || 
                string.IsNullOrEmpty(playerRegistrationData.password))
            {
                SetRequestText("Please fill all fields");
                return;
            }

            if (playerRegistrationData.name.Length > 15)
            {
                SetRequestText("Name is too long (max 15 characters)");
                return;
            }
            
            if (playerRegistrationData.password.Length < 6)
            {
                SetRequestText("Password is too short (min 6 characters)");
                return;
            }
            
            StartCoroutine(_registerPlayerRequest.PostRequestCoroutine(playerRegistrationData, ProcessRegistrationRequestResult));
        }
        
        public void SendLoginRequest()
        {
            var playerLoginData = new PlayerLoginData
            {
                username = nameInputField.text,
                password = passwordInputField.text
            };
            
            StartCoroutine(_loginPlayerRequest.PostRequestCoroutine(playerLoginData, ProcessLoginRequestResult));
        }
        
        private void ProcessRegistrationRequestResult(StatusCode statusCode, string message)
        {
            if (statusCode == StatusCode.Success)
            {
                var playerData = JsonUtility.FromJson<PlayerData>(message);
                SavePlayerData(playerData);
                
                loggedInUserText.SetText(
                    "Last logged in user: " + playerData.name);
                
                SetRequestText("Registration successful!");
            }
            else { SetRequestText(message); }
        }
        
        private void ProcessLoginRequestResult(StatusCode statusCode, string message)
        {
            if (statusCode == StatusCode.Success)
            {
                var playerData = JsonUtility.FromJson<PlayerData>(message);
                SavePlayerData(playerData);
                
                loggedInUserText.SetText(
                    "Last logged in user: " + playerData.name);
                
                SetRequestText("Login successful!");
            }
            else { SetRequestText(message); }
        }
        
        private void SavePlayerData(PlayerData playerData)
        {
            PlayerPrefs.SetString("username", playerData.name);
            PlayerPrefs.SetString("playerId", playerData.id);
            PlayerPrefs.SetString("bestRecordId", playerData.bestRecordId);
            PlayerPrefs.Save();
        }
        
        private void SetRequestText(string text)
        {
            requestStatusText.text = text;
        }
    }
}
