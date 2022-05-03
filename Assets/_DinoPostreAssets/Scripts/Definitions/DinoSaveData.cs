using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class DinoSaveData
    {
        [SerializeField]
        private int int_ID;
        [SerializeField]
        private int int_currentHealth;
        [SerializeField]
        private float float_maxHealth;
        [SerializeField]
        private int int_Level;
        [SerializeField]
        private DinoDef.DinoChar enm_Dino;
        [SerializeField]
        private Dictionary<IngredientDef.IngredientType, int> dic_statChanges;
        [SerializeField]
        private bool isSelected;

        private float f_dinoPower;

        public DinoDef.DinoChar Dino { get => enm_Dino; }
        public int Level { get => int_Level; }
        public float Power { get => f_dinoPower; }
        public int CurrentHealth { get => int_currentHealth; set => int_currentHealth= value; }
        public float MaxHealth { get => float_maxHealth; set => float_maxHealth = value; }
        public bool IsSelected { get => isSelected; set => isSelected=value; }
        public int ID { get => int_ID; }

        public DinoSaveData(DinoDef.DinoChar _dino, int _ID, bool _selected=false)
        {
            int_Level = 1;
            isSelected = _selected;
            int_ID = _ID;
            enm_Dino = _dino;
            float_maxHealth = DinoSpecsDef.Instance().LookForStats(enm_Dino).CalculateCurrentValue(DinoStatsDef.Stats.HP, int_Level);
            dic_statChanges = new Dictionary<IngredientDef.IngredientType, int>();
            dic_statChanges.Add(IngredientDef.IngredientType.AGRIDULCE, 0);
            dic_statChanges.Add(IngredientDef.IngredientType.AGRIO, 0);
            dic_statChanges.Add(IngredientDef.IngredientType.DULCE, 0);
            dic_statChanges.Add(IngredientDef.IngredientType.NEUTRO, 0);
            dic_statChanges.Add(IngredientDef.IngredientType.PICOSO, 0);
            dic_statChanges.Add(IngredientDef.IngredientType.SALADO, 0);
        }

        public int GetStatChanged(IngredientDef.IngredientType _type)
        {
            if(dic_statChanges.ContainsKey(_type))
                return dic_statChanges[_type];

            return 0;
        }

        public void SetStatChange(IngredientDef.IngredientType _type)
        {
            dic_statChanges[_type] += 1;

            switch (_type)
            {
                case IngredientDef.IngredientType.AGRIDULCE:
                    if(dic_statChanges[IngredientDef.IngredientType.AGRIO]>0)
                        dic_statChanges[IngredientDef.IngredientType.AGRIO] -= 1;
                    break;
                case IngredientDef.IngredientType.AGRIO:
                    if (dic_statChanges[IngredientDef.IngredientType.AGRIDULCE] > 0)
                        dic_statChanges[IngredientDef.IngredientType.AGRIDULCE] -= 1;
                    break;
                case IngredientDef.IngredientType.DULCE:
                    if (dic_statChanges[IngredientDef.IngredientType.SALADO] > 0)
                        dic_statChanges[IngredientDef.IngredientType.SALADO] -= 1;
                    break;
                case IngredientDef.IngredientType.NEUTRO:
                    if (dic_statChanges[IngredientDef.IngredientType.PICOSO] > 0)
                        dic_statChanges[IngredientDef.IngredientType.PICOSO] -= 1;
                    break;
                case IngredientDef.IngredientType.PICOSO:
                    if (dic_statChanges[IngredientDef.IngredientType.NEUTRO] > 0)
                        dic_statChanges[IngredientDef.IngredientType.NEUTRO] -= 1;
                    break;
                case IngredientDef.IngredientType.SALADO:
                    if (dic_statChanges[IngredientDef.IngredientType.DULCE] > 0)
                        dic_statChanges[IngredientDef.IngredientType.DULCE] -= 1;
                    break;
            }
        }

        public void LevelUP()
        {
            int_Level += 1;
            f_dinoPower=DinoSpecsDef.Instance().CalculatePower(enm_Dino, int_Level);
            float_maxHealth = DinoSpecsDef.Instance().LookForStats(enm_Dino).CalculateCurrentValue(DinoStatsDef.Stats.HP, int_Level);
        }
    }
}