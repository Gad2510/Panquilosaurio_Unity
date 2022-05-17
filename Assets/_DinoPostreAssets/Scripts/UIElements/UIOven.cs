using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Definitions;
using Dinopostres.Managers;
using UnityEngine.Events;

namespace Dinopostres.UIElements
{
    public class UIOven : Dispacher
    {
        [Header("OVEN - Buttons")]
        [SerializeField]
        private Button btn_levelUP;
        [SerializeField]
        private Button btn_return;

        [Header("Migas - Text")]
        [SerializeField]
        private Image img_dinoImg;

        [Header("Migas - Text")]
        [SerializeField]
        private TextMeshProUGUI txt_migas;

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
        public List<IngredientCount> lst_ingredients2LUP;

        private UIDinoDes UID_currentDino;
        protected override void Start()
        {
            arr_reqIngredients = trns_ingredientsParent.GetComponentsInChildren<UIIngredientDes>(true);

            btn_levelUP.onClick.AddListener(() =>
            {

            if (UID_currentDino != null && GameManager._instance.PD_gameData.CanBePurchase(lst_ingredients2LUP))
                {
                    DinoSaveData _save = UID_currentDino.ReturnStoreData();
                    GameManager._instance.PD_gameData.MakePurchase(lst_ingredients2LUP);
                    _save.LevelUP();
                    UpdateSliders(_save);
                    UID_currentDino.QuickRelodStats();
                    UpdateDescription(_save);
                    UpdateIngredients(_save.Dino, _save.Level);
                    Debug.Log($"Dino levelUp {_save.Level}");
                    txt_migas.text = GameManager._instance.PD_gameData._Migas.ToString("00000");
                }
            });
            btn_return.onClick.AddListener(() => {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.oven,false); 
            });

            txt_migas.text = GameManager._instance.PD_gameData._Migas.ToString("00000");

            base.Start();
        }
        protected override void SetButtonEvent(UIDescriptions<DinoSaveData> _uiDino)
        {
            UIDinoDes _uiDinoDes = (UIDinoDes)_uiDino;
            _uiDinoDes.AddBtnSelectedEvent(() => {
                UID_currentDino = _uiDinoDes;
                DinoSaveData _save = UID_currentDino.ReturnStoreData();
                UpdateDescription(_save);
                MoveDinoUI(UID_currentDino._ParentIndex);
                int_lastIndex = UID_currentDino._ParentIndex;
                UpdateIngredients(_save.Dino, _save.Level);
                img_dinoImg.sprite = EnemyStorage._Instance().GetDinoImage(_save.Dino);
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

            for (int i =0;i< arr_reqIngredients.Length; i++)
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

        protected override UnityAction GetDesciptionEvent(DinoSaveData _item)
        {
            return () => { };
        }
    }
}