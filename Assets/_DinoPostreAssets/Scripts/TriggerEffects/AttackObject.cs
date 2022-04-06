using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Events;

namespace Dinopostres.TriggerEffects
{
    public class AttackObject : MonoBehaviour
    {
        protected string str_Compere;
        [SerializeField]
        protected float f_baseDamage;

        protected float f_damage;

        public float _Damage { set => f_damage = value; }

        protected virtual void Awake()
        {
            str_Compere = transform.root.CompareTag("Player") ? "Enemy" : "Player";
            gameObject.layer = LayerMask.NameToLayer(str_Compere);
        }
        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(str_Compere))
            {
                SendDamage(other);
            }
        }

        protected void SendDamage(Collider _col)
        {
            float damageValue = (f_damage * f_baseDamage)/50;

            ActionEvent ev = new ActionEvent(0, "Sending Damage", new List<object> { damageValue, transform.position });

            _col.GetComponent<Dinopostres.CharacterControllers.Controller>().ExecuteAction(CharacterControllers.Controller.GameActions.HIT,ev);
        }

    }
}
