using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Definitions;
namespace Dinopostres.UIElements
{
    public class UIOven : Dispacher
    {
        [Header("OVEN - Text")]
        [SerializeField]
        TextMeshProUGUI txt_peso;
        [SerializeField]
        TextMeshProUGUI txt_textura;
        [SerializeField]
        TextMeshProUGUI txt_sabor;
        [SerializeField]
        TextMeshProUGUI txt_cobertura;
        [SerializeField]
        TextMeshProUGUI txt_confite;

        [Header("OVEN - Sliders")]
        [SerializeField]
        Slider sl_peso;
        [SerializeField]
        Slider sl_textura;
        [SerializeField]
        Slider sl_sabor;
        [SerializeField]
        Slider sl_cobertura;
        [SerializeField]
        Slider sl_confite;

        protected override void SetButtonEvent(UIDinoDes _uiDino)
        {
            _uiDino.AddBtnClicEvent(() => gameObject.SetActive(false));
            _uiDino.AddBtnSelectedEvent(() => {
                UpdateDescription(_uiDino.ReturnStoreData());
                MoveDinoUI(_uiDino._ParentIndex);
                int_lastIndex = _uiDino._ParentIndex;

            });
        }

        protected override void UpdateSliders(DinoSaveData _info)
        {
            UpdateStat(sl_healthRef, txt_DescriptionPS, _info.MaxHealth, true);
            UpdateStat(sl_peso, txt_peso,DinoSpecsDef.Instance().LookForStats(_info.Dino).CalculateCurrentValue(DinoStatsDef.Stats.PESO,_info.Level));
            UpdateStat(sl_textura, txt_textura, DinoSpecsDef.Instance().LookForStats(_info.Dino).CalculateCurrentValue(DinoStatsDef.Stats.TEXTURA,_info.Level));
            UpdateStat(sl_sabor, txt_sabor, DinoSpecsDef.Instance().LookForStats(_info.Dino).CalculateCurrentValue(DinoStatsDef.Stats.SABOR,_info.Level));
            UpdateStat(sl_cobertura, txt_cobertura, DinoSpecsDef.Instance().LookForStats(_info.Dino).CalculateCurrentValue(DinoStatsDef.Stats.COBERTURA,_info.Level));
            UpdateStat(sl_confite, txt_confite, DinoSpecsDef.Instance().LookForStats(_info.Dino).CalculateCurrentValue(DinoStatsDef.Stats.CONFITE,_info.Level));
        }

        private void UpdateStat(Slider _slporcentage, TextMeshProUGUI _textRef, float _value, bool _isLife=false)
        {
            float stat = _value;
            _textRef.text = $"{stat}";
            float divide = (_isLife) ? 1000: 500;
            _slporcentage.value = stat / divide;
        }
    }
}