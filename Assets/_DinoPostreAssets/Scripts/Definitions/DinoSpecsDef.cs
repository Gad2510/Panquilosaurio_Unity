using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [CreateAssetMenu(fileName = "DinoSpecs", menuName = "Dinopostre/DinoSpecs", order = 1)]
    public class DinoSpecsDef:ScriptableObject
    {
        static DinoSpecsDef DS_instance;

        [SerializeField]
        List<DinoStatsDef> lst_DinoStats;

        public static DinoSpecsDef Instance()
        {
            if (DS_instance == null)
            {
                DS_instance=Resources.Load<DinoSpecsDef>("ScriptableObjects/DinoSpecs");
            }

            return DS_instance;
        }

        public DinoStatsDef LookForStats(DinoDef.DinoChar _dino)
        {
            try
            {
                return lst_DinoStats.Where((x) => x._Dino == _dino).First();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Definition not found in the scriptable object");
                return null;
            }
        }

        public float CalculatePower(DinoDef.DinoChar _dino, int _level)
        {
            try
            {
                float power = 0;
                DinoStatsDef stats = lst_DinoStats.Where((x) => x._Dino == _dino).First();
                for (int i = 0; i < (int)DinoStatsDef.Stats.none; i++)
                {
                    power += stats.CalculateCurrentValue((DinoStatsDef.Stats)i, _level);
                }
                return power;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Definition not found in the scriptable object to get current power");
                return -1;
            }
        }
    }
}
