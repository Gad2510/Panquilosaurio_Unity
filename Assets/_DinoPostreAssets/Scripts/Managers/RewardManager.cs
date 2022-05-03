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

        public void SpawnRewards(Vector3 _pos, IngredientDef.Sample _type)
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