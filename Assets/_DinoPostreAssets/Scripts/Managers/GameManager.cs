using System.Collections;
using System.Collections.Generic;

using UnityEngine.InputSystem;
using UnityEngine;
using Dinopostres.Events;
using Dinopostres.Definitions;

namespace Dinopostres.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;

        private PlayerData PD_gameData;
        private static int int_timeScale = 1;
        private float f_musicVolume;
        private float f_FXVolume;
        public static float _Time { get => int_timeScale; }
        public PlayerData _GameData
        {
            get
            {
                if (PD_gameData == null)
                {
                    PD_gameData = new PlayerData("");
                    //PD_gameData = MemoryManager.LoadGame("");
                    GameMode.OnRecordEvent += PD_gameData.Colectable;
                }
                return PD_gameData;
            }
        }

        
        private void OnDisable()
        {
            if(PD_gameData!= null)
                GameMode.OnRecordEvent -= PD_gameData.Colectable;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void InitGameManager()
        {
            GameObject go = new GameObject("Manager");
            _instance = go.AddComponent<GameManager>();
            go.AddComponent<ControllersManager>();
            go.AddComponent<LevelManager>();
            
            DontDestroyOnLoad(go);
        }

        public void LoadGame (string _index)
        {
            PD_gameData = MemoryManager.LoadGame(_index);

            if (PD_gameData == null)
            {
                MemoryManager.NewGame(_index);
            }

            GameMode.OnRecordEvent += PD_gameData.Colectable;
        }
        public void PauseGame(bool _state)
        {
            int_timeScale = (_state) ? 0 : 1;
        }
    }
}

