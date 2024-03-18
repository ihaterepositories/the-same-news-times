using System;
using UnityEngine.Serialization;

namespace DataModels
{
    [System.Serializable]
    public class PlayerData
    {
        public string id;
        public string name;
        public string email;
        public string password;
        public string bestRecordId;
    }
}