using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class BossStageDef
    {
        [SerializeField]
        private LocationCount.Area enm_Area;
        [SerializeField]
        private LocationCount.Rank enm_Rank;
        [SerializeField]
        private List<BossRelation> lst_bosses;

        public LocationCount.Area _Area { get => enm_Area; }
        public LocationCount.Rank _Rank { get => enm_Rank; }
         
        public BossRelation GetRandomBoss()
        {
            int max =lst_bosses.Sum((x) => x._Value);
            int ran = Random.Range(0, max);
            int i = 0;
            for(int count =0; i<lst_bosses.Count && count<ran; i++)
            {
                count += lst_bosses[i]._Value;
            }
            return lst_bosses[i-1];
        }

        public List<DinoDef> GetAllBossesDef()
        {
            try
            {
                List<DinoDef> dinos=(lst_bosses.Select((x) => EnemyStorage._Instance().Look4DinoDef(x._Boss))).ToList();
                return dinos;
            }catch (System.Exception e)
            {
                Debug.LogError($"No se pudo encontrar ningun boss asignado a este nivel Area {enm_Area.ToString()}  Rank {enm_Rank.ToString()}");
                return null;
            }
        }
    }

    [System.Serializable]
    public class BossRelation
    {
        [SerializeField]
        private DinoDef.DinoChar enm_boss;
        [SerializeField]
        private DinoDef.DinoChar enm_Companion;
        [SerializeField]
        private int int_value;
        public DinoDef.DinoChar _Companion { get => enm_Companion; }
        public DinoDef.DinoChar _Boss { get => enm_boss; }
        public int _Value { get => int_value; }
    }
}