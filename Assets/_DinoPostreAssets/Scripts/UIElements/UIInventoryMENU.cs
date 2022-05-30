using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Dinopostres.Definitions;
using Dinopostres.Managers;

namespace Dinopostres.UIElements
{
    public class UIInventoryMENU : MonoBehaviour
    {
        [SerializeField]
        Button btn_return;

        private UIIngredientDes[] arr_Ingredients;
        int activeSlots = 0;
        // Start is called before the first frame update
        void Awake()
        {
            btn_return.onClick.AddListener(() => LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.inventory, false));
        }

        private void OnEnable()
        {
            if (arr_Ingredients == null)
            {
                arr_Ingredients = GetComponentsInChildren<UIIngredientDes>();
                arr_Ingredients = arr_Ingredients.OrderBy((x) => x._Ingredient).ToArray();
            }

            if (activeSlots < GameManager._instance._GameData.Inventory.Count)
            {
                activeSlots = GameManager._instance._GameData.Inventory.Count;
                UpdateSlots();
            }
            btn_return.Select();
        }

        private void UpdateSlots()
        {
            for(int i=0; i < activeSlots; i++)
            {
                arr_Ingredients[i].UpdateDescriptions(GameManager._instance._GameData.Inventory[i]._Ingredient);
            }
        }
    }
}