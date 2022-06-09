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
        public delegate void EventListener(Events.Event ev);
        public EventListener OnRecordEvent;
        public EventListener OnPlayerDead;

        public static GameManager _instance;
        private LevelManager LM_LevelManager;

        private PlayerData PD_gameData;
        private static int int_timeScale = 1;
        private float f_musicVolume;
        private float f_FXVolume;

        public DinoPostreAction InS_gameActions;
        public const int int_maxLives= 3;
        private int int_lives;

        public int _Lives { get => int_lives; }
        public int _LivesInverse { get => int_maxLives-int_lives; }
        public static float _TimeScale { get => int_timeScale*Time.deltaTime; }
        public static float _Time { get => int_timeScale; }
        public PlayerData _GameData
        {
            get
            {
                if (PD_gameData == null)
                {
                    PD_gameData = new PlayerData("");
                    //PD_gameData = MemoryManager.LoadGame("");
                    OnRecordEvent += PD_gameData.Colectable;
                }
                return PD_gameData;
            }
        }
        // Start is called before the first frame update
        private void Awake()
        {

            InS_gameActions = new DinoPostreAction();

            int_lives = int_maxLives;
        }

        private void OnEnable()
        {
            InS_gameActions.Enable();
        }
        private void OnDisable()
        {
            InS_gameActions.Disable();
            if(PD_gameData!= null)
                OnRecordEvent -= PD_gameData.Colectable;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void InitGameManager()
        {
            GameObject go = new GameObject("Manager");
            _instance = go.AddComponent<GameManager>();
            _instance.LM_LevelManager = go.AddComponent<LevelManager>();
            DontDestroyOnLoad(go);
        }

        public void LoadGame (string _index)
        {
            PD_gameData = MemoryManager.LoadGame(_index);

            if (PD_gameData == null)
            {
                MemoryManager.NewGame(_index);
            }

            OnRecordEvent += PD_gameData.Colectable;
        }

        public void LoseLive()
        {
            int_lives--;
            OnPlayerDead.Invoke(null);
            if(int_lives<= 0 ||(_GameData.DinoInventory.Count - _LivesInverse) <= 0)

            {
                LM_LevelManager.LoadLevel("Criadero");
            }
        }

        public void PauseGame(bool _state)
        {
            int_timeScale = (_state) ? 0 : 1;
        }

        public DinoSaveData GetActiveDino()
        {
            return PD_gameData.GetActive();
        }
    }
}

