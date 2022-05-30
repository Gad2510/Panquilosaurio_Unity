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
        Object DP_prefeb;
        [SerializeField]
        Sprite spt_DinoImage;
        [SerializeField]
        List<IngredientCount> lst_Rewards;

        public DinoChar _Dino { get => Dino; }
        public string _DefaultSkill { get => str_DefaultSkill; }
        public Object _Prefab { get => DP_prefeb; }
        public Sprite _DinoImage { get => spt_DinoImage; }
        public List<IngredientCount> _Rewards { get => lst_Rewards; }

        public int HasLocation(LocationCount.Area _ckeakArea, LocationCount.Rank _checkRank)
        {
            try
            {
                LocationCount hasLocation = lst_Areas.Where((x) => (x._Area == _ckeakArea) && (x._Rank == _checkRank)).First();
                return (int)hasLocation._Value;
            }
            catch
            {
                return -1;
            }
        }
    }
}

