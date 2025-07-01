using UnityEditor.Overlays;

namespace ClockApp.PlayerData
{
    public static class StaticDefine
    {
        public static readonly string PlayerData = "PlayerData";
        public static readonly int Change2HourStandrard = 99 * 60 * 60;
    }

    public class PlayerDataManager
    {
        private static PlayerDataManager _instance;

        public static PlayerDataManager Instance
        {
            get { return _instance ??= new PlayerDataManager(); }
        }

        private static PlayerInfo _playerInfo;

        public PlayerInfo PlayerInfo
        {
            get
            {
                if (_playerInfo == null)
                {
                    TryLoadData(StaticDefine.PlayerData, out _playerInfo);
                }

                return _playerInfo;
            }
        }

        private PlayerInfo LoadData(string fileName)
        {
            var data = JsonMgr.Instance.LoadData<PlayerInfo>(fileName);
            return data;
        }

        public bool TryLoadData(string fileName, out PlayerInfo data)
        {
            var tempData = JsonMgr.Instance.LoadData<PlayerInfo>(fileName);
            if (tempData == null)
            {
                data = new PlayerInfo();
                return false;
            }

            data = tempData;
            return true;
        }

        public void SaveData(string fileName = null)
        {
            if (fileName == null)
            {
                fileName = StaticDefine.PlayerData;
            }

            JsonMgr.Instance.SaveData(_playerInfo, fileName);
        }

        public void AddType(string typeName)
        {
            if (!_playerInfo.timeInfo.ContainsKey(typeName))
            {
                _playerInfo.timeInfo.Add(typeName, 0);
            }
        }

        public void AddTime(string typeName, float time)
        {
            if (_playerInfo.timeInfo.ContainsKey(typeName))
            {
                _playerInfo.timeInfo[typeName] += time;
            }
        }

        public float GetTime(string typeName)
        {
            if (_playerInfo.timeInfo.ContainsKey(typeName))
            {
                return _playerInfo.timeInfo[typeName];
            }

            return -1;
        }
    }
}