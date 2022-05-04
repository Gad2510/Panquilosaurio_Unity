using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Dinopostres.Definitions
{
    [CreateAssetMenu(fileName = "Locations", menuName = "Dinopostre/Locations", order = 5)]
    public class Locations : ScriptableObject
    {
        private static Locations L_instance;

        [SerializeField]
        private List<LocationDef> lst_locationDef;

        public static Locations Instance()
        {
            if (L_instance == null)
            {
                L_instance = Resources.Load<Locations>("ScriptableObjects/Locations");
            }
            return L_instance;
        }

        public LocationDef LookForDef(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            try
            {
                return lst_locationDef.First((x) => x._Area == _area && x._Rank == _rank);
            }
            catch(System.Exception e)
            {
                Debug.LogError("Location definition not found");
                return null;
            }
        }

        public string LookForLevelName(LocationCount.Area _area, LocationCount.Rank _rank)
        {
            try
            {
                return lst_locationDef.First((x) => x._Area == _area && x._Rank == _rank)._LevelName;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Location definition not found level name");
                return "Menu";
            }
        }

    }
}