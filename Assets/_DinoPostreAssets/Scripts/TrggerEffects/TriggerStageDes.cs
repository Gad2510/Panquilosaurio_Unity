using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Managers;

namespace Dinopostres.TriggerEffects
{
    public class TriggerStageDes : MonoBehaviour
    {
        private Teleporter tp_parentTeleporter;

        // Start is called before the first frame update
        void Awake()
        {
            tp_parentTeleporter = GetComponentInParent<Teleporter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.CompareTag("Player"))
            {
                CallStageDes();
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.decriptions,true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.root.CompareTag("Player"))
            {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.decriptions, false);
            }
        }

        private void CallStageDes()
        {
            ((GameModeMAP)LevelManager._Instance._GameMode).UpdateStageUIDescription(tp_parentTeleporter._Area, tp_parentTeleporter._Rank, transform);
        }
    }
}