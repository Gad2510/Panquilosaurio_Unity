using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [Serializable]
    public class IngredientCount
    {
        [SerializeField]
        private IngredientDef.Sample enm_Ingredient;
        [SerializeField]
        private int int_Count;

        public IngredientDef.Sample _Ingredient { get => enm_Ingredient; }
        public int _Count { get => int_Count; set => int_Count += value; }

        public IngredientCount(IngredientDef.Sample _type, int _init)
        {
            enm_Ingredient = _type;
            int_Count = _init;
        }

        public IngredientCount(IngredientDef.Sample _type)
        {
            enm_Ingredient = _type;
            int_Count = 0;
        }


    }
}