using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.CharacterControllers;
using Dinopostres.Definitions;
using Dinopostres.Managers;

namespace Dinopostres.UIElements
{
    public class Dispacher : MonoBehaviour
    {
        [SerializeField]
        private Transform trns_DescriptionParent;
        [Header("Dispacher - Texts")]
        [SerializeField]
        private TextMeshProUGUI txt_DescriptionName;
        [SerializeField]
        private TextMeshProUGUI txt_DescriptionPower;
        [SerializeField]
        protected TextMeshProUGUI txt_DescriptionPS;
        [Header("Dispacher - Sliders")]
        [SerializeField]
        protected Slider sl_healthRef;

        private UIDinoDes [] arr_UIDinoDescriptions;
        protected int int_lastIndex;
        protected int int_currentIndex;

        // Start is called before the first frame update
        protected void Start()
        {
            arr_UIDinoDescriptions = trns_DescriptionParent.GetComponentsInChildren<UIDinoDes>();
            arr_UIDinoDescriptions = arr_UIDinoDescriptions.OrderBy((x) => x.name).ToArray();
            int_currentIndex = 0;
            int_lastIndex = 0;

            InitBtnEvents();
            InitUiValues();
            InitUIVisuals();
        }

        protected void OnEnable()
        {
            if (arr_UIDinoDescriptions != null)
            {
                InitUiValues();
                InitUIVisuals();
            }

            if (Player.PL_Instance == null)
            {
                Invoke(nameof(BlockPlayerInputs), 0.1f);
                return;
            }
            BlockPlayerInputs();
        }

        private void OnDisable()
        {
            Player.PL_Instance.IsDispacheOpen = false;
        }

        private void BlockPlayerInputs()
        {
            Player.PL_Instance.IsDispacheOpen = true;
        }

        protected void InitBtnEvents()
        {
            foreach(UIDinoDes label in arr_UIDinoDescriptions)
            {
                SetButtonEvent(label);
            }
        }
        protected virtual void SetButtonEvent(UIDinoDes _uiDino)
        {
            _uiDino.AddBtnClicEvent(() => gameObject.SetActive(false));
            _uiDino.AddBtnSelectedEvent(() => {
                UpdateDescription(_uiDino.ReturnStoreData());
                MoveDinoUI(_uiDino._ParentIndex);
                int_lastIndex = _uiDino._ParentIndex;

            });
        }
        private void InitUIVisuals()
        {
            if (GameManager._instance._GameData.DinoInventory.Count() < 8)
            {
                for (int i = 7; i >= GameManager._instance._GameData.DinoInventory.Count(); i--)
                {
                    arr_UIDinoDescriptions[i].gameObject.SetActive(false);
                }
            }
            arr_UIDinoDescriptions[int_currentIndex].SetButtonAsSelected();
        }
        protected void InitUiValues()
        {
            for(int i =0; i< GameManager._instance._GameData.DinoInventory.Count() && i<8; i++)
            {
                DinoSaveData saveData = GameManager._instance._GameData.DinoInventory[i];
                arr_UIDinoDescriptions[i].InitStats(saveData, () => Player.PL_Instance.SwitchDino(saveData));
            }
        }

        protected void MoveDinoUI(int _siblingIndex)
        {
            DinoSaveData saveData = null;
            switch (_siblingIndex)
            {
                case 0:
                    int_currentIndex -= 1;
                    if (int_currentIndex > 0)
                    {
                        saveData = GameManager._instance._GameData.DinoInventory[int_currentIndex - 1];
                        trns_DescriptionParent.GetChild(7).SetAsFirstSibling();
                        arr_UIDinoDescriptions[(int_currentIndex-1)%8].InitStats(saveData,() => Player.PL_Instance.SwitchDino(saveData));
                    }
                    
                    break;
                case 7:
                    int_currentIndex += 1;
                    if (int_currentIndex< GameManager._instance._GameData.DinoInventory.Count()-1)
                    {
                        saveData = GameManager._instance._GameData.DinoInventory[int_currentIndex + 1];
                        trns_DescriptionParent.GetChild(0).SetAsLastSibling();
                        arr_UIDinoDescriptions[(int_currentIndex+1)% 8].InitStats(saveData, () => Player.PL_Instance.SwitchDino(saveData));
                    }
                    
                    break;
                default:
                    int_currentIndex += (int_lastIndex > _siblingIndex) ? -1 : 1;
                    break;
            }
            int_currentIndex = Mathf.Clamp(int_currentIndex, 0, GameManager._instance._GameData.DinoInventory.Count() - 1);
        }

        protected void UpdateDescription(DinoSaveData _info)
        {
            txt_DescriptionName.text = _info.Dino.ToString();
            txt_DescriptionPower.text=_info.Power.ToString();

            UpdateSliders(_info);
        }

        protected virtual void UpdateSliders(DinoSaveData _info) {
            float maxHealth = _info.MaxHealth;
            txt_DescriptionPS.text = $"{_info.CurrentHealth} / {maxHealth}";
            sl_healthRef.value = _info.CurrentHealth / maxHealth;
        }
    }
}