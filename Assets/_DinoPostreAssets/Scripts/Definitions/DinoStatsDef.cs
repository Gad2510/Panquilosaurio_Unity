using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class DinoStatsDef
    {

        private const int MAX_LEVEL = 100;
        public enum Stats
        {
            HP,PESO,CONFITE,COBERTURA,SABOR,TEXTURA,none
        }

        [SerializeField]
        DinoDef.DinoChar enm_Dino;

        [SerializeField]
        private float initHP;
        [SerializeField]
        private float endHp;
        [SerializeField]
        private float initPeso;
        [SerializeField]
        private float endPeso;
        [SerializeField]
        private float initConfite;
        [SerializeField]
        private float endConfite;
        [SerializeField]
        private float initCobertura;
        [SerializeField]
        private float endCobertura;
        [SerializeField]
        private float initSabor;
        [SerializeField]
        private float endSabor;
        [SerializeField]
        private float initTextura;
        [SerializeField]
        private float endTextura;

        public DinoDef.DinoChar _Dino { get => enm_Dino; }

        public float CalculateCurrentValue(Stats _stat, int _level)
        {
            float init;
            float end;
            GetStats(_stat, out init, out end);
            return init + Mathf.Floor(((end - init) / MAX_LEVEL) * (_level));
        }

        public void GetStats (Stats _stat, out float _initV, out float _endV)
        {
            switch (_stat)
            {
                case Stats.HP:
                    _initV = initHP;
                    _endV = endHp;
                    break;
                case Stats.PESO:
                    _initV = initPeso;
                    _endV = endPeso;
                    break;
                case Stats.COBERTURA:
                    _initV = initCobertura;
                    _endV = endCobertura;
                    break;
                case Stats.CONFITE:
                    _initV = initConfite;
                    _endV = endConfite;
                    break;
                case Stats.SABOR:
                    _initV = initSabor;
                    _endV = endSabor;
                    break;
                case Stats.TEXTURA:
                    _initV = initTextura;
                    _endV = endTextura;
                    break;
                default:
                    _initV = -1;
                    _endV = -1;
                    break;
            }
        }
    }
}
