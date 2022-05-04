using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class LocationCount
    {
        public enum Area
        {
            demo,
            menu,
            criadero,
            praderaCriadero,
            volcan,
            none
        }

        public enum Rank
        {
            F,
            none
        }
        [SerializeField]
        Area enm_Area;
        [SerializeField]
        Rank enm_Rank;
        [SerializeField]
        [Range (0,100)]
        float int_Value;

        public Area _Area { get => enm_Area; }
        public Rank _Rank { get => enm_Rank; }
        public float _Value { get => int_Value; }
    }
}
