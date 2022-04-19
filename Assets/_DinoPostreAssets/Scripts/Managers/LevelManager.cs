using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Managers
{
    public class LevelManager : MonoBehaviour
    {
        EnemyManager EM_EnemyManager;
        RewardManager RM_RewardManger;
        bool isIncrementalStage;

        public EnemyManager _EnemyManager { get => EM_EnemyManager; }
        public RewardManager _RewardManager { get => RM_RewardManger; }

        // Start is called before the first frame update
        void Awake()
        {
            EM_EnemyManager= this.gameObject.AddComponent<EnemyManager>();
            RM_RewardManger = this.gameObject.AddComponent<RewardManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}