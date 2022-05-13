using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Dinopostres.Definitions;
using Dinopostres.Managers;

namespace Dinopostres.UIElements
{
    public class UIRecipeBook : CarruselBehavior<Recipe>
    {
        [SerializeField]
        TextMeshProUGUI txt_migas;

        private UIRecipeDes UIR_currentRecipe;
        private List<IngredientCount> lst_ingredients;

        protected override void Start()
        {
            txt_migas.text = GameManager._instance.PD_gameData._Migas.ToString("00000");
            base.Start();
        }

        protected override UnityAction GetDesciptionEvent(Recipe _item)
        {
            throw new System.NotImplementedException();
        }

        protected override void InitUiValues()
        {
            RecipeBook._Instance().SortRecipies(GameManager._instance._GameData.UnlockRecipies);
            for(int i = 0; i < arr_items.Length; i++)
            {
                Recipe rep= RecipeBook._Instance()[i];
                arr_items[i].InitStats(rep, () => {
                    if (UIR_currentRecipe != null && GameManager._instance.PD_gameData.CanBePurchase(UIR_currentRecipe.StoreData._Ingredients))
                    {
                        GameManager._instance.PD_gameData.MakePurchase(UIR_currentRecipe.StoreData._Ingredients);
                        GameManager._instance._GameData.RegisterNewDino(rep._Dino);
                        UpdateRecepeeDes();
                        txt_migas.text = GameManager._instance.PD_gameData._Migas.ToString("00000");
                    }
                });
            }
            arr_items[0].SetButtonAsSelected();
        }

        protected override void InitUIVisuals()//Define cuales items son visibles
        {

        }

        protected override void SetButtonEvent(UIDescriptions<Recipe> _info)//Se aplica en el Start
        {
            _info = (UIRecipeDes)_info;
            //Evento que no se puden retirar
            _info.AddBtnClicEvent(()=> {
                
            });
            _info.AddBtnSelectedEvent(() =>
            {
                Debug.Log("ButtonSelected");
                MoveDinoUI(_info._ParentIndex);
                int_lastIndex = _info._ParentIndex;
                UIR_currentRecipe = (UIRecipeDes)_info;
            });
        }

        protected override Recipe SetItemValue(int _index)//Se apliaca cada que se mueve el item
        {
            return RecipeBook._Instance()[_index];
        }

        private void UpdateRecepeeDes()
        {
            foreach(UIDescriptions<Recipe> rec in arr_items)
            {
                rec.QuickRelodStats();
            }
        }

    }
}