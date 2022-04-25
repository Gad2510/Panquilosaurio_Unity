using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Managers;
using Dinopostres.Definitions;

namespace Dinopostres.TriggerEffects
{
    public class Collectables : MonoBehaviour
    {
        [SerializeField]
        IngredientDef.Sample enm_Type;
        Rigidbody selfRigid;
        MeshRenderer mhr_selfRenderer;

        WaitForSeconds w4s_CouldDown = new WaitForSeconds(5f);
        // Start is called before the first frame update
        void Awake()
        {
            mhr_selfRenderer = GetComponent<MeshRenderer>();
            selfRigid = GetComponent<Rigidbody>();
        }

        void OnBecameVisible()
        {
            gameObject.layer = LayerMask.NameToLayer("Colectables");
            StartCoroutine(TimerActiveValues());
        }

        private void OnBecameInvisible()
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore");
        }

        private IEnumerator TimerActiveValues()
        {
            yield return w4s_CouldDown;
            Unspawn();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.CompareTag("Player"))
            {
                RegisterCount();
                Unspawn();
            }
        }

        private void Unspawn()
        {
            mhr_selfRenderer.enabled = false;
            LevelManager._Instance._RewardManager.RegisterUnspawn(selfRigid, enm_Type);
        }

        private void RegisterCount()
        {
            //Call when the player get the collectable in time
            StopAllCoroutines();
        }
    }
}