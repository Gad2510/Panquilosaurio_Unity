using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Definitions;
namespace Dinopostres.UIElements
{
    public class UILoadGameBtn : UIDescriptions<PlayerData>
    {
        [SerializeField]
        private GameObject go_Info;
        [SerializeField]
        private GameObject go_noData;

        [SerializeField]
        private Transform trns_parentImgs;
        [SerializeField]
        private TextMeshProUGUI txt_dinoAmount;
        [SerializeField]
        private TextMeshProUGUI txt_migas;

        private Image[] arr_bossImg;

        public string _Name { get => txt_name.text; }

        protected override void Awake()
        {
            base.Awake();
            arr_bossImg = trns_parentImgs.GetComponentsInChildren<Image>();
        }

        public override void QuickRelodStats()
        {
            go_Info.SetActive(storeData != null);
            go_noData.SetActive(storeData == null);
            
            if (storeData != null)
            {
                txt_migas.text = storeData._Migas.ToString();
                txt_dinoAmount.text = storeData.CalculateRegisterDinos().ToString();
                txt_name.text = storeData.ID;
                bool isDeafeted = true;
                for (int i =0;i<arr_bossImg.Length; i++)
                {
                    isDeafeted = storeData.IsBossDefeated((LocationCount.Rank)i);
                    if (isDeafeted)
                    {
                        arr_bossImg[i].sprite = BossStageStorage._Instance().GetAllBossesInStage(LocationCount.Area.volcan, (LocationCount.Rank)i)[0]._DinoImage;
                    }
                    
                }
            }
            else
            {
                txt_name.text = string.Format("Slot {0}", transform.GetSiblingIndex());
            }
        }

        
    }
}