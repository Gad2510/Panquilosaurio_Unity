using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Events;
using UnityEngine.UI;

namespace Dinopostres.CharacterControllers
{
    public abstract class Controller : MonoBehaviour
    {
        protected Slider sl_healthVisual;
        protected DinoPostre DP_current;
        protected Rigidbody selfRigid;
        protected bool isInvincible;
        private WaitForSeconds w4s_InvincibleColddown= new WaitForSeconds(2f);
        private Vector3 v3_Offset = new Vector3(0f,50f,0f);

        protected virtual void Start()
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

            Object sld = Resources.Load<Object>("Prefabs/sl_Controller");
            sl_healthVisual = (Instantiate(sld, GameObject.FindGameObjectWithTag("UI_Controllers").transform) as GameObject).GetComponent<Slider>();
        }

        protected virtual void OnDestroy()
        {
            if (DP_current)
            {
                Managers.EnemyManager._OnDamage -= ExecuteAction;
            }

            if(sl_healthVisual!=null)
                Destroy(sl_healthVisual.gameObject);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            Movement();
        }

        protected abstract void Movement();

        protected void MoveHealBar()
        {
            sl_healthVisual.transform.position = Camera.main.WorldToScreenPoint(transform.position)+ v3_Offset;
        }

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

                        if (selfRigid != null)
                            selfRigid.velocity = (transform.position-(Vector3)ev.GetParameterByIndex(1)).normalized * 5f;

                        sl_healthVisual.value = DP_current.GetHeath();
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
