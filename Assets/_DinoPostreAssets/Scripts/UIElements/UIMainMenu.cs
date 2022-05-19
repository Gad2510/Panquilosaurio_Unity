using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dinopostres.Managers;
namespace Dinopostres.UIElements
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField]
        Button btn_newGame;
        [SerializeField]
        Button btn_loadGame;
        [SerializeField]
        Button btn_settings;

        private int int_gameCount;
        // Start is called before the first frame update
        void Start()
        {
            int_gameCount = MemoryManager.GamesCount();
            btn_newGame.gameObject.SetActive(int_gameCount < 3);
            btn_loadGame.gameObject.SetActive(int_gameCount > 0);

            btn_newGame.onClick.AddListener(() => {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.menu, false);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.newGame, true); });
            btn_loadGame.onClick.AddListener(() => {

                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.menu, false);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.load, true); });
            btn_settings.onClick.AddListener(() => {

                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.menu, false);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.settings, true); });

            if(int_gameCount < 3)
            {
                btn_newGame.Select();
            }
            else
            {
                btn_loadGame.Select();
            }
            
        }

        private void OnEnable()
        {
            if (int_gameCount < 3)
            {
                btn_newGame.Select();
            }
            else
            {
                btn_loadGame.Select();
            }
        }
    }
}