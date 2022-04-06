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
            menu,
            criadero,
            praderaCriadero,
            volcan
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
        object int_Value;

        public Area _Area { get => enm_Area; }
        public Rank _Rank { get => enm_Rank; }
        public object _Value { get => int_Value; }
    }
}
