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
        Sample enm_Ingredient;
        [SerializeField]
        IngredientType enm_IngreType;
        [SerializeField]
        Sprite spt_IngreImage;
        [SerializeField]
        VisualEffectObject par_Particles;

        public Sample _Ingredient { get => enm_Ingredient; }
        public IngredientType _Type { get => enm_IngreType; }
        public Sprite _Sprite { get => spt_IngreImage; }

        public VisualEffectObject GetParticleSystem(int amount)
        {
            return par_Particles;
        }
    }
}