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

        public DinoStatsDef LookForStats(DinoDef.DinoChar _Dino)
        {
            try
            {
                return lst_DinoStats.Where((x) => x._Dino == _Dino).First();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Definition not found in the criptable object");
                return null;
            }
        }
    }
}
