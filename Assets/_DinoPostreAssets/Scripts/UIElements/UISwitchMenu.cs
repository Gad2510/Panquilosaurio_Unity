using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Dinopostres.Managers;
using Dinopostres.CharacterControllers;
namespace Dinopostres.UIElements
{
    public class UISwitchMenu : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> lst_menus;

        private int int_index = 0;

        private IEnumerator Start()
        {
            yield return null;
            foreach(GameObject go in lst_menus)
            {
                go.SetActive(false);
            }

            lst_menus[int_index].SetActive(true);
        }

        // Start is called before the first frame update
        private void OnEnable()
        {
            GameManager._instance.InS_gameActions.DinopostreController.AttackA.performed += MoveMenu;
            GameManager._instance.InS_gameActions.DinopostreController.AttackB.performed += MoveMenu;
        }
        private void OnDisable()
        {
            GameManager._instance.InS_gameActions.DinopostreController.AttackA.performed -= MoveMenu;
            GameManager._instance.InS_gameActions.DinopostreController.AttackB.performed -= MoveMenu;
        }
        public void MoveMenu(InputAction.CallbackContext _ctx)
        {
            lst_menus[int_index].SetActive(false);

            int_index = (_ctx.action.name == "Attack A") ? int_index - 1: int_index + 1;

            if(int_index>= lst_menus.Count)
            {
                int_index = 0;
            }
            else if (int_index < 0)
            {
                int_index = lst_menus.Count - 1;
            }

            lst_menus[int_index].SetActive(true);
        }
    }
}