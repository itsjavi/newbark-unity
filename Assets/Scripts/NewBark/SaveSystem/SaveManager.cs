using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NewBark.Player;
using UnityEngine;

namespace NewBark.SaveSystem
{
    public static class SaveManager
    {
        private static string _playerStateFile = Application.persistentDataPath + "/newbark-player.sav";
            
        public static void SavePlayerState(Player.Player player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_playerStateFile, FileMode.Create);
            PlayerState data = new PlayerState(player);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static PlayerState LoadPlayerState()
        {
            if (File.Exists(_playerStateFile))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(_playerStateFile, FileMode.Open);
                var data = formatter.Deserialize(stream) as PlayerState;
                stream.Close();
                return data;
            }

            return null;
        }
    }
}
