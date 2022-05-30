using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dinopostres.Definitions;
using Dinopostres.Managers;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace Dinopostres.UIElements
{
    public class UIRecipeDes : UIDescriptions<Recipe>, ISelectHandler
    {
        [SerializeField]
        private Image img_dinoImg;
        [SerializeField]
        private GameObject go_lockImage;
        [SerializeField]
        private Transform trns_parentIngredients;

        private UIIngredientDes[] arr_ingredients;
        //private Navigation costomNav;
        protected override void Awake()
        {
            base.Awake();
            //costomNav = new Navigation();
            arr_ingredients = trns_parentIngredients.GetComponentsInChildren<UIIngredientDes>(true);
        }

        public override void InitStats(Recipe _data, UnityAction _ev)
        {
            base.InitStats(_data, _ev);
            if (img_dinoImg != null)
                img_dinoImg.sprite = EnemyStorage._Instance().GetDinoImage(storeData._Dino);
            if (base.txt_name != null)
                txt_name.text = _data._Dino.ToString();

            LockOrUnlockRecipe();
            QuickRelodStats();
        }

        public override void QuickRelodStats()
        {
            LoadIngredientes();
        }

        private void LoadIngredientes() 
        {
            for (int i =0; i < arr_ingredients.Length; i++){
                if(i< storeData._Ingredients.Count)
                {
                    arr_ingredients[i].UpdateDescriptions(storeData._Ingredients[i]._Ingredient, storeData._Ingredients[i]._Count);
                }
                else
                {
                    arr_ingredients[i].UpdateDescriptions(IngredientDef.Sample.none);
                }
            }
        }

        private void LockOrUnlockRecipe()
        {
            bool isRecipeUnlock = GameManager._instance._GameData.GetRecipeUnlock((int)storeData._Dino);
            go_lockImage.SetActive(!isRecipeUnlock);
            btn_executer.interactable = isRecipeUnlock;
            //costomNav.mode = (isRecipeUnlock) ? Navigation.Mode.Automatic : Navigation.Mode.None;
            //btn_executer.navigation = costomNav;
        }
    }
}