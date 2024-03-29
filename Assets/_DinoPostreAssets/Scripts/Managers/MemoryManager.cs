using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Dinopostres.Definitions;
using System.Runtime.Serialization;

namespace Dinopostres.Managers
{
    public class MemoryManager : MonoBehaviour
    {
        public static PlayerData NewGame(string _gameName)
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            string pathCombined = Path.Combine(
                Application.persistentDataPath,
                _gameName + ".data");
            Debug.Log(pathCombined);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(pathCombined);
            PlayerData newGameData = new PlayerData(_gameName);
            bf.Serialize(file, newGameData);
            file.Close();
            return newGameData;
        }

        public static void DeleteGame(string _gameName)
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            string pathCombined = Path.Combine(
                Application.persistentDataPath,
                _gameName + ".data");

            File.Delete(pathCombined);
        }

        public static void SaveGame(PlayerData _data)
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            string pathCombined = Path.Combine(
                Application.persistentDataPath,
                _data._ID + ".data");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(pathCombined);
            bf.Serialize(file, _data);
            file.Close();
            //Debug.Log(pathCombined);
        }

        public static PlayerData LoadGame(string _gameName)
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            string pathCombined = Path.Combine(
                Application.persistentDataPath,
                _gameName + ".data");

            if (File.Exists(pathCombined))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(pathCombined, FileMode.Open);
                PlayerData gm = (PlayerData)bf.Deserialize(file);
                file.Close();

                return gm;
            }
            return new PlayerData();
        }

        public static List<PlayerData> LoadGames()
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            List<PlayerData> games = new List<PlayerData>();
            string pathCombined = Application.persistentDataPath;
            string [] gameNames=Directory.GetFiles(pathCombined);
            foreach(string str in gameNames)
            {
                pathCombined = Path.Combine(Application.persistentDataPath, str);
                try
                {
                    if (File.Exists(pathCombined))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file = File.Open(pathCombined, FileMode.Open);
                        PlayerData gm = (PlayerData)bf.Deserialize(file);
                        file.Close();
                        if (!string.IsNullOrEmpty(gm._ID))
                            games.Add(gm);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning($"unable to deserialize file {str} because: \n {e.Message}");
                }
            }

            return games;
        }

        public static int GamesCount()
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            List<PlayerData> games = new List<PlayerData>();
            string pathCombined = Application.persistentDataPath;
            string[] gameNames = Directory.GetFiles(pathCombined);

            foreach (string str in gameNames)
            {
                pathCombined = Path.Combine(Application.persistentDataPath, str);
                try
                {
                    if (File.Exists(pathCombined))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file = File.Open(pathCombined, FileMode.Open);
                        PlayerData gm = (PlayerData)bf.Deserialize(file);
                        file.Close();
                        if (!string.IsNullOrEmpty(gm._ID))
                            games.Add(gm);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning($"unable to deserialize file {str} because: \n {e.Message}");
                }
            }

            return games.Count;
        }

        public static  string [] GetGameName()
        {
            return Directory.GetFiles(Application.persistentDataPath);
        }
    }
}