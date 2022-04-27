using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.CharacterControllers
{
    public class Boss : Enemy
    {
        protected override void Start()
        {
            base.Start();

            transform.localScale = Vector3.one * 3f;
        }
    }
}