using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Managers;
namespace Dinopostres.TriggerEffects
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField]//Si se necesita desbloquear o es la entrada a una estncia
        private bool isLock;
        [SerializeField]//Marca si estas dentro de una estancia
        private bool isStage;
        [SerializeField]
        private bool isBoss;

        [SerializeField]
        private string str_destiny;

        private void Start()
        {
            if(!isStage && isLock)
            {
                LevelManager._Instance.RegisterTeleporter(gameObject, this.GetInstanceID(), isBoss);
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isLock)
            {
                if (other.transform.root.CompareTag("Player"))
                {
                    ChangeLocation(other.transform.root);
                }
            }
        }

        private void ChangeLocation(Transform _player)
        {
            if (isStage)
            {
                //Change Scene
            }
            else
            {
                
                Vector3 tel=LevelManager._Instance.SelectNextTeleporter(this.GetInstanceID()).transform.position;
                tel.y = _player.position.y;
                _player.position = tel;
            }
        }
    }
}