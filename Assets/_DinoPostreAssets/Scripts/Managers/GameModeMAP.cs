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
        public LocationCount.Area _Area { get => enm_area; }
        public LocationCount.Rank _Rank { get => enm_rank; }
        public bool hasChangeCheck;
        protected override void InitMenus()
        {
            base.InitMenus();
            base.dic_menus.Add(MenuDef.decriptions, LoadGameMenu(dic_menuRef[MenuDef.decriptions]));
        }

        public Vector3 GetObjectPos()
        {
            return trns_descriptionFollow.position + v3_Offset;
        }

        public void UpdateStageUIDescription(LocationCount.Area _area, LocationCount.Rank _rank, Transform _follow)
        {
            hasChangeCheck = enm_area != _area && enm_rank != _rank;
            enm_area = _area;
            enm_rank = _rank;
            trns_descriptionFollow = _follow;
        }

        public void ShowDescriptions(bool _state)
        {
            base.dic_menus[MenuDef.decriptions].SetActive(_state);
        }
    }
}