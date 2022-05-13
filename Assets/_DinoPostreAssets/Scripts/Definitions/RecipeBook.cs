using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Dinopostres.Definitions
{
    [CreateAssetMenu(fileName = "RecipeBook", menuName = "Dinopostre/RecipeBook", order = 6)]
    public class RecipeBook : ScriptableObject
    {
        private static RecipeBook RB_instance;

        [SerializeField]
        private List<Recipe> lst_Recetas;

        public static RecipeBook _Instance()
        {
            if(RB_instance == null)
            {
                RB_instance = Resources.Load<RecipeBook>("ScriptableObjects/RecipeBook");

            }

            return RB_instance;
        }

        public Recipe Look4Recipe(DinoDef.DinoChar _dino)
        {
            try
            {
                return lst_Recetas.First((x) => x._Dino == _dino);

            }catch (System.Exception e)
            {
                Debug.LogError($"No se encontro la receta para el dino {_dino}");
                return null;
            }
        }

        public void SortRecipies(List<int> unlockRecipies)
        {
            try
            {
                lst_Recetas = lst_Recetas.OrderBy((x) => !unlockRecipies.Any((y) => y == (int)x._Dino)).ToList();
            }
            catch
            {
                Debug.LogError("An error occur while sorting recipes");
            }
            
        }

        public Recipe this[int i] { get => lst_Recetas[i]; }
    }
}