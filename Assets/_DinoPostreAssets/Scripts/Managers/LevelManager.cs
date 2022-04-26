using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dinopostres.Definitions;

namespace Dinopostres.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public LocationCount.Area enm_lastArea;
        public LocationCount.Rank enm_lastRank;
        public List<DinoDef> lst_enemiesInLevel;

        private EnemyStorage ES_EnemyStorage;

        private static LevelManager LM_instance;
        private EnemyManager EM_EnemyManager;
        private RewardManager RM_RewardManger;
        private bool isIncrementalStage;

        private GameObject go_dispacher;
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

            ES_EnemyStorage = EnemyStorage._Instance();

            LoadStage(LocationCount.Area.demo, LocationCount.Rank.none);

            Object pref_dispacher = Resources.Load<Object>("Prefabs/UI_Dispacher");
            go_dispacher= Instantiate(pref_dispacher) as GameObject;
            go_dispacher.SetActive(false);
        }

        private void OnLevelWasLoaded(int level)
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadStage(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            enm_lastArea = _area;
            enm_lastRank = _rank;
            lst_enemiesInLevel = ES_EnemyStorage.GetEnemiesPerLevel(_area, _rank);
        }

        public void OpenCloseDispacher(bool _state)
        {
            go_dispacher.SetActive(_state);
        }

        public Object GetEnemiesFromLevel(out float _rarety)
        {
            float max = lst_enemiesInLevel.Sum((x) => x.HasLocation(enm_lastArea, enm_lastRank));
            _rarety = Random.Range(0f, max);
            int i = 0;

            for(int count = 0; i < lst_enemiesInLevel.Count() || count< _rarety; i++)
            {
                count += lst_enemiesInLevel[i].HasLocation(enm_lastArea, enm_lastRank);
            }

            return lst_enemiesInLevel[0]._Prefab;
        }

    }
}