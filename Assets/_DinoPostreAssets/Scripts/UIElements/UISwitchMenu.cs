using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Dinopostres.Managers;
using Dinopostres.Interfeces;
using Dinopostres.CharacterControllers;
namespace Dinopostres.UIElements
{
    public class UISwitchMenu : MonoBehaviour
    {
        [SerializeField]
        private List<UIMenu> lst_menus;

        private int int_index = 0;

        private IEnumerator Start()
        {
            yield return null;
            foreach(UIMenu go in lst_menus)
            {
                go.SetObjectActive=false;
            }

            lst_menus[int_index].SetObjectActive=true;
        }

        // Start is called before the first frame update
        private void OnEnable()
        {
            GameManager._instance.InS_gameActions.DinopostreController.AttackA.performed += MoveMenu;
            GameManager._instance.InS_gameActions.DinopostreController.AttackB.performed += MoveMenu;
            Player.PL_Instance.IsDispacheOpen = true;
        }
        private void OnDisable()
        {
            GameManager._instance.InS_gameActions.DinopostreController.AttackA.performed -= MoveMenu;
            GameManager._instance.InS_gameActions.DinopostreController.AttackB.performed -= MoveMenu;
            Player.PL_Instance.IsDispacheOpen = false;
        }
        public void MoveMenu(InputAction.CallbackContext _ctx)
        {
            lst_menus[int_index].SetObjectActive=false;

            switch (_ctx.action.name)
            {
                case "Attack A":
                    int_index--;
                    if (int_index < 0)
                    {
                        int_index = lst_menus.Count - 1;
                    }
                    break;
                case "Attack B":
                    int_index++;
                    if (int_index >= lst_menus.Count)
                    {
                        int_index = 0;
                    }
                    break;
            }

            lst_menus[int_index].SetObjectActive=true;
        }
    }
    [System.Serializable]
    public class UIMenu
    {
        [SerializeField]
        GameObject go_menu;
        [SerializeField]
        Sprite img_label;
        IUIMultipleMenu UI_carrusselBH;

        public bool SetObjectActive { 
            set {
                go_menu.SetActive(value);

                if (UI_carrusselBH == null)
                    UI_carrusselBH= go_menu.GetComponent<IUIMultipleMenu>();

                if(value)
                    UI_carrusselBH.SetFirstButtonSelected();
            }
        }
    }
}