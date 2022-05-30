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
        [SerializeField]
        private string str_buildingName;
        public bool onTrigger;

        private void Start()
        {
            GameManager._instance.InS_gameActions.DinopostreController.Interaction.performed += OpenMenu;
        }
        private void OnDestroy()
        {
            GameManager._instance.InS_gameActions.DinopostreController.Interaction.performed -= OpenMenu;
        }

        private void OnTriggerEnter(Collider other)
        {
            onTrigger = other.transform.root.CompareTag("Player") && !other.CompareTag("Attack");
            if (onTrigger && LevelManager._Instance._GameMode._LastMenu != enm_openMenu)
            {
                ((GameModeMAP)LevelManager._Instance._GameMode)._BuildingName = str_buildingName;
                ((GameModeMAP)LevelManager._Instance._GameMode).SetDescripcionFollow(transform);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.decriptions, true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            onTrigger = other.transform.root.CompareTag("Player") && !other.CompareTag("Attack");
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.root.CompareTag("Player") && !other.CompareTag("Attack"))
            {
                ((GameModeMAP)LevelManager._Instance._GameMode)._BuildingName = "";
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.decriptions, false);
                onTrigger = false;
            }
        }

        private void OpenMenu(InputAction.CallbackContext _ctx)
        {
            int mask = (int)GameMode.MenuDef.decriptions;
            if (onTrigger && ((int)LevelManager._Instance._GameMode._LastMenu & mask) >=1)
            {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(enm_openMenu, true);
            }
        }
    }
}