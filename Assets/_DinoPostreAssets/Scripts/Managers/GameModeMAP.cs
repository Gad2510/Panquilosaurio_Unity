using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.Definitions;
namespace Dinopostres.Managers
{
    public class GameModeMAP : GameModeINSTAGE
    {
        [SerializeField]
        private LocationCount.Area enm_area = LocationCount.Area.none;
        [SerializeField]
        private LocationCount.Rank enm_rank = LocationCount.Rank.none;
        private Transform trns_descriptionFollow;
        private Vector3 v3_Offset= new Vector3(0f,1f,0f);
        private string str_BuildingName;
        private bool hasChangeCheckArea = false;
        private bool hasTriggerBuilding = false;
        public LocationCount.Area _Area { get => enm_area; }
        public LocationCount.Rank _Rank { get => enm_rank; }
        public bool _HasChangeCheckArea { get => hasChangeCheckArea; }
        public bool _HasTriggerBuilding { get => hasTriggerBuilding; }
        public string _BuildingName { 
            get { return str_BuildingName; } 
            set { str_BuildingName = value;
                hasTriggerBuilding = !string.IsNullOrEmpty(value);
            } 
        }

        protected override void Awake()
        {
            base.Awake();
        }
        protected override void InitMenus()
        {
            base.InitMenus();
            base.dic_menus.Add(MenuDef.decriptions, LoadGameMenu(dic_menuRef[MenuDef.decriptions]));
            base.dic_menus.Add(MenuDef.oven, LoadGameMenu(dic_menuRef[MenuDef.oven]));
        }

        public Vector3 GetObjectPos()
        {
            return trns_descriptionFollow.position + v3_Offset;
        }

        public void UpdateStageUIDescription(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            hasChangeCheckArea = enm_area == _area && enm_rank == _rank;
            enm_area = _area;
            enm_rank = _rank;
        }

        public void SetDescripcionFollow(Transform _follow)
        {
            trns_descriptionFollow = _follow;
        }
    }
}