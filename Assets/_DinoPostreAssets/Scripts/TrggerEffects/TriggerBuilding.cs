using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Dinopostres.Managers;
namespace Dinopostres.TriggerEffects
{
    public class TriggerBuilding : MonoBehaviour
    {
        [SerializeField]
        private GameMode.MenuDef enm_openMenu;
        private bool onTrigger;

        private void Awake()
        {
            GameManager._instance.InS_gameActions.DinopostreController.AttackA.performed += OpenMenu;
        }
        private void OnDestroy()
        {
            GameManager._instance.InS_gameActions.DinopostreController.AttackA.performed -= OpenMenu;
        }

        private void OnTriggerEnter(Collider other)
        {
            onTrigger = other.transform.root.CompareTag("Player");
        }

        private void OpenMenu(InputAction.CallbackContext _ctx)
        {
            if (onTrigger)
            {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.oven, true);
            }
        }
    }
}