using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [CreateAssetMenu(fileName = "EnemyStorage", menuName = "Dinopostre/EnemyStorage", order = 3)]
    public class EnemyStorage : ScriptableObject
    {
        private static EnemyStorage ES_instance;
        [SerializeField]
        List<DinoDef> lst_Definitions;

        public static EnemyStorage _Instance()
        {
            if (ES_instance == null)
            {
                ES_instance = Resources.Load<EnemyStorage>("ScriptableObjects/EnemyStorage");
            }

            return ES_instance;
        }

        public List<DinoDef> GetEnemiesPerLevel(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            try
            {
                List<DinoDef> enemies = lst_Definitions.Where((x) => x.HasLocation(_area, _rank) > 0).ToList();
                return enemies;
            }
            catch
            {
                Debug.Log("An error occur while getting enemies in level");
                return null;
            }
        }

        public DinoDef Look4DinoDef(DinoDef.DinoChar _dino)
        {
            try
            {
                DinoDef def = lst_Definitions.Where((x) => x._Dino == _dino).First();
                return def;
            }
            catch
            {
                Debug.LogWarning("Dino not define");
                return null;
            }
        }
        public string GetDeafultEnemyAttack(DinoDef.DinoChar _dino)
        {
            try
            {
                DinoDef def =lst_Definitions.Where((x) => x._Dino == _dino).First();
                return def._DefaultSkill;
            }
            catch
            {
                Debug.LogWarning("Default attack not define");
                return "";
            }
        }

        public Sprite GetDinoImage(DinoDef.DinoChar _dino)
        {
            try
            {
                DinoDef def = lst_Definitions.Where((x) => x._Dino == _dino).First();
                return def._DinoImage;
            }
            catch
            {
                Debug.LogWarning("Dino image not found");
                return null;
            }
        }


    }
}
