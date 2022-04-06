using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Managers
{
    public class LevelManager : MonoBehaviour
    {
        EnemyManager EM_EnemyManager;
        bool isIncrementalStage;

        public EnemyManager _EnemyManager { get => EM_EnemyManager; }

        // Start is called before the first frame update
        void Awake()
        {
            EM_EnemyManager= this.gameObject.AddComponent<EnemyManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}