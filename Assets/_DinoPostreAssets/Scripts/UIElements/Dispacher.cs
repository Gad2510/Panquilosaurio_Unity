using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dinopostres.CharacterControllers;
using Dinopostres.Definitions;

namespace Dinopostres.UIElements
{
    public class Dispacher : MonoBehaviour
    {
        [SerializeField]
        private Transform trns_DescriptionParent;
        private UIDinoDes [] arr_UIDinoDescriptions;
        int currentIndex;

        public PlayerData test;

        // Start is called before the first frame update
        void Start()
        {
            arr_UIDinoDescriptions = trns_DescriptionParent.GetComponentsInChildren<UIDinoDes>();
            arr_UIDinoDescriptions = arr_UIDinoDescriptions.OrderBy((x) => x.name).ToArray();
            currentIndex = 0;

            InitBtnEvents();
            InitUIVisuals();
            InitUiValues();
        }

        private void OnEnable()
        {
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
                label.AddBtnEvent(() => gameObject.SetActive(false));
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
            arr_UIDinoDescriptions[0].SetButtonAsSelected();
        }
        private void InitUiValues()
        {
            for(int i =0; i< test.DinoInventory.Count() && i<8; i++)
            {
                arr_UIDinoDescriptions[i].InitStats(test.DinoInventory[i]);
            }
        }
        public void Test()
        {
            Debug.Log("TEST");
        }
    }
}