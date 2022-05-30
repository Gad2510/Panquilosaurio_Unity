using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Dinopostres.Events;
using Dinopostres.Managers;
namespace Dinopostres.CharacterControllers
{
    public class Boss : Enemy
    {
        private bool isAttackObject;
        private Dictionary<Attacks,float>  _attackList;
        private WaitWhile ww_TackleCoulddow;

        private Collider[] arr_hitCOlliders;
        private int int_mask= 1<<6;
        protected new const float f_Distance2Attack = 2.1f;
        private const float f_deadTime= 4f;
        protected override void Start()
        {
            base.Start();

            _attackList = new Dictionary<Attacks, float>();
            _attackList.Add(base._defaultAttack, f_attackPreparation);
            _attackList.Add(SuperTackle, 1f);
            _attackList.Add(ShockWave, 0f);
            _attackList.Add(ChargeShockWave, 3f);

            isStaticHealthBar = true;
            transform.localScale = Vector3.one * 3f;
            selfRigid.mass = 100f;


            ww_TackleCoulddow = new WaitWhile(() => selfRigid.velocity.magnitude > 0.1f);
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void SetAttack()
        {
            int choseAttack=Random.Range(0, _attackList.Count);
            ctn_Attack = StartCoroutine(prepareAttack(_attackList.ElementAt(choseAttack).Key, _attackList.ElementAt(choseAttack).Value, choseAttack==3));
        }

        protected override void CreateHealthBar()
        {
            Object sld = Resources.Load<Object>("Prefabs/grp_bossHealthBar");
            base.sl_healthVisual = (Instantiate(sld, GameObject.FindGameObjectWithTag("UI_Controllers").transform) as GameObject).GetComponentInChildren<Slider>();
        }

        protected override void GetHIT()
        {
            if (isDead)
            {
                RecordEvent ev = new RecordEvent(6, "Enemy defeted", 3000 + (int)DP_current._DinoChar);
                GameManager._instance.OnRecordEvent(ev);
                StartCoroutine(SpawnReward());
            }
        }

        private void SuperTackle()
        {
            StartCoroutine(TackleCouldown());
        }

        private IEnumerator TackleCouldown()
        {
            nav_MeshAgent.isStopped = true;
            selfRigid.constraints |= RigidbodyConstraints.FreezeRotationY;
            isAttacking = true;

            LeanTween.value(0f, 0.1f, 0.4f).setOnUpdate((x)=> { 
                nav_MeshAgent.baseOffset = x;
                isPlayerNear = x<0.025f;
                selfRigid.velocity = -(transform.forward*0.4f);
            });

            isAttackObject = true;
            float counter = 0;
            while (counter < f_attackPreparation)
            {
                counter += GameManager._TimeScale;
                yield return null;
            }
            counter = 0;
            base.selfRigid.AddForce(transform.forward * 5f, ForceMode.VelocityChange);
            LeanTween.value(0.1f, 0f, 0.2f).setOnUpdate((x) => nav_MeshAgent.baseOffset = x);
            while (counter < f_invinibleCoulddown)
            {
                counter += GameManager._TimeScale;
                yield return null;
            }
            nav_MeshAgent.enabled = false;
            yield return ww_TackleCoulddow;
            nav_MeshAgent.enabled = true;
            selfRigid.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            nav_MeshAgent.isStopped = false;
            isAttacking = false;
            nav_MeshAgent.baseOffset = 0f;
            isAttackObject = false;
        }
        protected override void FallBack(Vector3 _dir, float _intensity = 100)
        {
            if (sl_healthVisual.value <= 0)
            {
                GetDead();
            }

            GetHIT();
        }
        protected override IEnumerator SpawnReward()
        {
            gameObject.layer = 1 >> 9;
            nav_MeshAgent.isStopped = true;
            float counter = 0;
            while (counter < f_deadTime)
            {
                transform.localScale = (Vector3.one * 3) * (1-(counter/f_deadTime));
                for (int i = 0; i < 5 && (counter / f_deadTime) % 0.2f < 0.001f; i++)
                {

                    DP_current.GetRewards(true);
                }

                counter += GameManager._TimeScale;
                yield return null;
            }
            LevelManager._Instance.SpawnExitTeleporter();
            Destroy(this.gameObject);
        }
        private void ChargeShockWave()
        {
            ShockWave();
        }
        private void ShockWave()
        {
            Debug.Log("ShockWave");
            arr_hitCOlliders = Physics.OverlapSphere(transform.position, 5f, int_mask);
            try
            {
                if(arr_hitCOlliders.Any((x) => x.transform.root.CompareTag("Player")))
                {
                    Collider playerCol = arr_hitCOlliders.First((x) => x.transform.root.CompareTag("Player"));
                    ActionEvent ev = new ActionEvent(playerCol.transform.root.GetInstanceID(), "Sending Damage", ActionEvent.GameActions.REPEL, new List<object> {transform.position });
                    Managers.EnemyManager._OnDamage(ev);
                }
            }
            catch
            {

            }

        }

        protected override void Percepcion()
        {
            if(!nav_MeshAgent.isStopped)
                base.Percepcion();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isAttackObject)
            {
                if (collision.transform.root.CompareTag("Player"))
                {
                    float damageValue = 10f;

                    ActionEvent ev = new ActionEvent(collision.transform.root.GetInstanceID(), "Sending Damage", ActionEvent.GameActions.HIT, new List<object> { damageValue, transform.position });
                    Managers.EnemyManager._OnDamage(ev);
                }
            }
        }
    }
}