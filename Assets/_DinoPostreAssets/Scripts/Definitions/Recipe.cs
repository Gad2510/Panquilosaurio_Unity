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
        [SerializeField]
        private UnlockDef UD_requirements;

        public DinoDef.DinoChar _Dino { get => enm_dino; }
        public List<IngredientCount> _Ingredients { get => lst_ingredientBase; }
        public UnlockDef _Requirements => UD_requirements;
        public List<IngredientCount>  GetIngredientsNextLevel(int _level)
        {
            List<IngredientCount> nextLevelIngr = new List<IngredientCount>();
            for (int i =0;i<lst_ingredientBase.Count;i++)
            {
                int count = CalculateIngredienteAmount(lst_ingredientBase[i]._Count,_level);
                nextLevelIngr.Add(new IngredientCount(lst_ingredientBase[i]._Ingredient, count));
            }
            return nextLevelIngr;
        }

        private int CalculateIngredienteAmount(int _countBase, int _level)
        {
            switch (emn_rate)
            {
                case GrowRate.corto:
                    return (int)(((_level / 5) + 1) * _countBase);
                case GrowRate.mediano:
                    return (int)((((Mathf.Pow(_level, 2)*4)/500) + 1) * _countBase);
                case GrowRate.largo:
                    return (int)(((Mathf.Pow(_level, 3) / 5000) + 1) * _countBase);
                default:
                    return 1;
            }
        }
    }
}