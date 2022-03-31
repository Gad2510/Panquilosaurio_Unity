using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.CharacterControllers;

namespace Dinopostres.Managers
{
    public class GameManager : MonoBehaviour
    {
        LevelManager LM_LevelManager;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public List<Enemy> ReturnCurrentEnemyList()
        {
            return LM_LevelManager._EnemyManager.CurrentEnemies;
        }
    }
}

