using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Definitions {
    [System.Serializable]
    public class Recipe
    {
        private enum GrowRate
        {
            corto, mediano, largo
        }

        [SerializeField]
        private DinoDef.DinoChar enm_dino;
        [SerializeField]
        private GrowRate emn_rate;
        [SerializeField]
        private List<IngredientCount> lst_ingredientBase;

        public DinoDef.DinoChar _Dino { get => enm_dino; }

        public List<IngredientCount> GetIngredientsNextLevel(int _level)
        {
            List<IngredientCount> nextLevelIngr= lst_ingredientBase;
            foreach (IngredientCount ing in nextLevelIngr)
            {
                CalculateIngredienteAmount(ing._Count,_level);
            }
            return nextLevelIngr;
        }

        private int CalculateIngredienteAmount(int _countBase, int _level)
        {
            switch (emn_rate)
            {
                case GrowRate.corto:
                    return (int)(((_countBase / 5) + 1) * _level);
                case GrowRate.mediano:
                    return (int)((((Mathf.Pow(_countBase, 2)*4)/500) + 1) * _level);
                case GrowRate.largo:
                    return (int)(((Mathf.Pow(_countBase, 3) / 5000) + 1) * _level);
                default:
                    return 1;
            }
        }
    }
}