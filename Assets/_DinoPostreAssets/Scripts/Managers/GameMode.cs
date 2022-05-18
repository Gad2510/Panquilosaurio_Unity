using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Dinopostres.Managers
{
    public abstract class GameMode : MonoBehaviour
    {

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
            none = 1<< 0
        }
        [SerializeField]
        private Stack<MenuDef> stk_lastMenu;
       

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
            {MenuDef.decriptions, "UI_Descriptions" },
            {MenuDef.oven, "UI_Oven" }
        };

        protected Dictionary<MenuDef, GameObject> dic_menus;
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
            Debug.Log("Init menus");
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
            Debug.Log(_menu);
            if (!dic_menus.ContainsKey(_menu))
                return;
            Debug.Log($"{stk_lastMenu.Count} || CurrentMenu");
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
    }
}