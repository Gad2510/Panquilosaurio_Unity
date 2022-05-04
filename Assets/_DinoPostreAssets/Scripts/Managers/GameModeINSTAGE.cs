using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Managers
{
    public class GameModeINSTAGE : GameMode
    {
        
        public GameObject _Status
        {
            get
            {
                Debug.Log($"Status Contains key {dic_menus.ContainsKey(MenuDef.status)} | Contains value {dic_menus[MenuDef.status] != null}");
                if (dic_menus.ContainsKey(MenuDef.status) )
                {
                    if(dic_menus[MenuDef.status] == null)
                    {
                        dic_menus[MenuDef.status] = LoadGameMenu(dic_menuRef[MenuDef.status], true);
                    }
                    return dic_menus[MenuDef.status];
                }
                    

                dic_menus.Add(MenuDef.status, LoadGameMenu(dic_menuRef[MenuDef.status], true));
                return dic_menus[MenuDef.status];
            }
        }
        protected override void InitMenus()
        {
            dic_menus = new Dictionary<MenuDef, GameObject>();
            dic_menus.Add(MenuDef.dispacher, LoadGameMenu(dic_menuRef[MenuDef.dispacher]));
            dic_menus.Add(MenuDef.pause, LoadGameMenu(dic_menuRef[MenuDef.pause]));
            dic_menus.Add(MenuDef.inventory, LoadGameMenu(dic_menuRef[MenuDef.inventory]));
            dic_menus.Add(MenuDef.status, LoadGameMenu(dic_menuRef[MenuDef.status],true));
            dic_menus.Add(MenuDef.gameplay, LoadGameMenu(dic_menuRef[MenuDef.gameplay],true));
        }

        public void OpenCloseDispacher(bool _state)
        {
            if (enm_lastMenu == MenuDef.none)
                dic_menus[MenuDef.dispacher].SetActive(_state);
        }

        public void OpenOnventory(bool fromPause = false)
        {
            enm_lastMenu = (fromPause) ? MenuDef.pause : MenuDef.none;
            dic_menus[MenuDef.pause].SetActive(false);
            dic_menus[MenuDef.inventory].SetActive(true);
        }

        public void CloseInventory()
        {
            dic_menus[MenuDef.inventory].SetActive(false);
            if (enm_lastMenu == MenuDef.pause)
                dic_menus[MenuDef.pause].SetActive(true);
        }
    }
}