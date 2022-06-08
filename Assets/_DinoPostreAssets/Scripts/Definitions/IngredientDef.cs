using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class IngredientDef
    {
        public enum Sample
        {
            MIGAS, CHOCOLATE, CREMA, MANZANA, MANTEQUILLA,none
        }

        public enum IngredientType
        {
            DULCE,SALADO,AGRIO,NEUTRO,PICOSO,AGRIDULCE,none
        }

        [SerializeField]
        private Sample enm_Ingredient;
        [SerializeField]
        private IngredientType enm_IngreType;
        [SerializeField]
        private Sprite spt_IngreImage;
        [SerializeField]
        private Object obj_Colectable;

        public Sample _Ingredient { get => enm_Ingredient; }
        public IngredientType _Type { get => enm_IngreType; }
        public Sprite _Sprite { get => spt_IngreImage; }
        public Object _Mesh { get => obj_Colectable; }
    }
}