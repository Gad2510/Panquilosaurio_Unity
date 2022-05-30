using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.InputSystem;
using Dinopostres.Managers;

namespace Dinopostres.UIElements
{
    public class UIPauseMENU : MonoBehaviour
    {
        [SerializeField]
        private Button btn_resume;
        [SerializeField]
        private Button btn_exit;
        [SerializeField]
        private Button btn_inventory;
        [SerializeField]
        private GameObject go_PauseMenu;
        [SerializeField]
        private GameObject go_InventoryMenu;

        private const int int_mask = (int)GameMode.MenuDef.decriptions | (int)GameMode.MenuDef.none | (int)GameMode.MenuDef.pause;
        // Start is called before the first frame update
        void Awake()
        {
            GameManager._instance.InS_gameActions.DinopostreController.Pause.performed +=PauseGame;
            btn_resume.onClick.AddListener(() => PauseGame());
            btn_exit.onClick.AddListener(() =>ExitStage());
            btn_inventory.onClick.AddListener(() => LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.inventory, true));
        }

        private void OnEnable()
        {
            btn_resume.Select();
        }
        private void OnDestroy()
        {
            GameManager._instance.InS_gameActions.DinopostreController.Pause.performed -= PauseGame;
        }
        private void PauseGame(InputAction.CallbackContext _ctx)
        {
            if(((int)LevelManager._Instance._GameMode._LastMenu & int_mask) >=1 )
            {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.pause,!gameObject.activeSelf);

                GameManager._instance.PauseGame(this.gameObject.activeSelf);
            }
            
        }

        private void PauseGame()
        {
            LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.pause, !gameObject.activeSelf);

            GameManager._instance.PauseGame(this.gameObject.activeSelf);
        }

        private void ExitStage()
        {
            PauseGame();
            LevelManager._Instance.LoadLevel((LevelManager._Instance._Stage != LevelManager.GameStates.Map)?"Criadero":"Menu");
        }

        
    }
}