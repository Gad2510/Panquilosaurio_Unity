using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class DinoDef
    {
        public enum DinoChar
        {
            Agujaceratops,
            Microceratus,
            Eocaecilio,
            Protarchaeopteryx,
            none
        };
        [SerializeField]
        DinoChar Dino;
        [SerializeField]
        List<LocationCount> lst_Areas;
        [SerializeField]
        string str_DefaultSkill;
        [SerializeField]
        Dinopostres.CharacterControllers.DinoPostre DP_prefeb;
        [SerializeField]
        Sprite spt_DinoImage;

        DinoChar _Dino { get => Dino; }
        string _DefaultSkill { get => str_DefaultSkill; }
        Dinopostres.CharacterControllers.DinoPostre _Prefab { get => DP_prefeb; }
        Sprite _DinoImage { get => spt_DinoImage; }

        public int HasLocation(LocationCount.Area _ckeakArea, LocationCount.Rank _checkRank)
        {
            try
            {
                LocationCount hasLocation = lst_Areas.Where((x) => (x._Area == _ckeakArea) && (x._Rank == _checkRank)).First();
                return hasLocation._Value;
            }
            catch(System.Exception e)
            {
                return -1;
            }
        } 
    }
}

