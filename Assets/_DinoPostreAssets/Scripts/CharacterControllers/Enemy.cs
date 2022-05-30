using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Dinopostres.Definitions;
using Dinopostres.Events;
using Dinopostres.Managers;

namespace Dinopostres.CharacterControllers
{
    public class Enemy : Controller
    {
        public enum EnemyState
        {
            move,attack,dead,hit,none
        }
        public EnemyState _state = EnemyState.none;
        public delegate void Attacks();
        protected Attacks _defaultAttack;

        private const float f_Distance2Player = 5f;
        protected const float f_Distance2Attack = 1.1f;

        protected bool isPlayerNear;
        protected bool isAttacking;
        private bool hasLimitView= true;
        public float f_PlayerDistance;
        public float f_AttackDistance;
        protected NavMeshAgent nav_MeshAgent;
        protected const float f_attackPreparation = 2f;
        protected Coroutine ctn_Attack;
        private Vector3 v3_Origin;

       
        public bool _HasLimitView { set => hasLimitView = false; }

        protected override void Start()
        {
            base.Start();
            LevelManager._Instance._EnemyManager.RegisterEnemy(this.gameObject);
            isPlayerNear = false;
            isAttacking = false;
            v3_Origin = transform.position;
            nav_MeshAgent = gameObject.AddComponent<NavMeshAgent>();

            nav_MeshAgent.speed = f_Speed;
            nav_MeshAgent.radius = 0.1f;
            

            _defaultAttack = DinoDefaultAttack;
        }
        // Update is called once per frame
        protected override void Update()
        {
            if (!isAttacking && !DP_current.IsAttacking)
            {
                Percepcion();
                base.Update();
            }
            if(nav_MeshAgent!=null)
                nav_MeshAgent.speed = f_Speed * GameManager._Time;

            MoveHealBar();

            if (isPlayerNear)
            {
                transform.LookAt(Player.PL_Instance.transform);
            }
        }

        protected override void Movement()
        {
            if (!isPlayerNear && hasLimitView)
            {
                nav_MeshAgent.destination = v3_Origin;
                transform.LookAt(v3_Origin+transform.forward);
                DP_current.SetAnimationVariable("f_Speed", 0f);
                return;
            }
            _state = EnemyState.move;

            selfRigid.velocity -= selfRigid.velocity * (GameManager._TimeScale*4f);
            if (selfRigid.velocity.magnitude < 0)
                selfRigid.velocity = Vector3.zero;

            DP_current.SetAnimationVariable("f_Speed", 0.5f);

            if (f_AttackDistance > f_Distance2Attack) //Movement
            {
                nav_MeshAgent.destination = Player.PL_Instance.transform.position;
            }
            else 
            {
                isAttacking = true;
                if(!base.isInmovilize)
                    nav_MeshAgent.velocity = Vector3.zero;

                SetAttack();
            }

            
        }

        protected virtual void SetAttack()
        {
            _state = EnemyState.attack;
            ctn_Attack = StartCoroutine(prepareAttack(_defaultAttack, f_attackPreparation));
        }

        public void SetEnemyLevel(int _level)
        {
            if (DP_current == null)
                DP_current = GetComponentInChildren<DinoPostre>();

            DP_current.InitStats(_level);

        }
        //To check how near the player is to the enemy
        protected virtual void Percepcion()
        {
            f_PlayerDistance = Vector3.Distance(Player.PL_Instance.transform.position, v3_Origin);
            f_AttackDistance = Vector3.Distance(Player.PL_Instance.transform.position, transform.position);

            isPlayerNear = f_PlayerDistance < f_Distance2Player;
        }

        protected IEnumerator prepareAttack(Attacks _skill, float _couldown, bool inVulnerable=false)
        {
            base.isInvincible = inVulnerable;
            float counter = 0;
            while(counter< _couldown)
            {
                counter += GameManager._TimeScale;
                yield return null;
            }
            base.isInvincible = false;
            isAttacking = false;
            _skill.Invoke();
        }

        private void DinoDefaultAttack() 
        {
            DP_current.ExecuteAttack(4);
        }

        protected override void GetHIT()
        {
            if (ctn_Attack != null)
            {
                StopCoroutine(ctn_Attack);
                base.isInvincible = false;
                isAttacking = false;
            }
            if (isDead)
            {
                RecordEvent ev = new RecordEvent(6, "Enemy defeted", 3000 + (int)DP_current._DinoChar);
                GameManager._instance.OnRecordEvent(ev);
                StartCoroutine(SpawnReward());
            }
        }

        protected override void GetDead()
        {
            _state = EnemyState.dead;
            StopAllCoroutines();
            base.isDead = true;
            LevelManager._Instance._EnemyManager.RemoveEnemy(this.gameObject);
            LeanTween.cancelAll();
        }
        protected virtual IEnumerator SpawnReward()
        {
            gameObject.layer = 1>>9;
            LeanTween.scale(gameObject, Vector3.one * 0.1f, 1f);
            if (nav_MeshAgent != null)
            {
                nav_MeshAgent.isStopped = true;
                nav_MeshAgent.baseOffset = 0.001f;
            }
            float counter = 0;
            while (counter < f_invinibleCoulddown)
            {
                v3_lastVel = selfRigid.velocity;
                counter += GameManager._TimeScale;
                yield return null;
            }
            if (nav_MeshAgent != null)
                nav_MeshAgent.baseOffset = 0f;

            Destroy(nav_MeshAgent,0.2f);
            yield return ww_InmovilizeByLunch;
            DP_current.GetRewards();
            Destroy(this.gameObject);
        }

        protected override IEnumerator StopMovement()
        {
            if (nav_MeshAgent == null)
            {
                yield break;
            }

            _state = EnemyState.hit;

            nav_MeshAgent.isStopped = true;
            nav_MeshAgent.baseOffset = 0.001f;
            isInmovilize = true;
            
            float counter = 0;
            while (counter < f_invinibleCoulddown)
            {
                v3_lastVel = selfRigid.velocity;
                counter += GameManager._TimeScale;
                yield return null;
            }
            nav_MeshAgent.baseOffset = 0f;
            yield return ww_InmovilizeByLunch;
            isInmovilize = false;
            nav_MeshAgent.isStopped = false;
        }
    }
}
