using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Definitions;

namespace Dinopostres.UIElements
{
    public class UIDinoDes : UIDescriptions<DinoSaveData>, ISelectHandler
    {
        [SerializeField]
        private TextMeshProUGUI txt_power;
        [SerializeField]
        private Slider sl_healthBar;
        [SerializeField]
        private Image img_dinoImg;
        public override void InitStats(DinoSaveData _dinoData, UnityEngine.Events.UnityAction _ev)
        {
            base.InitStats(_dinoData, _ev);

            if (txt_name != null)
                txt_name.text = storeData._Dino.ToString();
        }
        public override void QuickRelodStats()
        {
            
            if (txt_power != null)
                txt_power.text = string.Format("CP {0}", storeData._Power.ToString());

            if (img_dinoImg != null)
                img_dinoImg.sprite = EnemyStorage._Instance().GetDinoImage(storeData._Dino);

            if (sl_healthBar != null)
            {
                float maxHealth = storeData._MaxHealth;
                sl_healthBar.value = storeData._CurrentHealth / maxHealth;
            }
        }
    }
}