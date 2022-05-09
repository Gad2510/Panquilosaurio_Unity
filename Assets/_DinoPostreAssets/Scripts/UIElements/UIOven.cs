using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Definitions;
using Dinopostres.Managers;
namespace Dinopostres.UIElements
{
    public class UIOven : Dispacher
    {
        [Header("OVEN - Buttons")]
        [SerializeField]
        private Button btn_levelUP;
        [SerializeField]
        private Button btn_return;
        
        [Header("OVEN - Text")]
        [SerializeField]
        private TextMeshProUGUI txt_peso;
        [SerializeField]
        private TextMeshProUGUI txt_textura;
        [SerializeField]
        private TextMeshProUGUI txt_sabor;
        [SerializeField]
        private TextMeshProUGUI txt_cobertura;
        [SerializeField]
        private TextMeshProUGUI txt_confite;
         
        [Header("OVEN - Sliders")]
        [SerializeField]
        private Slider sl_peso;
        [SerializeField]
        private Slider sl_textura;
        [SerializeField]
        private Slider sl_sabor;
        [SerializeField]
        private Slider sl_cobertura;
        [SerializeField]
        private Slider sl_confite;

        [Header("OVEN - Ingredients")]
        [SerializeField]
        private Transform trns_ingredientsParent;
        private UIIngredientDes[] arr_reqIngredients;
        private List<IngredientCount> lst_ingredients2LUP;

        private UIDinoDes UID_currentDino;
        protected override void Start()
        {
            arr_reqIngredients = trns_ingredientsParent.GetComponentsInChildren<UIIngredientDes>();

            btn_levelUP.onClick.AddListener(() =>
            {
                if (UID_currentDino != null )
                {
                    UID_currentDino.ReturnStoreData().LevelUP();
                    UpdateSliders(UID_currentDino.ReturnStoreData());
                    UID_currentDino.QuickRelodStats();
                }
            });
            btn_return.onClick.AddListener(() => {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.oven,false); 
            });



            base.Start();
        }
        protected override void SetButtonEvent(UIDinoDes _uiDino)
        {
            _uiDino.AddBtnSelectedEvent(() => {
                UID_currentDino = _uiDino;
                DinoSaveData _save = UID_currentDino.ReturnStoreData();
                UpdateDescription(_save);
                MoveDinoUI(UID_currentDino._ParentIndex);
                int_lastIndex = UID_currentDino._ParentIndex;
                UpdateIngredients(_save.Dino, _save.Level);
            });
        }

        protected override void UpdateSliders(DinoSaveData _info)
        {
            UpdateStat(sl_healthRef, txt_descriptionPS, _info.MaxHealth, true);
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

        private void UpdateIngredients(DinoDef.DinoChar _dino , int _level)
        {
            lst_ingredients2LUP = RecipeBook._Instance().Look4Recipe(_dino).GetIngredientsNextLevel(_level);
            
            for(int i =0;i< arr_reqIngredients.Length; i++)
            {
                if (i < lst_ingredients2LUP.Count)
                {
                    arr_reqIngredients[i].UpdateDescriptions(lst_ingredients2LUP[i]._Ingredient, lst_ingredients2LUP[i]._Count);
                }
                else
                {
                    arr_reqIngredients[i].UpdateDescriptions(IngredientDef.Sample.none);
                }
            }
        }
    }
}