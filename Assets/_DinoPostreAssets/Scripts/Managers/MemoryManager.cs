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
        public static void NewGame(string _gameName)
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            string pathCombined = Path.Combine(
                Application.persistentDataPath,
                _gameName + ".data");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(pathCombined);
            bf.Serialize(file, new PlayerData());
            file.Close();
            Debug.Log(pathCombined);
        }

        public static void SaveGame(string _gameName,PlayerData _data)
        {
            //Path pesistente del sistema en el que se guardan los datos del juego
            string pathCombined = Path.Combine(
                Application.persistentDataPath,
                _gameName + ".data");
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
                Debug.Log(gm.DinoInventory[0].ID);

                return gm;
            }
            return new PlayerData();
        }
        public static  string [] GetGameName()
        {
            return Directory.GetFiles(Application.persistentDataPath);
        }
    }
}