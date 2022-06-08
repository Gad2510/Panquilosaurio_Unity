using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class UnlockDef
    {
        [SerializeField]
        private int int_id;
        [SerializeField]
        private List<DinoCount> lst_dinoCount;
        [SerializeField]
        private List<LocationCount> lst_locCount;
        [SerializeField]
        private int int_migasRequire =-1;
        [SerializeField]
        private int int_totalLocations =-1;
        [SerializeField]
        private int int_totalEnemies =-1;
        [SerializeField]
        private int int_changesRequire =-1;

        public int _ID => int_id;
        public List<DinoCount> _EnemyCount => lst_dinoCount;
        public List<LocationCount> _LocationCount => lst_locCount;
        public int _TotalMigas => int_migasRequire;
        public int _TotalLocation => int_totalLocations;
        public int _TotalEnemies => int_totalEnemies;
        public int _RequireChanges => int_changesRequire;

    }
}