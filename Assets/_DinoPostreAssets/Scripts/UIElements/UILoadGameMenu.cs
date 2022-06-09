using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dinopostres.Definitions;
using Dinopostres.Managers;
using UnityEngine.InputSystem;

namespace Dinopostres.UIElements
{
    public class UILoadGameMenu : MonoBehaviour
    {
        [SerializeField]
        private Transform trns_loadBtnsParnet;
        [SerializeField]
        private Button btn_back;

        private UILoadGameBtn[] arr_LoadBtns;
        private UILoadGameBtn UILBtn_CurrentBtn;
        // Start is called before the first frame update
        private void Start()
        {
            arr_LoadBtns = GetComponentsInChildren<UILoadGameBtn>();
            UpdateLoadBtns();

            btn_back.onClick.AddListener(() => {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.load, false);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.menu, true);
            });


            arr_LoadBtns[0].SetButtonAsSelected();
        }

        private void OnEnable()
        {
            if(arr_LoadBtns!= null)
                arr_LoadBtns[0].SetButtonAsSelected();

            GameMode._Instance.SetControllerFuntions(ControllersManager.PlayerActions.Return,
                ControllersManager.InputState.Perform, DeleteSaveData);
        }

        private void OnDisable()
        {
            GameMode._Instance.SetControllerFuntions(ControllersManager.PlayerActions.Return,
                ControllersManager.InputState.Perform, DeleteSaveData,false);
        }

        private void DeleteSaveData(InputAction.CallbackContext ctx)
        {
            if (UILBtn_CurrentBtn == null)
                return;
            PlayerData pl = UILBtn_CurrentBtn.StoreData;
            MemoryManager.DeleteGame(pl._ID);

            UpdateLoadBtns();
        }

        private void UpdateLoadBtns()
        {
            List<PlayerData> games= MemoryManager.LoadGames();

            foreach(UILoadGameBtn uiBtn in arr_LoadBtns)
            {
                if (uiBtn._ParentIndex<games.Count)
                {
                    uiBtn.AddBtnSelectedEvent(() => { UILBtn_CurrentBtn = uiBtn; });
                    uiBtn.InitStats(games[uiBtn._ParentIndex], () => {
                        GameManager._instance.LoadGame(games[uiBtn._ParentIndex]._ID);
                        LevelManager._Instance.LoadLevel("Criadero");
                    });
                }
                else
                {
                    uiBtn.AddBtnSelectedEvent(() => { UILBtn_CurrentBtn = null; });
                    uiBtn.InitStats(null, () => { });
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

        }
    }
}