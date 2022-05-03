using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Dinopostres.Events;

namespace Dinopostres.CharacterControllers
{
    public class Boss : Enemy
    {
        private bool isAttackObject;
        private Dictionary<Attacks,WaitForSeconds>  _attackList;
        private WaitForSeconds w4s_TackleCoulddown = new WaitForSeconds(4f);
        protected override void Start()
        {
            base.Start();

            _attackList = new Dictionary<Attacks, WaitForSeconds>();
            _attackList.Add(base._defaultAttack, w4s_AttackPreparation);
            _attackList.Add(SuperTackle, new WaitForSeconds(1));
            _attackList.Add(ShockWave, null);
            _attackList.Add(ShockWave, new WaitForSeconds(3));

            isStaticHealthBar = true;
            transform.localScale = Vector3.one * 3f;
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
            base.GetHIT();
        }

        private void SuperTackle()
        {
            base.selfRigid.AddForce(transform.forward);
            StartCoroutine(TackleCouldown(w4s_TackleCoulddown));
        }

        private IEnumerator TackleCouldown(WaitForSeconds _time)
        {
            isAttackObject = true;
            yield return _time;
            isAttackObject = false;
        }

        private void ShockWave()
        {
            RaycastHit hit;
            if(Physics.SphereCast(transform.position,10f,transform.forward,out hit, 10f,LayerMask.NameToLayer("Player"))){
                Vector3 dir = (hit.transform.position - transform.position).normalized;
                hit.rigidbody.AddForce(dir * 10f);
            }
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