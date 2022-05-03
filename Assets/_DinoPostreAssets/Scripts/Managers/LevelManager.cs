using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dinopostres.Definitions;

namespace Dinopostres.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private enum GameStates
        {
            Menu,
            Map,
            InStage
        }

        private Dictionary<string, GameStates> dic_levelStates = new Dictionary<string, GameStates>
        {
            {"Menu",GameStates.Menu },
            {"Criadero",GameStates.Map },
            {"PraderCrianza",GameStates.InStage },
            {"Volcan",GameStates.InStage },
            {"PanquilosaurioTest",GameStates.InStage },
        };

        public LocationCount.Area enm_lastArea;
        public LocationCount.Rank enm_lastRank;
        public List<DinoDef> lst_enemiesInLevel;

        private EnemyStorage ES_EnemyStorage;
        private BossStageStorage BSS_BossStageStorage;

        private static LevelManager LM_instance;
        private EnemyManager EM_EnemyManager;
        private RewardManager RM_RewardManger;
        private GameMode GM_currentMode;

        private int int_stageCount;
        private Dictionary<int, GameObject> dic_TelportersInLevel;
        private GameObject go_bossTeleporter;

        public GameMode _GameMode { get => GM_currentMode; }
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

            string levelName=SceneManager.GetActiveScene().name;
            LoadGameMode(levelName);

            EM_EnemyManager = this.gameObject.AddComponent<EnemyManager>();
            RM_RewardManger = this.gameObject.AddComponent<RewardManager>();

            ES_EnemyStorage = EnemyStorage._Instance();
            BSS_BossStageStorage = BossStageStorage._Instance();

            LoadStage(LocationCount.Area.demo, LocationCount.Rank.none);

            dic_TelportersInLevel = new Dictionary<int, GameObject>();
            int_stageCount = 0;
        }

        private void OnLevelWasLoaded(int level)
        {
            dic_TelportersInLevel.Clear();
            int_stageCount = 0;
        }

        public void LoadGameMode(string _levelName)
        {
            if (GM_currentMode != null)
                Destroy(GM_currentMode);
            switch (dic_levelStates[_levelName])
            {
                case GameStates.Menu:
                    GM_currentMode = gameObject.AddComponent<GameModeMENU>();
                    break;
                case GameStates.InStage:
                    GM_currentMode = gameObject.AddComponent<GameModeINSTAGE>();
                    break;
                case GameStates.Map:
                    GM_currentMode = gameObject.AddComponent<GameModeMAP>();
                    break;
                default:
                    GM_currentMode = gameObject.AddComponent<GameMode>();
                    break;
            }
        }

        public void LoadLevel(string _level)
        {
            LoadLevel(_level);
        }

        public void LoadStage(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            enm_lastArea = _area;
            enm_lastRank = _rank;
            lst_enemiesInLevel = ES_EnemyStorage.GetEnemiesPerLevel(_area, _rank);
        }

        

        public Object GetEnemiesFromLevel(out float _rarety)
        {
            float max = lst_enemiesInLevel.Sum((x) => x.HasLocation(enm_lastArea, enm_lastRank));
            _rarety = Random.Range(0f, max);
            int i = 0;

            for(int count = 0; i < lst_enemiesInLevel.Count() && count< _rarety; i++)
            {
                count += lst_enemiesInLevel[i].HasLocation(enm_lastArea, enm_lastRank);
            }
            Debug.Log($"Rarety = {_rarety} | index = {i-1}");
            return lst_enemiesInLevel[i-1]._Prefab;
        }

        public Object GetBossFromLevel(out Object _companion)
        {
            BossRelation rel= BSS_BossStageStorage.SelectRandomBoss(enm_lastArea, enm_lastRank);
            _companion =ES_EnemyStorage.Look4DinoDef(rel._Companion)._Prefab;
            return ES_EnemyStorage.Look4DinoDef(rel._Boss)._Prefab;
        }

        public void RegisterTeleporter(GameObject _go, int _id, bool isBoss=false)
        {
            if (!isBoss)
                dic_TelportersInLevel.Add(_id, _go);
            else
                go_bossTeleporter = _go;
        }

        public GameObject SelectNextTeleporter(int currentTeleporterID)
        {
            if (int_stageCount < 3)
            {
                try
                {
                    int ran = Random.Range(0, dic_TelportersInLevel.Count() - 1);
                    GameObject tel = dic_TelportersInLevel.Where((x) => x.Key != currentTeleporterID).ElementAt(ran).Value;
                    int_stageCount++;
                    return tel;
                }
                catch (System.Exception e)
                {
                    Debug.LogError("No teleporter register");
                    return go_bossTeleporter;
                }
            }

            return go_bossTeleporter;
        }
    }
}