using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MemorySystem : MonoBehaviour
{
    public static void NewGame(string _gameName)
    {
        //Path pesistente del sistema en el que se guardan los datos del juego
        string pathCombined = Path.Combine(
            Application.persistentDataPath,
            _gameName + ".data");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(pathCombined);
        bf.Serialize(file, new GameData(_gameName, 15, 24, 10));
        file.Close();
        Debug.Log(pathCombined);
    }

    public static GameData LoadGame(string _gameName)
    {
        //Path pesistente del sistema en el que se guardan los datos del juego
        string pathCombined = Path.Combine(
            Application.persistentDataPath,
            _gameName + ".data");

        if (File.Exists(pathCombined))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathCombined, FileMode.Open);
            GameData gm= (GameData)bf.Deserialize(file);
            file.Close();
            Debug.Log(gm.GameName);

            return gm;
        }
        return new GameData();
    }
}
