using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Events;

namespace Dinopostres.CharacterControllers
{
    public abstract class Controller : MonoBehaviour
    {

        protected DinoPostre DP_current;
        protected Rigidbody selfRigid;
        protected bool isInvincible;
        private WaitForSeconds w4s_InvincibleColddown= new WaitForSeconds(2f);

        protected virtual void Awake()
        {
            isInvincible = false;

            selfRigid = GetComponent<Rigidbody>();
            DP_current = GetComponentInChildren<DinoPostre>();
            gameObject.tag = "Controller";
            if (DP_current)
            {
                //index 0= cantidad , 1= posicion
                Managers.EnemyManager._OnDamage += ExecuteAction;
            }
        }

        protected virtual void OnDestroy()
        {
            if (DP_current)
            {
                Managers.EnemyManager._OnDamage -= ExecuteAction;
            }

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            Movement();
        }

        protected abstract void Movement();

        public void ExecuteAction(ActionEvent ev)
        {
            if (ev.ID != transform.GetInstanceID())
                return;

            switch (ev._Action)
            {
                case Events.ActionEvent.GameActions.HIT:
                    if (!isInvincible)
                    {
                        StartCoroutine(InvinsibleCouldown());
                        DP_current.GetDamage((float)ev.GetParameterByIndex(0));
                    }
                    break;
                default:
                    Debug.LogWarning("Action not found");
                    break;
            }
        }

        private IEnumerator InvinsibleCouldown()
        {
            isInvincible = true;
            yield return w4s_InvincibleColddown;
            isInvincible = false;
        }
    }
}
