using UnityEngine;

namespace Dinopostres.TriggerEffects
{
    public class AttackObject : MonoBehaviour
    {
        protected string str_Compere;
        protected float f_damage;

        public float _Damage { set => f_damage = value; }
        protected void Awake()
        {
            str_Compere = transform.root.CompareTag("Player") ? "Enemy" : "Player";
            gameObject.layer = LayerMask.NameToLayer(str_Compere);
        }
    }
}
