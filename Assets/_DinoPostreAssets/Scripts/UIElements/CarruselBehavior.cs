using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Dinopostres.CharacterControllers;
using Dinopostres.Managers;
using Dinopostres.Interfeces;

namespace Dinopostres.UIElements
{
    public abstract class CarruselBehavior<T> : MonoBehaviour, IUIMultipleMenu
    {
        [SerializeField]
        private Transform trns_itemParents;

        protected UIDescriptions<T>[] arr_items;

        protected int int_lastIndex;
        protected int int_currentIndex;

        protected virtual void Start()
        {
            arr_items = trns_itemParents.GetComponentsInChildren<UIDescriptions<T>>();
            arr_items = arr_items.OrderBy((x) => x.transform.GetSiblingIndex()).ToArray();
            int_currentIndex = 0;
            int_lastIndex = 0;

            InitBtnEvents();
            InitUiValues();
            InitUIVisuals();
        }
        protected abstract void SetButtonEvent(UIDescriptions<T> _info);
        protected abstract UnityAction GetDesciptionEvent(T _item);
        protected abstract void InitUIVisuals();
        protected abstract void InitUiValues();
        protected abstract T SetItemValue(int _index);

        protected virtual void OnEnable()
        {
            if (arr_items != null)
            {
                InitBtnEvents();
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

        protected virtual void BlockPlayerInputs()
        {
            Player.PL_Instance.IsDispacheOpen = true;
            if (arr_items != null)
                arr_items[int_currentIndex].SetButtonAsSelected();
        }

        public void SetFirstButtonSelected()
        {
            if (arr_items == null)
                return;
            arr_items[0].SetButtonAsSelected();
        }

        protected void InitBtnEvents()
        {
            foreach (UIDescriptions<T> label in arr_items)
            {
                SetButtonEvent(label);
            }
        }

        protected void MoveDinoUI(int _siblingIndex, int _inventoryCount)
        {
            T saveData;
            if (_siblingIndex == 0)
            {
                int_currentIndex -= 1;
                if (int_currentIndex > 0)
                {
                    saveData = SetItemValue(int_currentIndex - 1);
                    trns_itemParents.GetChild(arr_items.Length-1).SetAsFirstSibling();
                    arr_items[(int_currentIndex - 1) % arr_items.Length].InitStats(saveData, GetDesciptionEvent(saveData));
                }

            }
            else if (_siblingIndex == arr_items.Length - 1)
            {
                int_currentIndex += 1;
                if (int_currentIndex < _inventoryCount)
                {
                    saveData = SetItemValue(int_currentIndex + 1);
                    trns_itemParents.GetChild(0).SetAsLastSibling();
                    arr_items[(int_currentIndex + 1) % arr_items.Length].InitStats(saveData, GetDesciptionEvent(saveData));
                }
            }
            else
            {
                int_currentIndex += (int_lastIndex > _siblingIndex) ? -1 : 1;
            }

            int_currentIndex = Mathf.Clamp(int_currentIndex, 0, _inventoryCount);
        }
    }
}