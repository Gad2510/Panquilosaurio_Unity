using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Definitions;

namespace Dinopostres.Managers
{
    public class RewardManager : MonoBehaviour
    {
        private Dictionary<IngredientDef.Sample ,Queue<Rigidbody>> inactiveObjects;
        // Start is called before the first frame update
        void Awake()
        {
            inactiveObjects = new Dictionary<IngredientDef.Sample, Queue<Rigidbody>>();
        }

        public void SpawnRewards(Vector3 _pos, IngredientDef.Sample _type, float amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Rigidbody go;
                if (inactiveObjects.ContainsKey(_type) && inactiveObjects[_type].Count > 0)
                {
                    //ActiveObject
                    go=inactiveObjects[_type].Dequeue();
                }
                else
                {
                    //Create object
                    Object rewardTemplate = Ingredients.Instance().GetIngredientPrefab(_type);

                    go = (Instantiate(rewardTemplate, _pos, Quaternion.identity) as GameObject).GetComponent<Rigidbody>();
                }

                float angle = Random.Range(0f, 360f);
                go.gameObject.GetComponent<MeshRenderer>().enabled = true;
                go.transform.position = _pos;
                go.velocity = (new Vector3(Mathf.Sin(angle), 0.5f, Mathf.Cos(angle)))*3;
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