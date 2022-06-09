using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Definitions;
namespace Dinopostres.Managers
{
    public class GameModeINSTAGE : GameMode
    {
        public const int int_maxLives = 3;
        private int int_lives;

        public int _Lives { get => int_lives; }
        public int _LivesInverse { get => int_maxLives - int_lives; }
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

        protected override void Awake()
        {
            int_lives = int_maxLives;
            base.Awake();
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

        public void LoseLive()
        {
            int_lives--;
            GameMode.OnPlayerDead.Invoke(null);
            if (int_lives <= 0 || (_GameData._DinoInventory.Count - _LivesInverse) <= 0)

            {
                LevelManager._Instance.LoadLevel("Criadero");
            }
        }

        public DinoSaveData GetActiveDino()
        {
            return _GameData.GetActive();
        }
    }
}