using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Dinopostres.Definitions
{
    [CreateAssetMenu(fileName = "BossStageStorage", menuName = "Dinopostre/BossStageStorage", order = 4)]
    public class BossStageStorage : ScriptableObject
    {
        private static BossStageStorage BSS_instance;

        [SerializeField]
        private List<BossStageDef> lst_bossStageDef;

        public static BossStageStorage _Instance()
        {
            if (BSS_instance == null)
            {
                BSS_instance = Resources.Load<BossStageStorage>("ScriptableObjects/BossStageStorage");
            }

            return BSS_instance;
        }

        public List<DinoDef> GetAllBossesInStage(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            try
            {
                return lst_bossStageDef.First((x) => x._Area == _area && x._Rank == _rank).GetAllBossesDef();
            }
            catch
            {
                return null;
            }
        }

        public BossRelation SelectRandomBoss(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            try
            {
                return lst_bossStageDef.Where((x) => x._Area == _area && x._Rank == _rank).First().GetRandomBoss();
            }
            catch(System.Exception e)
            {
                Debug.LogError("No Boss in the current stage");
                return null;
            }
        }
    }
}