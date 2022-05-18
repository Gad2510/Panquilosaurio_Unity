using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Definitions;
using Dinopostres.Managers;
using Dinopostres.Events;
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
        private LocationCount.Area enm_area= LocationCount.Area.none;
        [SerializeField]
        private LocationCount.Rank enm_rank = LocationCount.Rank.none;

        public LocationCount.Area _Area { get => enm_area; }
        public LocationCount.Rank _Rank { get => enm_rank; }
        private void Awake()
        {
            if(!isStage && isLock)
            {
                Debug.Log("Teleporter register");
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
                LevelManager._Instance.LoadStage(enm_area, enm_rank);
                string lvlName = Locations.Instance().LookForLevelName(enm_area, enm_rank);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.decriptions, false);
                if (lvlName.Equals("Criadero"))
                {

                    RecordEvent ev = new RecordEvent(7, "Call from Teleporter", 40000 + ((int)LevelManager._Instance._Area * 100) + (int)LevelManager._Instance._Rank);
                    GameManager._instance.OnRecordEvent(ev);
                }

                LevelManager._Instance.LoadLevel(lvlName);
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