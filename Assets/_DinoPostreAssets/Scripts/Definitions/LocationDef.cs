using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Definitions {
    [System.Serializable]
    public class LocationDef 
    {
        [SerializeField]
        private LocationCount.Area enm_area;
        [SerializeField]
        private LocationCount.Rank enm_rank;
        [SerializeField]
        private string str_levelName;
        [SerializeField]
        private AudioClip clp_backgroundMusic;

        public LocationCount.Area _Area { get => enm_area; }
        public LocationCount.Rank _Rank { get => enm_rank; }
        public string _LevelName { get => str_levelName; }
        public AudioClip _BackgroundMusic { get => clp_backgroundMusic; }

        public LocationDef(LocationCount.Area _area, LocationCount.Rank _rank, string _lvl)
        {
            enm_area = _area;
            enm_rank = _rank;
            str_levelName = _lvl;
            clp_backgroundMusic = null;
        }
    }
}