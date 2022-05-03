using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Managers
{
    public abstract class GameMode : MonoBehaviour
    {

        public enum MenuDef
        {
            pause,
            inventory,
            dispacher,
            status,
            gameplay,
            menu,
            settings,
            load,
            decriptions,
            none
        }
        protected MenuDef enm_lastMenu;
        protected Dictionary<MenuDef, string> dic_menuRef =new Dictionary<MenuDef, string>
        {
            {MenuDef.pause, "UI_PauseMenu" },
            {MenuDef.inventory, "UI_InventoryMenu" },
            {MenuDef.dispacher, "UI_Dispacher" },
            {MenuDef.status, "UI_ControllersStatus" },
            {MenuDef.gameplay, "UI_Gameplay" },
            {MenuDef.menu, "" },
            {MenuDef.settings, "" },
            {MenuDef.load, "" },
            {MenuDef.decriptions, "" },
        };

        protected Dictionary<MenuDef, GameObject> dic_menus;

        public MenuDef _LatMenu { get => enm_lastMenu; }
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            InitMenus();
            enm_lastMenu = MenuDef.none;
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected abstract void InitMenus();

        protected GameObject LoadGameMenu(string _name, bool _active = false)
        {
            Object pref = Resources.Load<Object>($"Prefabs/{_name}");
            GameObject var = Instantiate(pref) as GameObject;
            var.SetActive(_active);

            return var;
        }
    }
}