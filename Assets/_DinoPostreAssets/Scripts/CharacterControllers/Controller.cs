using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Events;

namespace Dinopostres.CharacterControllers
{
    public abstract class Controller : MonoBehaviour
    {

        public delegate void Actions(ActionEvent ev);
        private Actions OnGetHit;

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
                OnGetHit += DP_current.GetDamage;
            }
        }

        protected virtual void OnDestroy()
        {
            if (DP_current)
            {
                OnGetHit -= DP_current.GetDamage;
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
            Debug.Log("ACTION EVENT");
            switch (ev._Action)
            {
                case Events.ActionEvent.GameActions.HIT:
                    if (!isInvincible)
                    {
                        StartCoroutine(InvinsibleCouldown());
                        OnGetHit(ev);
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
