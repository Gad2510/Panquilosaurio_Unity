using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dinopostres.Managers;
using Dinopostres.Definitions;
namespace Dinopostres.TriggerEffects {
    public class RecordTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent ua_trigerEvent;
        [SerializeField]
        private UnlockDef UD_unlockCondition;
        private void Start()
        {
            if (GameManager._instance._GameData.CheckForUnlock(UD_unlockCondition))
            {
                ua_trigerEvent.Invoke();
            }
        }
    }
}