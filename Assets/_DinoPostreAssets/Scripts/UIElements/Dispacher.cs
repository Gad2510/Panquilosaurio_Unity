using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.CharacterControllers;
using Dinopostres.Definitions;

namespace Dinopostres.UIElements
{
    public class Dispacher : MonoBehaviour
    {
        [SerializeField]
        private Transform trns_DescriptionParent;
        [SerializeField]
        private TextMeshProUGUI txt_DescriptionName;
        [SerializeField]
        private TextMeshProUGUI txt_DescriptionPower;
        [SerializeField]
        private TextMeshProUGUI txt_DescriptionPS;
        [SerializeField]
        private Slider sl_healthRef;

        private UIDinoDes [] arr_UIDinoDescriptions;
        private int int_lastIndex;
        public int int_currentIndex;

        public PlayerData test;

        // Start is called before the first frame update
        void Start()
        {
            arr_UIDinoDescriptions = trns_DescriptionParent.GetComponentsInChildren<UIDinoDes>();
            arr_UIDinoDescriptions = arr_UIDinoDescriptions.OrderBy((x) => x.name).ToArray();
            int_currentIndex = 0;
            int_lastIndex = 0;

            InitBtnEvents();
            InitUiValues();
            InitUIVisuals();
        }

        private void OnEnable()
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

        private void InitBtnEvents()
        {
            foreach(UIDinoDes label in arr_UIDinoDescriptions)
            {
                label.AddBtnClicEvent(() => gameObject.SetActive(false));
                label.AddBtnSelectedEvent(() => { 
                    UpdateDescription(label.ReturnStoreData());
                    MoveDinoUI(label._ParentIndex);
                    int_lastIndex = label._ParentIndex;

                });
            }
        }
        private void InitUIVisuals()
        {
            if (test.DinoInventory.Count() < 8)
            {
                for (int i = 7; i >= test.DinoInventory.Count(); i--)
                {
                    arr_UIDinoDescriptions[i].gameObject.SetActive(false);
                }
            }
            arr_UIDinoDescriptions[int_currentIndex].SetButtonAsSelected();
        }
        private void InitUiValues()
        {
            for(int i =0; i< test.DinoInventory.Count() && i<8; i++)
            {
                arr_UIDinoDescriptions[i].InitStats(test.DinoInventory[i]);
            }
        }

        private void MoveDinoUI(int _siblingIndex)
        {

            
            switch (_siblingIndex)
            {
                case 0:
                    int_currentIndex -= 1;
                    if (int_currentIndex > 0)
                    {
                        trns_DescriptionParent.GetChild(7).SetAsFirstSibling();
                        arr_UIDinoDescriptions[(int_currentIndex-1)%8].InitStats(test.DinoInventory[int_currentIndex-1]);
                    }
                    
                    break;
                case 7:
                    int_currentIndex += 1;
                    if (int_currentIndex< test.DinoInventory.Count()-1)
                    {
                        trns_DescriptionParent.GetChild(0).SetAsLastSibling();
                        arr_UIDinoDescriptions[(int_currentIndex+1)% 8].InitStats(test.DinoInventory[int_currentIndex+1]);
                    }
                    
                    break;
                default:
                    int_currentIndex += (int_lastIndex > _siblingIndex) ? -1 : 1;
                    break;
            }
            int_currentIndex = Mathf.Clamp(int_currentIndex, 0, test.DinoInventory.Count() - 1);
        }

        private void UpdateDescription(DinoSaveData _info)
        {
            txt_DescriptionName.text = _info.Dino.ToString();
            txt_DescriptionPower.text= DinoSpecsDef.Instance().CalculatePower(_info.Dino, _info.Level).ToString();

            float maxHealth = DinoSpecsDef.Instance().LookForStats(_info.Dino).CalculateCurrentValue(DinoStatsDef.Stats.HP, _info.Level);
            txt_DescriptionPS.text = $"{_info.CurrentHealth} / {maxHealth}";
            sl_healthRef.value = _info.CurrentHealth / maxHealth;
        }
    }
}