using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Definitions;

namespace Dinopostres.TriggerEffects
{
    public class Collectables : MonoBehaviour
    {
        IngredientDef.Sample enm_Type;

        MeshFilter mfl_selfMeshFilter;
        MeshRenderer mhr_selfRenderer;

        WaitForSeconds w4s_CouldDown = new WaitForSeconds(5f);
        // Start is called before the first frame update
        void Awake()
        {
            mfl_selfMeshFilter = GetComponent<MeshFilter>();
            mhr_selfRenderer = GetComponent<MeshRenderer>();
        }

        public void InitValues(IngredientDef.Sample _type)
        {
            Mesh meshRef;
            Material matRef;
            Ingredients.Instance().GetIngredientElements(_type, out meshRef, out matRef);

            mfl_selfMeshFilter.mesh = meshRef;
            mhr_selfRenderer.material = matRef;
            enm_Type = _type;
        }

        private IEnumerator TimerActiveValues()
        {
            yield return w4s_CouldDown;
        }

        private void OnTriggerEnter(Collider other)
        {

        }
    }
}