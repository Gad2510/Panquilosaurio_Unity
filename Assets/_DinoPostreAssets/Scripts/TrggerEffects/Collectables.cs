using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Dinopostres.TriggerEffects
{
    public class Collectables : MonoBehaviour
    {
        VisualEffect vfx_Collectable;
        VFXEventAttribute vfxA_EventAtribute;
        // Start is called before the first frame update
        void Awake()
        {
            vfx_Collectable = GetComponent<VisualEffect>();

            vfxA_EventAtribute = vfx_Collectable.CreateVFXEventAttribute();

            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}