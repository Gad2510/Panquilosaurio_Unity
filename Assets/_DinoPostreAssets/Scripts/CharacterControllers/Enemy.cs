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
        public delegate void Attacks();
        protected Attacks _defaultAttack;

        private const float f_Distance2Player = 3f;
        private const float f_Distance2Attack = 1.1f;

        private bool isPlayerNear;
        private bool isAttacking;
        private bool hasLimitView= true;
        public float f_PlayerDistance;
        public float f_AttackDistance;
        private NavMeshAgent nav_MeshAgent;
        protected WaitForSeconds w4s_AttackPreparation = new WaitForSeconds(2);
        protected Coroutine ctn_Attack;
        private Vector3 v3_Origin;

        private float f_Speed;
        public bool _HasLimitView { set => hasLimitView = false; }

        protected override void Start()
        {
            base.Start();
            LevelManager._Instance._EnemyManager.RegisterEnemy(this.gameObject);

            isPlayerNear = false;
            isAttacking = false;
            v3_Origin = transform.position;
            nav_MeshAgent = gameObject.AddComponent<NavMeshAgent>();

            f_Speed = (100 / base.DP_current._Peso);
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
            nav_MeshAgent.speed = f_Speed * GameManager._instance._TimeScale;
            MoveHealBar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LevelManager._Instance._EnemyManager.RemoveEnemy(this.gameObject);
        }

        protected override void Movement()
        {
            if (!isPlayerNear && hasLimitView)
            {
                nav_MeshAgent.destination = v3_Origin;
                transform.LookAt(v3_Origin+transform.forward);
                return;
            }

            if (f_AttackDistance > f_Distance2Attack) //Movement
            {
                nav_MeshAgent.destination = Player.PL_Instance.transform.position;
            }
            else 
            {
                isAttacking = true;
                nav_MeshAgent.velocity = Vector3.zero;
                SetAttack();
            }

            transform.LookAt(Player.PL_Instance.transform);

        }

        protected virtual void SetAttack()
        {
            ctn_Attack = StartCoroutine(prepareAttack(_defaultAttack, w4s_AttackPreparation));
        }

        public void SetEnemyLevel(int _level)
        {
            if (DP_current == null)
                DP_current = GetComponentInChildren<DinoPostre>();

            DP_current.InitStats(_level);

        }
        //To check how near the player is to the enemy
        protected void Percepcion()
        {
            f_PlayerDistance = Vector3.Distance(Player.PL_Instance.transform.position, v3_Origin);
            f_AttackDistance = Vector3.Distance(Player.PL_Instance.transform.position, transform.position);

            isPlayerNear = f_PlayerDistance < f_Distance2Player;
        }

        protected IEnumerator prepareAttack(Attacks _skill, WaitForSeconds _couldown, bool inVulnerable=false)
        {
            base.isInvincible = inVulnerable;
            yield return _couldown;
            base.isInvincible = false;
            _skill.Invoke();
            isAttacking = false;
        }

        private void DinoDefaultAttack() 
        {
            DP_current.ExecuteAttack(4);
        }

        protected override void GetHIT()
        {
            StopCoroutine(ctn_Attack);
            isAttacking = false;
            if (isDead)
            {
                RecordEvent ev = new RecordEvent(6, "Enemy defeted", 3000 + (int)DP_current._DinoChar);
                GameManager._instance.OnRecordEvent(ev);
                StartCoroutine(SpawnReward());
            }
        }

        protected override void GetDead()
        {
            base.isDead = true;
        }
        private IEnumerator SpawnReward()
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore");
            yield return null;
            yield return new WaitWhile(() => base.selfRigid.velocity.magnitude > 0.1f);

            DP_current.GetRewards();
            Destroy(this.gameObject);
        }
    }
}
