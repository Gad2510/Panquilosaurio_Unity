using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Events;
using UnityEngine.UI;
using Dinopostres.Managers;

namespace Dinopostres.CharacterControllers
{
    public abstract class Controller : MonoBehaviour
    {
        protected Slider sl_healthVisual;
        protected DinoPostre DP_current;
        protected Rigidbody selfRigid;
        protected bool isInvincible;
        protected bool isDead;

        private WaitForSeconds w4s_InvincibleColddown= new WaitForSeconds(0.2f);
        private Vector3 v3_Offset = new Vector3(0f,50f,0f);

        protected bool isStaticHealthBar=false;

        protected virtual void Start()
        {
            isInvincible = false;
            isDead = false;

            selfRigid = GetComponent<Rigidbody>();
            DP_current = GetComponentInChildren<DinoPostre>();
            gameObject.tag = "Controller";
            if (DP_current)
            {
                //index 0= cantidad , 1= posicion
                Managers.EnemyManager._OnDamage += ExecuteAction;
            }

            CreateHealthBar();
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
            if(!isDead)
                Movement();
        }

        protected abstract void Movement();

        protected void MoveHealBar()
        {
            if (isStaticHealthBar)
                return;

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
                        StartCoroutine(InvinsibleCouldown(w4s_InvincibleColddown));
                        DP_current.GetDamage((float)ev.GetParameterByIndex(0));
                        sl_healthVisual.value = DP_current.GetHeath();

                        if (selfRigid != null)
                        {
                            float intensity = 1;
                            if (sl_healthVisual.value <= 0)
                            {
                                GetDead();
                                intensity = 5;
                            }

                            selfRigid.velocity = (transform.position - (Vector3)ev.GetParameterByIndex(1)).normalized * intensity;
                        }
                        GetHIT();
                    }
                    break;
                default:
                    Debug.LogWarning("Action not found");
                    break;
            }
        }

        protected virtual void CreateHealthBar()
        {
            Object sld = Resources.Load<Object>("Prefabs/sl_Controller");
            sl_healthVisual = (Instantiate(sld, ((GameModeINSTAGE)LevelManager._Instance._GameMode)._Status.transform) as GameObject).GetComponent<Slider>();
        }

        protected abstract void GetDead();
        protected abstract void GetHIT();
        protected IEnumerator InvinsibleCouldown(WaitForSeconds _time)
        {
            isInvincible = true;
            yield return _time;
            isInvincible = false;
        }
    }
}
