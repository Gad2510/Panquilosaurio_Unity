using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Dinopostres.CharacterControllers;
using Dinopostres.Definitions;

namespace Dinopostres.UIElements
{
    public class UIDinoDes : MonoBehaviour, ISelectHandler
    {

        [SerializeField]
        TextMeshProUGUI txt_name;
        [SerializeField]
        TextMeshProUGUI txt_power;
        [SerializeField]
        Slider sl_healthBar;
        [SerializeField]
        Image img_dinoImg;

        Button btn_ChangeDino;
        private DinoSaveData dsd_currentData;

        UnityEngine.Events.UnityAction onClick;
        UnityEvent ue_onSelect;

        public int _ParentIndex { get => transform.GetSiblingIndex(); }

        private void Awake()
        {
            btn_ChangeDino = GetComponent<Button>();

            ue_onSelect = new UnityEvent();
        }

        public void InitStats(DinoSaveData _dinoData, UnityEngine.Events.UnityAction _ev)
        {
            if (onClick != null)
                RemoveBtnClicEvent(onClick);
            onClick = _ev;
            AddBtnClicEvent(onClick);
            dsd_currentData = _dinoData;

            QuickRelodStats();
        }

        public void QuickRelodStats()
        {
            if (txt_name != null)
                txt_name.text = dsd_currentData.Dino.ToString();
            if (txt_power != null)
                txt_power.text = string.Format("CP {0}", dsd_currentData.Power.ToString());

            if (img_dinoImg != null)
                img_dinoImg.sprite = EnemyStorage._Instance().GetDinoImage(dsd_currentData.Dino);

            if (sl_healthBar != null)
            {
                float maxHealth = dsd_currentData.MaxHealth;
                sl_healthBar.value = dsd_currentData.CurrentHealth / maxHealth;
            }
        }

        public DinoSaveData ReturnStoreData()
        {
            return dsd_currentData;
        }
        public void AddBtnClicEvent(UnityEngine.Events.UnityAction _ev)
        {
            btn_ChangeDino.onClick.AddListener(_ev);
        }

        public void RemoveBtnClicEvent(UnityEngine.Events.UnityAction _ev)
        {
            btn_ChangeDino.onClick.RemoveListener(_ev);
        }

        public void AddBtnSelectedEvent(UnityEngine.Events.UnityAction _ev)
        {
            ue_onSelect.AddListener(_ev);
        }

        public void SetButtonAsSelected()
        {
            btn_ChangeDino.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            InvokeSelect();
        }

        private void InvokeSelect()
        {
            //Debug.Log($"Dino ID: {dsd_currentData.ID}");
            ue_onSelect.Invoke();
        }
    }
}