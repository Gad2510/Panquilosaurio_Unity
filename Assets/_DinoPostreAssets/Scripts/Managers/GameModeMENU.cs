using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Managers
{
    public class GameModeMENU : GameMode
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void InitMenus()
        {
            dic_menus = new Dictionary<MenuDef, GameObject>();
            dic_menus.Add(MenuDef.menu, LoadGameMenu(dic_menuRef[MenuDef.menu], true));
            dic_menus.Add(MenuDef.settings, LoadGameMenu(dic_menuRef[MenuDef.settings]));
            dic_menus.Add(MenuDef.load, LoadGameMenu(dic_menuRef[MenuDef.load]));
            dic_menus.Add(MenuDef.newGame, LoadGameMenu(dic_menuRef[MenuDef.newGame]));
        }
    }
}