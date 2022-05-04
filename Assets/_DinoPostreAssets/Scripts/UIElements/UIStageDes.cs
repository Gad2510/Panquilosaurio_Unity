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
        private Transform go_description;
        [SerializeField]
        private GameObject go_parentImages;
        [SerializeField]
        private TextMeshProUGUI txt_stageName;

        private Image[] arr_dinoImages;

        private void Awake()
        {
            arr_dinoImages = go_parentImages.GetComponentsInChildren<Image>();
        }

        private void OnEnable()
        {
            if (LevelManager._Instance._GameMode!=null && !((GameModeMAP)LevelManager._Instance._GameMode).hasChangeCheck)
            {
                UpdateDescriptions();
            }
        }

        private void Update()
        {
            go_description.position= Camera.main.WorldToScreenPoint(((GameModeMAP)LevelManager._Instance._GameMode).GetObjectPos());
        }

        private void UpdateDescriptions()
        {
            GameModeMAP temp = (GameModeMAP)LevelManager._Instance._GameMode;
            Debug.Log(temp.gameObject);
            Debug.Log($"{temp._Area} | {temp._Rank}");
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
    }
}