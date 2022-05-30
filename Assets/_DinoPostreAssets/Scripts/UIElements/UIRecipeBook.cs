using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public UIDescriptions<Recipe> [] test;
        protected override void Start()
        {
            txt_migas.text = GameManager._instance._GameData._Migas.ToString("00000");
            base.Start();
        }

        protected override UnityAction GetDesciptionEvent(Recipe _item)
        {
            return () => {
                if (UIR_currentRecipe != null && GameManager._instance._GameData.CanBePurchase(UIR_currentRecipe.StoreData._Ingredients))
                {
                    GameManager._instance._GameData.MakePurchase(UIR_currentRecipe.StoreData._Ingredients);
                    GameManager._instance._GameData.RegisterNewDino(_item._Dino);
                    GameManager._instance.OnRecordEvent(null);
                    UpdateRecepeeDes();
                    txt_migas.text = GameManager._instance._GameData._Migas.ToString("00000");
                }
            };
        }

        protected override void InitUiValues()
        {
            RecipeBook._Instance().SortRecipies(GameManager._instance._GameData.UnlockRecipies);
            for(int i = 0; i < arr_items.Length; i++)
            {
                Recipe rep= RecipeBook._Instance()[i];
                arr_items[i].InitStats(rep,GetDesciptionEvent(rep));
            }
        }

        protected override void InitUIVisuals()//Define cuales items son visibles
        {
            if(arr_items!= null)
            {
                arr_items = arr_items.OrderBy((x) => x.transform.parent.GetSiblingIndex()).ToArray();
                test = arr_items;
                arr_items[0].SetButtonAsSelected();
            }
                
        }

        protected override void SetButtonEvent(UIDescriptions<Recipe> _info)//Se aplica en el Start
        {
            _info = (UIRecipeDes)_info;
            //Evento que no se puden retirar
            _info.AddBtnClicEvent(()=> {
                
            });
            _info.AddBtnSelectedEvent(() =>
            {
                MoveDinoUI(_info._ParentIndex, RecipeBook._Instance()._RecepeCount-1);
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