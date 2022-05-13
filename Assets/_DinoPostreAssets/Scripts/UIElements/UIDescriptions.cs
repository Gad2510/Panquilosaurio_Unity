using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Dinopostres.UIElements
{
    public abstract class UIDescriptions<T> : MonoBehaviour, ISelectHandler
    {
        [SerializeField]
        protected TextMeshProUGUI txt_name;

        [SerializeField]
        protected Button btn_executer;

        protected T storeData;
        protected UnityAction onClick;
        protected UnityAction onClickInitSet;
        protected UnityEvent ue_onSelect;

        public T StoreData { get => storeData; }

        protected virtual void Awake()
        {
            ue_onSelect = new UnityEvent();
        }

        //Reload NOn static Data
        public abstract void QuickRelodStats();
        public int _ParentIndex { get => transform.GetSiblingIndex(); }

        //Reload All information
        public virtual void InitStats(T _data, UnityAction _ev)
        {
            if (onClick != null)
                RemoveBtnClicEvent(onClick);

            onClick = _ev;
            AddBtnClicEvent(onClick);
            storeData = _data;

            QuickRelodStats();
        }

        public T ReturnStoreData()
        {
            return storeData;
        }
        public void AddBtnClicEvent(UnityAction _ev)
        {
            if (onClickInitSet != null)
                RemoveBtnClicEvent(onClickInitSet);

            onClickInitSet = _ev;
            btn_executer.onClick.AddListener(onClickInitSet);
        }
        public void RemoveBtnClicEvent(UnityAction _ev)
        {
            btn_executer.onClick.RemoveListener(_ev);
        }
        public void AddBtnSelectedEvent(UnityAction _ev)
        {
            ue_onSelect.RemoveAllListeners();
            ue_onSelect.AddListener(_ev);
        }
        public void SetButtonAsSelected()
        {
            btn_executer.Select();
        }
        public void OnSelect(BaseEventData eventData)
        {
            InvokeSelect();
        }
        protected void InvokeSelect()
        {
            ue_onSelect.Invoke();
        }
    }
}