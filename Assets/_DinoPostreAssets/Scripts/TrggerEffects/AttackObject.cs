using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Events;

namespace Dinopostres.TriggerEffects
{
    public class AttackObject : MonoBehaviour
    {
        [SerializeField]
        protected float f_baseDamage;

        protected float f_damage;

        public float _Damage { set => f_damage = value; }

        protected virtual void Awake()
        {
            string str_Compere = transform.root.CompareTag("Player") ? "Enemy" : "Player";
            gameObject.layer = LayerMask.NameToLayer(str_Compere);
        }
        protected void OnTriggerEnter(Collider other)
        {
            /*CharacterControllers.Controller ctrl = other.GetComponentInParent<CharacterControllers.Controller>();
            if (ctrl!=null)
            {
                SendDamage(ctrl);
            }*/

            if (other.transform.root.CompareTag("Controller"))
            {
                SendDamage(other);
            }
        }

        /*protected void SendDamage(CharacterControllers.Controller _ctrl)
        {
            float damageValue = (f_damage * f_baseDamage)/50;

            ActionEvent ev = new ActionEvent(0, "Sending Damage", new List<object> { damageValue, transform.position });

            _ctrl.ExecuteAction(CharacterControllers.Controller.GameActions.HIT,ev);
        }*/

        protected void SendDamage(Collider _col)
        {
            float damageValue = (f_damage * f_baseDamage) / 50;

            ActionEvent ev = new ActionEvent(0, "Sending Damage",ActionEvent.GameActions.HIT ,new List<object> { damageValue, transform.position });
            _col.transform.root.SendMessage("ExecuteAction", ev);
        }

    }
}
