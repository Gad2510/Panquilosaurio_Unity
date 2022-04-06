using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class DinoStatsDef
    {
        public enum Stats
        {
            HP,PESO,CONFITE,COBERTURA,SABOR,TEXTURA,none
        }

        [SerializeField]
        DinoDef.DinoChar enm_Dino;

        [SerializeField]
        float initHP;
        [SerializeField]
        float endHp;
        [SerializeField]
        float initPeso;
        [SerializeField]
        float endPeso;
        [SerializeField]
        float initConfite;
        [SerializeField]
        float endConfite;
        [SerializeField]
        float initCobertura;
        [SerializeField]
        float endCobertura;
        [SerializeField]
        float initSabor;
        [SerializeField]
        float endSabor;
        [SerializeField]
        float initTextura;
        [SerializeField]
        float endTextura;

        public DinoDef.DinoChar _Dino { get => enm_Dino; }
        
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
