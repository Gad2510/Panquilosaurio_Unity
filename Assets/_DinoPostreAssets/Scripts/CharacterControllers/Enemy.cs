using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Dinopostres.Definitions;
using Dinopostres.Managers;

namespace Dinopostres.CharacterControllers
{
    public class Enemy : Controller
    {
        private const float f_Distance2Player = 3f;
        private const float f_Distance2Attack = 0.5f;

        bool isPlayerNear;
        bool isAttacking;
        public float f_PlayerDistance;
        public float f_AttackDistance;
        NavMeshAgent nav_MeshAgent;
        WaitForSeconds w4s_AttackPreparation= new WaitForSeconds(2);
        Coroutine ctn_Attack;
        Vector3 v3_Origin;

        protected override void Start()
        {
            base.Start();
            LevelManager._Instance._EnemyManager.RegisterEnemy(this.gameObject);

            isPlayerNear = false;
            isAttacking = false;
            v3_Origin = transform.position;
            nav_MeshAgent = gameObject.AddComponent<NavMeshAgent>();

            nav_MeshAgent.speed = (base.DP_current._Peso / 100);
        }
        // Update is called once per frame
        protected override void Update()
        {
            if (!isAttacking && !DP_current.IsAttacking)
            {
                Percepcion();
                base.Update();
            }

            MoveHealBar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LevelManager._Instance._EnemyManager.RemoveEnemy(this.gameObject);
        }

        protected override void Movement()
        {
            if (!isPlayerNear)
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
                ctn_Attack = StartCoroutine(prepareAttack());
            }

            transform.LookAt(Player.PL_Instance.transform);

        }
        //To check how near the player is to the enemy
        private void Percepcion()
        {
            f_PlayerDistance = Vector3.Distance(Player.PL_Instance.transform.position, v3_Origin);
            f_AttackDistance = Vector3.Distance(Player.PL_Instance.transform.position, transform.position);

            isPlayerNear = f_PlayerDistance < f_Distance2Player;
        }

        private IEnumerator prepareAttack()
        {
            yield return w4s_AttackPreparation;

            DP_current.ExecuteAttack(4);
            isAttacking = false;
        }
    }
}
