using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace NewBark.State
{
    public static class SaveManager
    {
        private const string SaveFilePrefix = "newbark-gamedata_v";
        private const string SaveFileSuffix = ".sav";

        private static string GetSaveFileName(int schemaVersion)
        {
            return Path.Combine(Application.persistentDataPath, SaveFilePrefix + schemaVersion + SaveFileSuffix);
        }

        private static string GetSaveFileName()
        {
            return GetSaveFileName(GameData.SchemaVersion);
        }

        private static string FindSaveFileName()
        {
            for (var i = GameData.SchemaVersion; i >= GameData.MinCompatibleSchemaVersion; i--)
            {
                var fileName = GetSaveFileName(i);

                if (File.Exists(fileName))
                {
                    return fileName;
                }
            }

            return null;
        }

        public static void Save(GameData data)
        {
            var stream = new FileStream(GetSaveFileName(), FileMode.Create);

            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            stream.Close();

            Debug.Log("Game SAVED. " + GetPlayTime(data));
        }

        public static GameData Load()
        {
            var fileName = FindSaveFileName();
            if (fileName == null) return null;

            var stream = new FileStream(fileName, FileMode.Open);
            if (stream.Length == 0)
            {
                stream.Close();
                return null;
            }

            var formatter = new BinaryFormatter();
            var data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            Debug.Log("Game LOADED. " + GetPlayTime(data));
            return data;
        }

        private static string GetPlayTime(GameData data)
        {
            var t = TimeSpan.FromSeconds(data.playTime);
            return "Play time: " + string.Format(
                       "{0:D2} hours, {1:D2} minutes, {2:D2} seconds, {3:D3} ms",
                       t.Hours,
                       t.Minutes,
                       t.Seconds,
                       t.Milliseconds
                   );
        }
    }
}
