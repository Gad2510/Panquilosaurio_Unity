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

        bool isBossStage;
        WaitForSeconds w4s_timerboss = new WaitForSeconds(10f);

        private void Awake()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            print(other.transform.root.tag);
            if (other.transform.root.CompareTag("Player"))
            {
                GetEnemiesTospawn();
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {

        }

        void GetEnemiesTospawn()
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

            OrderEnemies(enemies);
        }

        private void OrderEnemies(Dictionary<float, Object> enemies)
        {
            List<Object> ens = enemies.OrderBy((x) => x.Key).Select((x) => x.Value).ToList();
            Vector3 ofsset = Vector3.zero;
            for (int i = 0; i < ens.Count(); i++)
            {
                GameObject go= Instantiate(ens[i], transform.position + ofsset, Quaternion.identity) as GameObject;
                go.AddComponent<Enemy>();
                Rigidbody rd= go.AddComponent<Rigidbody>();
                rd.constraints = RigidbodyConstraints.FreezePositionY & RigidbodyConstraints.FreezeRotationX & RigidbodyConstraints.FreezeRotationZ;
                ofsset.x = Mathf.Sin(360 / (enemies.Count() - 1));
                ofsset.y = Mathf.Cos(360 / (enemies.Count() - 1));
            }
        }
    }
}