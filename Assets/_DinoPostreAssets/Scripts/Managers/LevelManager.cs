using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager LM_instance;
        private EnemyManager EM_EnemyManager;
        private RewardManager RM_RewardManger;
        private bool isIncrementalStage;

        public EnemyManager _EnemyManager { get => EM_EnemyManager; }
        public RewardManager _RewardManager { get => RM_RewardManger; }

        public static LevelManager _Instance { get => LM_instance; }
        // Start is called before the first frame update
        void Awake()
        {
            if (LM_instance == null)
            {
                LM_instance = this;
            }

            EM_EnemyManager = this.gameObject.AddComponent<EnemyManager>();
            RM_RewardManger = this.gameObject.AddComponent<RewardManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}