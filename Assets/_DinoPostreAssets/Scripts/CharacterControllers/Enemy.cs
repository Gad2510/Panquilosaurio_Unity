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
        private const float f_Distance2Attack = 1f;

        bool isPlayerNear;
        bool isAttacking;
        public float f_PlayerDistance;
        public float f_AttackDistance;
        private NavMeshAgent nav_MeshAgent;
        private WaitForSeconds w4s_AttackPreparation = new WaitForSeconds(2);
        private Coroutine ctn_Attack;
        private Vector3 v3_Origin;

        protected override void Start()
        {
            base.Start();
            LevelManager._Instance._EnemyManager.RegisterEnemy(this.gameObject);

            isPlayerNear = false;
            isAttacking = false;
            v3_Origin = transform.position;
            nav_MeshAgent = gameObject.AddComponent<NavMeshAgent>();

            Debug.Log($"Dino {base.DP_current._Peso}");
            nav_MeshAgent.speed = (base.DP_current._Peso / 100);
            nav_MeshAgent.radius = 0.1f;
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

        public void SetEnemyLevel(int _level)
        {
            if (DP_current == null)
                DP_current = GetComponentInChildren<DinoPostre>();

            DP_current.InitStats(_level);

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

        protected override void GetHIT()
        {
            StopCoroutine(ctn_Attack);
        }
    }
}
