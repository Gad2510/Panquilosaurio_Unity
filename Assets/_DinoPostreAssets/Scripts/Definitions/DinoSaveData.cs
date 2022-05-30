using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [Serializable]
    public class DinoSaveData
    {
        private int int_ID;
        private float f_currentHealth;
        private float f_maxHealth;
        private int int_Level;
        private DinoDef.DinoChar enm_Dino;
        private bool isSelected;

        private float f_dinoPower;

        public DinoDef.DinoChar Dino { get => enm_Dino; }
        public int Level { get => int_Level; }
        public float Power { get => f_dinoPower; }
        public float CurrentHealth { get => f_currentHealth; set => f_currentHealth= value; }
        public float MaxHealth { get => f_maxHealth; set => f_maxHealth = value; }
        public bool IsSelected { get => isSelected; set => isSelected=value; }
        public int ID { get => int_ID; }

        public DinoSaveData(DinoDef.DinoChar _dino, int _ID, bool _selected=false, int _level=1)
        {
            int_Level = _level;
            isSelected = _selected;
            int_ID = _ID;
            enm_Dino = _dino;
            f_maxHealth = DinoSpecsDef.Instance().LookForStats(enm_Dino).CalculateCurrentValue(DinoStatsDef.Stats.HP, int_Level);
            f_currentHealth = f_maxHealth;
            f_dinoPower = DinoSpecsDef.Instance().CalculatePower(enm_Dino, int_Level);
            
        }

        

        public void LevelUP()
        {
            int_Level += 1;
            f_dinoPower=DinoSpecsDef.Instance().CalculatePower(enm_Dino, int_Level);
            f_maxHealth = DinoSpecsDef.Instance().LookForStats(enm_Dino).CalculateCurrentValue(DinoStatsDef.Stats.HP, int_Level);
            RestoreDino();
        }

        public void RestoreDino()
        {
            f_currentHealth = f_maxHealth;
        }
    }
}