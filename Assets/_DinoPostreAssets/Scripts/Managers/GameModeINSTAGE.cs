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
                if (dic_menus.ContainsKey(MenuDef.status) )
                {
                    if(dic_menus[MenuDef.status] == null)
                    {
                        dic_menus[MenuDef.status] = LoadGameMenu(dic_menuRef[MenuDef.status], true);
                    }
                    return dic_menus[MenuDef.status];
                }
                    

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
            dic_menus.Add(MenuDef.controllers, LoadGameMenu(dic_menuRef[MenuDef.controllers],true));
        }
    }
}