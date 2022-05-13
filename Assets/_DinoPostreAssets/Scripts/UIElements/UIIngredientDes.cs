using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Managers;
using Dinopostres.Definitions;

namespace Dinopostres.UIElements
{
    public class UIIngredientDes : MonoBehaviour
    {
        [SerializeField]
        GameObject go_ingredientDetails;
        [SerializeField]
        Image img_ingredientImg;
        [SerializeField]
        TextMeshProUGUI txt_ingredientCount;

        IngredientDef.Sample enm_ingredient= IngredientDef.Sample.none;
       
        public IngredientDef.Sample _Ingredient { get => enm_ingredient; }

        private void OnEnable()
        {
            if(enm_ingredient== IngredientDef.Sample.none)
            {
                go_ingredientDetails.SetActive(false);
            }
        }

        public void UpdateDescriptions(IngredientDef.Sample _type, int _amount=-1, bool _decriptive=false)
        {
            if (_type == IngredientDef.Sample.none)
                return;

            enm_ingredient = _type;
            img_ingredientImg.sprite = Ingredients.Instance().GetIngredientVisual(enm_ingredient);
            int inInventory = GameManager._instance.PD_gameData.GetIngredientCount(enm_ingredient);
            if (_amount <1)
            {
                txt_ingredientCount.text = string.Format("X{0}", inInventory.ToString("00"));
            }
            else if(!_decriptive)
            {
                txt_ingredientCount.text = string.Format("{1}/{0}", _amount.ToString("00"),inInventory.ToString("00"));
                txt_ingredientCount.color = (inInventory >= _amount) ? Color.black : Color.red;
            }
            else
            {
                txt_ingredientCount.text = string.Format("X {0}", _amount.ToString("00"));
                txt_ingredientCount.color = (inInventory >= _amount) ? Color.black : Color.red;
            }
            
            go_ingredientDetails.SetActive(true);
        }
    }
}