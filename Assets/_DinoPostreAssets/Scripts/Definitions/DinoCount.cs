using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Definitions
{
    [Serializable]
    public class DinoCount 
    {
        [SerializeField]
        private DinoDef.DinoChar enm_dino;
        [SerializeField]
        private int int_value;

        public DinoDef.DinoChar _Dino { get => enm_dino; }
        public int _Value { get => int_value; set => int_value = value; }

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