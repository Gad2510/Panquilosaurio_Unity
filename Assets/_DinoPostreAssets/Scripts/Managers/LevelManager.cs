using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Dinopostres.Definitions;

namespace Dinopostres.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public enum GameStates
        {
            Menu,
            Map,
            InStage
        }

        private Dictionary<string, GameStates> dic_levelStates = new Dictionary<string, GameStates>
        {
            {"Menu",GameStates.Menu },
            {"Criadero",GameStates.Map },
            {"Pradera Crianza",GameStates.InStage },
            {"Volcan",GameStates.InStage },
            {"PanquilosaurioTest",GameStates.InStage },
        };

        public LocationCount.Area enm_lastArea= LocationCount.Area.demo;
        private LocationCount.Rank enm_lastRank= LocationCount.Rank.none;
        private List<DinoDef> lst_enemiesInLevel;

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
        public LocationCount.Area _Area { get => enm_lastArea; }
        public LocationCount.Rank _Rank { get => enm_lastRank; }
        public GameStates _Stage { get => dic_levelStates[SceneManager.GetActiveScene().name]; }
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

            dic_TelportersInLevel = new Dictionary<int, GameObject>();
            int_stageCount = 0;

            LoadStage(_Area, _Rank);
        }

        private void OnEnable()
        {
            SetLoadEvent(OnLoadLevel, true);
        }

        private void OnDisable()
        {
            SetLoadEvent(OnLoadLevel, false);
        }

        public void SetLoadEvent(UnityAction<Scene,LoadSceneMode> _ev, bool _toSet)
        {
            if(_toSet)
                SceneManager.sceneLoaded += _ev;
            else
                SceneManager.sceneLoaded -= _ev;
        }

        protected void OnLoadLevel(Scene _scene, LoadSceneMode _mode)
        {
            int_stageCount = 0;
            LoadGameMode(SceneManager.GetActiveScene().name);
            if (dic_levelStates[SceneManager.GetActiveScene().name]== GameStates.InStage)
            {
                MovePlayer();
            }
            else if(dic_levelStates[SceneManager.GetActiveScene().name] == GameStates.Map)
            {
                RecipeBook._Instance().CheckForUnlockRecipies(GameManager._instance.PD_gameData);
                GameManager._instance.PD_gameData.RestoreHP();
                MemoryManager.SaveGame(GameManager._instance.PD_gameData);
            }
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
            dic_TelportersInLevel.Clear();
            SceneManager.LoadScene(_level);
        }

        public void LoadStage(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            if (_Stage != GameStates.InStage)
                return;

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
                    int ran = Random.Range(0, dic_TelportersInLevel.Count());
                    GameObject tel = dic_TelportersInLevel.Where((x) => x.Key != currentTeleporterID).ElementAt(ran).Value;
                    int_stageCount++;
                    return tel;
                }
                catch
                {
                    Debug.LogError("No teleporter register");
                    return go_bossTeleporter;
                }
            }

            return go_bossTeleporter;
        }

        public void MovePlayer()
        {
            GameObject pl= GameObject.FindGameObjectWithTag("Player");
            int ran = Random.Range(0, dic_TelportersInLevel.Count());
            GameObject tl=  dic_TelportersInLevel.ElementAt(ran).Value;

            pl.transform.position = tl.transform.position;
        }
    }
}