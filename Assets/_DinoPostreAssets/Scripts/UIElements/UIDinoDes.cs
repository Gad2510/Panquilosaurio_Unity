using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.CharacterControllers;
using Dinopostres.Definitions;

namespace Dinopostres.UIElements
{
    public class UIDinoDes : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI txt_name;
        [SerializeField]
        TextMeshProUGUI txt_power;
        [SerializeField]
        Slider sl_healthBar;

        Button btn_ChangeDino;

        private void Awake()
        {
            btn_ChangeDino = GetComponent<Button>();

        }

        public void InitStats(DinoSaveData _dinoData)
        {
            txt_name.text = _dinoData.Dino.ToString();
            txt_power.text = DinoSpecsDef.Instance().CalculatePower(_dinoData.Dino,_dinoData.Level).ToString();

            float maxHealth = DinoSpecsDef.Instance().LookForStats(_dinoData.Dino).CalculateCurrentValue(DinoStatsDef.Stats.HP, _dinoData.Level);
            sl_healthBar.value = _dinoData.CurrentHealth /maxHealth;

            AddBtnEvent(() => { Player.PL_Instance.SwitchDino(_dinoData); });
        }

        public void AddBtnEvent(UnityEngine.Events.UnityAction _ev)
        {
            btn_ChangeDino.onClick.AddListener(_ev);
        }

        public void SetButtonAsSelected()
        {
            btn_ChangeDino.Select();
        }
    }
}