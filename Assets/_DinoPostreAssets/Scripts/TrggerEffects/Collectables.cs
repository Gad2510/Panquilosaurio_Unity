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
        private IngredientDef.Sample enm_Type;
        private Rigidbody selfRigid;
        private MeshRenderer mhr_selfRenderer;
        protected const float f_couldDown = 5f;

        private bool hasTaken = false;
        // Start is called before the first frame update
        private void Awake()
        {
            mhr_selfRenderer = GetComponent<MeshRenderer>();
            selfRigid = GetComponent<Rigidbody>();

        }

        private void OnBecameVisible()
        {
            gameObject.layer = LayerMask.NameToLayer("Colectables");
            hasTaken = false;
        }

        private IEnumerator TimerActiveValues()
        {
            float counter=0;
            while (counter <= f_couldDown)
            {
                counter += GameMode._Instance._TimeScale;
                yield return null;
            }
            Unspawn();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.CompareTag("Player") && !hasTaken)
            {
                hasTaken = true;
                RegisterCount();
                Unspawn();
            }

        }

        private void Unspawn()
        {
            mhr_selfRenderer.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Ignore");
            LevelManager._Instance._RewardManager.RegisterUnspawn(selfRigid, enm_Type);
        }

        private void RegisterCount()
        {
            //Call when the player get the collectable in time
            StopAllCoroutines();

            Events.RecordEvent ev = new Events.RecordEvent(0, "Envio de ingredientes", 10 + (int)enm_Type);
            GameMode.OnRecordEvent(ev);
        }
    }
}