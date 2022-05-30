using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Dinopostres.CharacterControllers;
using Dinopostres.Definitions;
using Dinopostres.Managers;

namespace Dinopostres.UIElements
{
    public class Dispacher : CarruselBehavior <DinoSaveData>
    {
        [Header("Dispacher - Texts")]
        [SerializeField]
        private TextMeshProUGUI txt_descriptionName;
        [SerializeField]
        private TextMeshProUGUI txt_descriptionPower;
        [SerializeField]
        protected TextMeshProUGUI txt_descriptionPS;
        [Header("Dispacher - Sliders")]
        [SerializeField]
        protected Slider sl_healthRef;
        
        protected override void SetButtonEvent(UIDescriptions<DinoSaveData> _uiDino)
        {
            _uiDino = (UIDinoDes)_uiDino;
            _uiDino.AddBtnClicEvent(() => GetDesciptionEvent(_uiDino.StoreData));
            _uiDino.AddBtnSelectedEvent(() => {
                UpdateDescription(_uiDino.ReturnStoreData());
                MoveDinoUI(_uiDino._ParentIndex, GameManager._instance._GameData.DinoInventory.Count() - 1);
                int_lastIndex = _uiDino._ParentIndex;

            });
        }
        protected override void InitUIVisuals()
        {
            if (GameManager._instance._GameData.DinoInventory.Count() < arr_items.Length)
            {
                for (int i = arr_items.Length - 1;i>=0; i--)
                {
                    arr_items[i].gameObject.SetActive(i < GameManager._instance._GameData.DinoInventory.Count());
                }
            }
            arr_items[int_currentIndex].SetButtonAsSelected();
        }
        protected override void InitUiValues()
        {
            for(int i =0; i< GameManager._instance._GameData.DinoInventory.Count() && i< arr_items.Length; i++)
            {
                DinoSaveData saveData = GameManager._instance._GameData.DinoInventory[i];
                arr_items[i].InitStats(saveData, GetDesciptionEvent(saveData));
            }
        }
        protected void UpdateDescription(DinoSaveData _info)
        {
            txt_descriptionName.text = _info.Dino.ToString();
            txt_descriptionPower.text=_info.Power.ToString();

            UpdateSliders(_info);
        }

        protected virtual void UpdateSliders(DinoSaveData _info) {
            float maxHealth = _info.MaxHealth;
            txt_descriptionPS.text = $"{_info.CurrentHealth} / {maxHealth}";
            sl_healthRef.value = _info.CurrentHealth / maxHealth;
        }

        protected override DinoSaveData SetItemValue(int _index)
        {
            return GameManager._instance._GameData.DinoInventory[_index];
        }

        protected override UnityAction GetDesciptionEvent(DinoSaveData _item)
        {
            return () => {
                if (_item.CurrentHealth > 0)
                {
                    Player.PL_Instance.SwitchDino(_item);
                    LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.dispacher, false);
                }
            };
        }
    }
}