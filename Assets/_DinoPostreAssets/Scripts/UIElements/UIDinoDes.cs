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

        Button btn_ChangeDino;
        public DinoSaveData dsd_currentData;

        UnityEngine.Events.UnityAction onClick;
        UnityEvent ue_onSelect;

        public int _ParentIndex { get => transform.GetSiblingIndex(); }

        private void Awake()
        {
            btn_ChangeDino = GetComponent<Button>();

            ue_onSelect = new UnityEvent();
        }

        public void InitStats(DinoSaveData _dinoData)
        {
            if (onClick != null)
                RemoveBtnClicEvent(onClick);

            txt_name.text = _dinoData.Dino.ToString();
            txt_power.text = DinoSpecsDef.Instance().CalculatePower(_dinoData.Dino,_dinoData.Level).ToString();

            float maxHealth = DinoSpecsDef.Instance().LookForStats(_dinoData.Dino).CalculateCurrentValue(DinoStatsDef.Stats.HP, _dinoData.Level);
            sl_healthBar.value = _dinoData.CurrentHealth /maxHealth;

            onClick = () => { Player.PL_Instance.SwitchDino(_dinoData); };
            AddBtnClicEvent(onClick);

            dsd_currentData = _dinoData;
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

        public void AddBtnSelectEvent(UnityEngine.EventSystems.PointerEventData _ev)
        {
            btn_ChangeDino.OnPointerEnter(_ev);
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