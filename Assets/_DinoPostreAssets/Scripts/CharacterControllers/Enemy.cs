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

        bool isPlayerNear;
        float f_PlayerDistance;
        NavMeshAgent nav_MeshAgent;


        protected override void Awake()
        {
            base.Awake();
            EnemyManager.RegisterEnemy(this);

            isPlayerNear = false;
            nav_MeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            Percepcion();
            base.Update();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EnemyManager.RemoveEnemy(this);
        }

        protected override void Movement()
        {
            if (!isPlayerNear)
                return;

            transform.LookAt(Player.PL_Instance.transform);

            if (f_PlayerDistance > 2)
            {
                nav_MeshAgent.destination = Player.PL_Instance.transform.position;
            }
            else
            {
                base.selfRigid.velocity = Vector3.zero;
            }
                

            
        }
        //To check how near the player is to the enemy
        private void Percepcion()
        {
            f_PlayerDistance = Vector3.Distance(Player.PL_Instance.transform.position, transform.position);
            isPlayerNear = f_PlayerDistance < 5f;
        }
    }
}
