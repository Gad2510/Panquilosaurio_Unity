using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspecialAtkParticles : AttackObject
{
    private List<int> lst_enmiesID = new List<int>();

    private void OnEnable()
    {
        lst_enmiesID.Clear();
    }

    private void OnParticleTrigger()
    {
        
    }
}
