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
        Mesh msh_Colectable;
        [SerializeField]
        Material mat_CollectableMaterial;

        public Sample _Ingredient { get => enm_Ingredient; }
        public IngredientType _Type { get => enm_IngreType; }
        public Sprite _Sprite { get => spt_IngreImage; }
        public Mesh _Mesh { get => msh_Colectable; }
        public Material _Mat { get => mat_CollectableMaterial; }
    }
}