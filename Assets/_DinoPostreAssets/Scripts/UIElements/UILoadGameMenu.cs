using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Definitions;
using Dinopostres.Managers;
namespace Dinopostres.UIElements
{
    public class UILoadGameMenu : MonoBehaviour
    {
        [SerializeField]
        private Transform trns_loadBtnsParnet;


        private UILoadGameBtn[] arr_LoadBtns;
        private int int_CurrentIndex;
        // Start is called before the first frame update
        void Start()
        {
            arr_LoadBtns = GetComponentsInChildren<UILoadGameBtn>();
            UpdateLoadBtns();
        }

        private void OnEnable()
        {
            if(arr_LoadBtns!= null)
                arr_LoadBtns[0].SetButtonAsSelected();
        }

        private void UpdateLoadBtns()
        {
            List<PlayerData> games= MemoryManager.LoadGames();

            foreach(UILoadGameBtn uiBtn in arr_LoadBtns)
            {
                if (uiBtn._ParentIndex<games.Count)
                {
                    uiBtn.InitStats(games[uiBtn._ParentIndex], () => {
                        GameManager._instance.LoadGame(games[uiBtn._ParentIndex].ID);
                        LevelManager._Instance.LoadLevel("Criadero");
                    });
                }
                /*else
                {
                    uiBtn.InitStats(null, () => {
                        MemoryManager.NewGame(arr_LoadBtns[uiBtn._ParentIndex]._Name);
                        GameManager._instance.LoadGame(arr_LoadBtns[uiBtn._ParentIndex]._Name);
                        LevelManager._Instance.LoadLevel("Criadero");
                    });
                }*/
                
            }

            arr_LoadBtns[0].SetButtonAsSelected();
        }
    }
}