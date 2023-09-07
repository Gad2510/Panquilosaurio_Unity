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
        protected bool isInmovilize = false;

        protected const float f_invinibleCoulddown = 0.3f;
        protected WaitWhile ww_InmovilizeByLunch;
        private Vector3 v3_Offset = new Vector3(0f,50f,0f);
        public float f_Speed;
        protected bool isStaticHealthBar=false;

        protected Vector3 v3_lastVel;

        protected void Awake()
        {
            ww_InmovilizeByLunch = new WaitWhile(InmovilizeInAir);
        }

        private bool InmovilizeInAir()
        {
            bool check = selfRigid.velocity.magnitude > 0.5f;
            if (GameMode._Instance._TimeScale > 0)
            {
                v3_lastVel = selfRigid.velocity;
                selfRigid.constraints = RigidbodyConstraints.FreezePositionY & RigidbodyConstraints.FreezeRotationX & RigidbodyConstraints.FreezeRotationZ;
                selfRigid.velocity -= (selfRigid.velocity * (Time.deltaTime * 10));
            }
            else
            {
                selfRigid.constraints = RigidbodyConstraints.FreezeAll;
                selfRigid.velocity = v3_lastVel;
            }

            if (isDead)
            {
                check &= transform.localScale.magnitude > 0.05f;
            }
            return  check;
        }

        protected virtual void Start()
        {
            isInvincible = false;
            isDead = false;
            selfRigid = GetComponent<Rigidbody>();
            DP_current = GetComponentInChildren<DinoPostre>();
            gameObject.tag = "Controller";

            Managers.EnemyManager._OnDamage += ExecuteAction;

            //f_Speed = (100 / DP_current._Peso);
            
            CreateHealthBar();
        }

        protected virtual void OnDestroy()
        {
            Managers.EnemyManager._OnDamage -= ExecuteAction;

            if(sl_healthVisual!=null)
                Destroy(sl_healthVisual.gameObject);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if(!isDead && !isInmovilize)
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
                    if (DP_current == null)
                        break;

                    if (!isInvincible)
                    {
                        StartCoroutine(InvinsibleCouldown(f_invinibleCoulddown));
                        DP_current.GetDamage((float)ev.GetParameterByIndex(0));
                        UpdateHealthBar();
                        StartCoroutine(StopMovement());
                        FallBack((transform.position-(Vector3)ev.GetParameterByIndex(1)).normalized);
                        
                        
                    }
                    break;
                case ActionEvent.GameActions.REPEL:
                   
                    if (gameObject.CompareTag("Player"))
                    {
                        FallBack((transform.position-(Vector3)ev.GetParameterByIndex(0)).normalized, 300f);
                        StartCoroutine(StopMovement());
                    }
                       
                    break;
                default:
                    Debug.LogWarning("Action not found");
                    break;
            }
        }
        protected void UpdateHealthBar()
        {
            sl_healthVisual.value = DP_current.GetHeath();
        }
        protected virtual void FallBack(Vector3 _dir,float _intensity=100f)
        {
            if (sl_healthVisual.value <= 0)
            {
                GetDead();
                _intensity = 200f;
            }

            GetHIT();

            if (selfRigid != null)
            {
                selfRigid.AddForce(_dir* _intensity);
            }
        }

        protected virtual void CreateHealthBar()
        {
            Object sld = Resources.Load<Object>("Prefabs/sl_Controller");
            sl_healthVisual = (Instantiate(sld, ((GameModeINSTAGE)LevelManager._Instance._GameMode)._Status.transform) as GameObject).GetComponent<Slider>();
        }

        protected abstract void GetDead();
        protected abstract void GetHIT();
        protected IEnumerator InvinsibleCouldown(float _time)
        {
            isInvincible = true;
            float counter = 0;
            while (counter < _time)
            {
                counter += GameMode._Instance._TimeScale;
                yield return null;
            }
            yield return _time;
            isInvincible = false;
        }

        protected virtual IEnumerator StopMovement()
        {
            isInmovilize = true;
            float counter = 0;
            while (counter < f_invinibleCoulddown)
            {
                v3_lastVel = selfRigid.velocity;
                counter += GameMode._Instance._TimeScale;
                yield return null;
            }
            yield return ww_InmovilizeByLunch;

            isInmovilize = false;

        }
    }
}
