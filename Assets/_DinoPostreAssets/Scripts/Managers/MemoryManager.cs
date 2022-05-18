using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Dinopostres.Definitions;
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
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(pathCombined);
            PlayerData newGameData = new PlayerData(_gameName);
            bf.Serialize(file, newGameData);
            file.Close();
            Debug.Log(pathCombined);

            return newGameData;
        }

        public static void SaveGame(PlayerData _data)
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            string pathCombined = Path.Combine(
                Application.persistentDataPath,
                _data.ID + ".data");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(pathCombined);
            bf.Serialize(file, _data);
            file.Close();
            Debug.Log(pathCombined);
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
                //Debug.Log(gm.DinoInventory[0].ID);

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
                pathCombined = Path.Combine(Application.persistentDataPath,str);
                if (File.Exists(pathCombined))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(pathCombined, FileMode.Open);
                    PlayerData gm = (PlayerData)bf.Deserialize(file);
                    file.Close();
                    if(!string.IsNullOrEmpty(gm.ID))
                        games.Add(gm);
                }
            }
            
            return games;
        }

        public static  string [] GetGameName()
        {
            return Directory.GetFiles(Application.persistentDataPath);
        }
    }
}