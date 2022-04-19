using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Definitions;

namespace Dinopostres.Managers
{
    public class RewardManager : MonoBehaviour
    {
        private Object rewardTemplate;

        private Queue<Rigidbody> rewardObjects;
        private Queue<Rigidbody> inactiveObjects;
        // Start is called before the first frame update
        void Awake()
        {
            rewardObjects = new Queue<Rigidbody>();
            inactiveObjects = new Queue<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SpawnRewards(Vector3 pos)
        {

            if (inactiveObjects.Count > 0)
            {
                //ActiveObject


            }
            else
            {
                //Create object
                Rigidbody go = Instantiate(rewardTemplate, pos, Quaternion.identity) as Rigidbody;
            }
        }
    }
}