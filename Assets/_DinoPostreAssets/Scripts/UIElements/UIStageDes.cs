using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Managers;
using Dinopostres.Definitions;

namespace Dinopostres.UIElements
{
    public class UIStageDes : MonoBehaviour
    {
        [SerializeField]
        private Transform trns_descriptionStage;
        [SerializeField]
        private Transform trns_descriptionBuilding;

        [SerializeField]
        private GameObject go_parentImages;
        [SerializeField]
        private TextMeshProUGUI txt_stageName;
        [SerializeField]
        private TextMeshProUGUI txt_buildingName;

        private Image[] arr_dinoImages;

        private void Awake()
        {
            arr_dinoImages = go_parentImages.GetComponentsInChildren<Image>();
        }

        private void OnEnable()
        {
            if(LevelManager._Instance._GameMode != null)
            {
                bool isBuilding = ((GameModeMAP)LevelManager._Instance._GameMode)._HasTriggerBuilding;
                trns_descriptionBuilding.gameObject.SetActive(isBuilding);
                trns_descriptionStage.gameObject.SetActive(!isBuilding);
                if (isBuilding)
                {
                    UpdateDescriptionBuilding();
                }
                else
                {
                    if(!((GameModeMAP)LevelManager._Instance._GameMode)._HasChangeCheckArea)
                        UpdateDescriptionsStage();
                }
            }
        }

        private void Update()
        {
            Transform object2move = (((GameModeMAP)LevelManager._Instance._GameMode)._HasTriggerBuilding) ?trns_descriptionBuilding :trns_descriptionStage;
            object2move.position= Camera.main.WorldToScreenPoint(((GameModeMAP)LevelManager._Instance._GameMode).GetObjectPos());
        }

        private void UpdateDescriptionsStage()
        {
            GameModeMAP temp = (GameModeMAP)LevelManager._Instance._GameMode;

            txt_stageName.text= Locations.Instance().LookForLevelName(temp._Area, temp._Rank);

            List<DinoDef> dinos= EnemyStorage._Instance().GetEnemiesPerLevel(temp._Area, temp._Rank);
            dinos.AddRange(BossStageStorage._Instance().GetAllBossesInStage(temp._Area, temp._Rank));
            dinos= dinos.Distinct().ToList();

            for(int i=0; i < arr_dinoImages.Length; i++)
            {
                arr_dinoImages[i].gameObject.SetActive(i < dinos.Count);
                if (i < dinos.Count)
                {
                    arr_dinoImages[i].sprite = dinos[i]._DinoImage;
                }
            }
        }

        private void UpdateDescriptionBuilding()
        {
            txt_buildingName.text = ((GameModeMAP)LevelManager._Instance._GameMode)._BuildingName;
        }
    }
}