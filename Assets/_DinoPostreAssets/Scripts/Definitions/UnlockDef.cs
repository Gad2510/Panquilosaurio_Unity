using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class UnlockDef
    {
        [SerializeField]
        int int_id;
        [SerializeField]
        List<DinoCount> lst_dinoCount;
        [SerializeField]
        List<LocationCount> lst_locCount;
        [SerializeField]
        int int_migasRequire=-1;
        [SerializeField]
        int int_totalLocations=-1;
        [SerializeField]
        int int_totalEnemies=-1;
        [SerializeField]
        int int_changesRequire=-1;

        public int _ID => int_id;
        public List<DinoCount> _EnemyCount => lst_dinoCount;
        public List<LocationCount> _LocationCount => lst_locCount;
        public int _TotalMigas => int_migasRequire;
        public int _TotalLocation => int_totalLocations;
        public int _TotalEnemies => int_totalEnemies;
        public int _RequireChanges => int_changesRequire;

    }
}