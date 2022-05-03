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

        // Start is called before the first frame update
        void Awake()
        {

        }

        private void OnEnable()
        {
            if(enm_ingredient== IngredientDef.Sample.none)
            {
                go_ingredientDetails.SetActive(false);
            }
        }

        public void UpdateDescriptions(IngredientDef.Sample _type)
        {
            Debug.Log($"Update to {_type}");
            enm_ingredient = _type;
            img_ingredientImg.sprite = Ingredients.Instance().GetIngredientVisual(enm_ingredient);
            txt_ingredientCount.text =string.Format( "X{0}",GameManager._instance.PD_gameData.GetIngredientCount(enm_ingredient).ToString("00"));
            go_ingredientDetails.SetActive(true);
        }
    }
}