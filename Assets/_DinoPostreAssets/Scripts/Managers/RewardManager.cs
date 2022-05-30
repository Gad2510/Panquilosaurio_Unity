using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Definitions;

namespace Dinopostres.Managers
{
    public class RewardManager : MonoBehaviour
    {
        private Dictionary<IngredientDef.Sample ,Queue<Rigidbody>> inactiveObjects;
        Vector3 offset= new Vector3(0, 0.3f, 0);
        // Start is called before the first frame update
        void Awake()
        {
            inactiveObjects = new Dictionary<IngredientDef.Sample, Queue<Rigidbody>>();
        }

        public void ClearReferences()
        {
            inactiveObjects.Clear();
        }

        public void SpawnRewards(Vector3 _pos, IngredientDef.Sample _type, bool isRandom=false)
        {
            Rigidbody go;
            if (inactiveObjects.ContainsKey(_type) && inactiveObjects[_type].Count > 0)
            {
                //ActiveObject
                go = inactiveObjects[_type].Dequeue();
                go.transform.position = _pos + offset;
            }
            else
            {
                //Create object
                Object rewardTemplate = Ingredients.Instance().GetIngredientPrefab(_type);

                go = (Instantiate(rewardTemplate, _pos +offset, Quaternion.identity) as GameObject).GetComponent<Rigidbody>();
            }
            go.gameObject.GetComponent<MeshRenderer>().enabled = true;

            if (isRandom)
            {
                float angle = Random.Range(0f, 360f)*Mathf.Deg2Rad;
                float impulse = Random.Range(0.5f, 2f);
                Vector3 direction = new Vector3(Mathf.Sin(angle), 1f, Mathf.Cos(angle)) * impulse;
                go.AddForce(direction, ForceMode.VelocityChange);
            }
        }
        public void RegisterUnspawn(Rigidbody go, IngredientDef.Sample _type)
        {
            if(!inactiveObjects.ContainsKey(_type) || inactiveObjects[_type]==null )
            {
                inactiveObjects[_type] = new Queue<Rigidbody>();
            }
            inactiveObjects[_type].Enqueue(go);
        }
    }
}