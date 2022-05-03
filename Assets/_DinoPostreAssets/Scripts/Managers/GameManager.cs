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
        public EventListener OnDead;

        public static GameManager _instance;
        private LevelManager LM_LevelManager;

        public PlayerData PD_gameData;
        private int int_timeScale = 1;
        private float f_musicVolume;
        private float f_FXVolume;

        public DinoPostreAction InS_gameActions;

        public const int int_maxLives= 3;
        private int int_lives;

        public int _Lives { get => int_lives; }
        public int _TimeScale { get => int_timeScale; }
        public PlayerData _GameData { get => PD_gameData; }
        // Start is called before the first frame update
        void Awake()
        {
            PlayerPrefs.DeleteAll();
            //MemoryManager.NewGame("GaboTest");
            PD_gameData = MemoryManager.LoadGame("GaboTest");

            InS_gameActions = new DinoPostreAction();

            int_lives = int_maxLives;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            InS_gameActions.Enable();

            InS_gameActions.DinopostreController.Pause.performed +=(ctx)=> MemoryManager.SaveGame("GaboTest", PD_gameData);

            OnRecordEvent += PD_gameData.Colectable;
        }
        private void OnDisable()
        {
            InS_gameActions.Disable();

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

        public void LoseLive()
        {
            int_lives--;
            OnDead.Invoke(null);

            if(int_lives<= 0)
            {
                LM_LevelManager.LoadLevel("Criadero");
            }
        }

        public void PauseGame(bool _state)
        {
            int_timeScale = (_state) ? 0 : 1;
            Debug.Log($"TimeScale = {int_timeScale}");
        }

        public DinoSaveData GetActiveDino()
        {
            return PD_gameData.GetActive();
        }
    }
}

