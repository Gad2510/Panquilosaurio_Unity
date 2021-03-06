using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dinopostres.Managers;
namespace Dinopostres.CharacterControllers
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private int int_spwnCount = 5;
        [SerializeField]
        private static int startLevel=1;

        [SerializeField]
        private bool isBossStage;

        private Object obj_companionRef;
        private WaitForSeconds w4s_timerboss = new WaitForSeconds(10f);
        private Collider coll_selfCol;
        private float f_colliderRadius;
        public static int StartLevel { set => startLevel = value; }

        private void Awake()
        {
            f_colliderRadius = GetComponent<SphereCollider>().radius/2;
            coll_selfCol = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            coll_selfCol.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.CompareTag("Player"))
            {
                if (!isBossStage)
                {
                    GetEnemiesTospawn();
                }
                else
                {
                    GetBossAndAllies();
                }
                coll_selfCol.enabled = false;
                LevelManager._Instance._Spawners = gameObject;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
        }

        private void GetEnemiesTospawn()
        {
            if (int_spwnCount <= 0)
                return;

            Dictionary<float, Object> enemies = new Dictionary<float, Object>();
            for (int i = 0; i < int_spwnCount; i++)
            {
                float rarety = 0;
                Object pref = LevelManager._Instance.GetEnemiesFromLevel(out rarety);

                if (!enemies.ContainsKey(rarety))
                {
                    enemies[rarety] = pref;
                }
                else
                {
                    enemies[rarety + 1] = pref;
                }
            }

            StartCoroutine(OrderEnemies(enemies));
        }
        private void GetBossAndAllies()
        {
            Object bossRef= LevelManager._Instance.GetBossFromLevel(out obj_companionRef);

            SpawnMinions(bossRef, 1, 0,0f,true);

            InvokeRepeating(nameof(SpawnCompanions),0,40f);
        }

        private void SpawnCompanions()
        {
            for (int i = 1; i < 5; i++)
            {
                SpawnMinions(obj_companionRef, 4, i,45f);
            }
        }

        private IEnumerator OrderEnemies(Dictionary<float, Object> enemies)
        {
            List<Object> ens = enemies.OrderBy((x) => x.Key).Select((x) => x.Value).ToList();
            
            for (int i = 0; i < ens.Count(); i++)
            {
                SpawnMinions(ens[i], ens.Count(), i);
                yield return null;
            }
            gameObject.SetActive(false);
        }

        private void SpawnMinions(Object pref,int count, int pos, float angleOffset = 0, bool isBoss=false)
        {
            Vector3 ofsset = Vector3.zero;
            if (pos != 0)
            {
                float angle = (((360 / (count)) * pos)+angleOffset)*Mathf.Deg2Rad;
                ofsset.x = Mathf.Sin(angle)*f_colliderRadius;
                ofsset.z = Mathf.Cos(angle)*f_colliderRadius;
            }
            GameObject go = Instantiate(pref, transform.position + ofsset, Quaternion.identity) as GameObject;
            Enemy en;
            if (!isBoss)
            {
                en = go.AddComponent<Enemy>();

                
            }
            else
            {
                en = go.AddComponent<Boss>();
            }

            if (isBossStage)
                en._HasLimitView = true;

            en.SetEnemyLevel(startLevel);
            Rigidbody rd = go.AddComponent<Rigidbody>();
            rd.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}