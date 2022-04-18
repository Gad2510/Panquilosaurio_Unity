using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Managers
{
    public class GameManager : MonoBehaviour
    {


        public static GameManager _instance;
        LevelManager LM_LevelManager;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [RuntimeInitializeOnLoadMethod]
        public static void InitGameManager()
        {
            GameObject go = new GameObject("Manager");
            _instance = go.AddComponent<GameManager>();
            _instance.LM_LevelManager = go.AddComponent<LevelManager>();
            DontDestroyOnLoad(go);
        }
    }
}

