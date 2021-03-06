using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Dinopostres.Definitions;
namespace Dinopostres.Managers
{
    public abstract class GameMode : MonoBehaviour
    {
        public delegate void EventListener(Events.Event ev);
        public static EventListener OnRecordEvent;
        public static EventListener OnPlayerDead;

        public enum MenuDef
        {
            pause=1<<1,
            inventory=1<<2,
            dispacher=1<<3,
            status=1<<4,
            gameplay = 1 << 5,
            menu = 1 << 6,
            settings = 1 << 7,
            load = 1 << 8,
            decriptions = 1 << 9,
            oven = 1 << 10,
            newGame= 1<<11,
            controllers= 1<<12,
            none = 1<< 0
        }
        private static GameMode GM_instance;
        private Stack<MenuDef> stk_lastMenu;
       

        protected Dictionary<MenuDef, string> dic_menuRef =new Dictionary<MenuDef, string>
        {
            {MenuDef.pause, "UI_PauseMenu" },
            {MenuDef.inventory, "UI_InventoryMenu" },
            {MenuDef.dispacher, "UI_Dispacher" },
            {MenuDef.status, "UI_ControllersStatus" },
            {MenuDef.gameplay, "UI_Gameplay" },
            {MenuDef.menu, "CV_MainMenu" },
            {MenuDef.settings, "CV_Settings" },
            {MenuDef.load, "CV_LoadGame" },
            {MenuDef.decriptions, "UI_Descriptions" },
            {MenuDef.oven, "UI_Oven" },
            {MenuDef.newGame, "CV_NewGame" },
            {MenuDef.controllers, "UI_Controllers" },
        };

        protected Dictionary<MenuDef, GameObject> dic_menus;
        public static GameMode _Instance => GM_instance;
        public float _Time => GameManager._Time;
        public float _TimeScale => GameManager._Time*Time.deltaTime;
        public PlayerData _GameData => GameManager._instance._GameData;
        public MenuDef _LastMenu { 
            get {
                if(stk_lastMenu.Count > 0)
                    return stk_lastMenu.Peek();
                else
                {
                    return MenuDef.none;
                }
            }           
        }
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            GM_instance = this;
            InitMenus();
            stk_lastMenu = new Stack<MenuDef>();
        }


        private void OnEnable()
        {
            LevelManager._Instance.SetLoadEvent(OnLoadLevel, true);
        }

        private void OnDisable()
        {
            LevelManager._Instance.SetLoadEvent(OnLoadLevel, false);
        }

        protected virtual void OnDestroy()
        {
            if (dic_menus==null || dic_menus.Count == 0)
                return;

            MenuDef[] keys = dic_menus.Keys.ToArray();
            foreach(MenuDef key in keys)
            {
                Destroy(dic_menus[key]);
            }
        }

        protected void OnLoadLevel(UnityEngine.SceneManagement.Scene _scene, UnityEngine.SceneManagement.LoadSceneMode _mode)
        {
            InitMenus();
        }

        protected abstract void InitMenus();

        protected GameObject LoadGameMenu(string _name, bool _active = false)
        {
            Object pref = Resources.Load<Object>($"Prefabs/{_name}");
            GameObject var = Instantiate(pref) as GameObject;
            var.SetActive(_active);

            return var;
        }

        public void OpenCloseSpecicficMenu(MenuDef _menu, bool _state)
        {
            if (!dic_menus.ContainsKey(_menu))
                return;

            if (stk_lastMenu.Count>0 &&  !_state)
            {
                stk_lastMenu.Pop();
                if(stk_lastMenu.Count > 0)
                    dic_menus[stk_lastMenu.Peek()].SetActive(true);
            }
            dic_menus[_menu].SetActive(_state);

            if (_state)
            {
                if (stk_lastMenu.Count > 0 && stk_lastMenu.Peek() != _menu)
                {
                    dic_menus[stk_lastMenu.Peek()].SetActive(false);
                    stk_lastMenu.Push(_menu);
                }
                if(stk_lastMenu.Count == 0)
                {
                    stk_lastMenu.Push(_menu);
                }
            }
        }

        public void SetControllerFuntions(ControllersManager.PlayerActions _action, ControllersManager.InputState _state, System.Action<InputAction.CallbackContext> _function, bool _set = true)
        {
            if(_set)
                ControllersManager._instance.AddComand(_action, _state, _function);
            else
                ControllersManager._instance.RemoveComand(_action, _state, _function);
        }

        public InputAction GetControllerValue(ControllersManager.PlayerActions _action)
        {
            return ControllersManager._instance.ReturnValue(_action);
        }
    }
}