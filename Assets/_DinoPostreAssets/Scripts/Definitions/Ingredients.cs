using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [CreateAssetMenu(fileName = "Ingredients", menuName = "Dinopostre/Ingredients", order = 2)]
    public class Ingredients: ScriptableObject
    {
        static Ingredients ingre_Instance;

        [SerializeField]
        List<IngredientDef> lst_Ingredients;

        public static Ingredients Instance()
        {
            if (ingre_Instance == null)
            {
                ingre_Instance=Resources.Load<Ingredients>("ScriptableObjects/Ingredients");
            }

            return ingre_Instance;
        } 

        public IngredientDef.IngredientType GetIngredientType(IngredientDef.Sample _sample)
        {
            try
            {
                return lst_Ingredients.Where((x) => x._Ingredient == _sample).First()._Type;
            }
            catch
            {
                Debug.LogWarning("Ingredient not found");
                return IngredientDef.IngredientType.none;
            }
        }

        public Sprite GetIngredientVisual(IngredientDef.Sample _sample)
        {
            try
            {
                return lst_Ingredients.Where((x) => x._Ingredient == _sample).First()._Sprite;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Ingredient not found");
                return null;
            }
        }

        public Object GetIngredientPrefab(IngredientDef.Sample _sample)
        {
            try
            {
                IngredientDef ingre= lst_Ingredients.Where((x) => x._Ingredient == _sample).First();
                return ingre._Mesh;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Ingredient not found");
                return null;
            }
        }
    }
}