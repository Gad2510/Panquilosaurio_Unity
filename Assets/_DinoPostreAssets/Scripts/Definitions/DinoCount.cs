using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class DinoCount 
    {
        [SerializeField]
        DinoDef.DinoChar enm_dino;
        [SerializeField]
        int int_value;

        public DinoDef.DinoChar Dino { get => enm_dino; }

        public int Value { get => int_value; set => int_value = value; }

        public DinoCount (DinoDef.DinoChar _dino)
        {
            enm_dino = _dino;
            int_value = 0;
        }

        public DinoCount(DinoDef.DinoChar _dino, int _value)
        {
            enm_dino = _dino;
            int_value = _value;
        }
    }
}