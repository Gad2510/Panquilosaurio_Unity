using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Dinopostres.Definitions;
using Dinopostres.Events;

namespace Dinopostres.CharacterControllers
{
    public class DinoPostre : MonoBehaviour
    {

        [SerializeField]
        int int_ID;
        [SerializeField]
        DinoDef.DinoChar enm_Dino;
        [SerializeField]
        List<SkillDef> lst_SkillList;
        [SerializeField]
        string[] lst_skills = new string[5];

        int int_Level;
        const int MAX_LEVEL= 100;
        bool isAttacking=false;
        bool isPlayer;
        public float f_TestHP;

        Dictionary<DinoStatsDef.Stats, float> dic_Stats = new Dictionary<DinoStatsDef.Stats, float>
        { 
            {DinoStatsDef.Stats.HP,0 }, 
            {DinoStatsDef.Stats.PESO,0 }, 
            {DinoStatsDef.Stats.CONFITE,0 }, 
            {DinoStatsDef.Stats.COBERTURA,0 }, 
            {DinoStatsDef.Stats.SABOR,0 }, 
            {DinoStatsDef.Stats.TEXTURA,0 }, 
        };

        public bool IsPlayer { set => isPlayer = value; }
        public bool IsAttacking { get => isAttacking; }
        public float _Peso { get => dic_Stats[DinoStatsDef.Stats.PESO]; }

        // Start is called before the first frame update
        void Awake()
        {
            int_Level = 1;
            InitStats();
            lst_skills[4]=EnemyStorage._Instance().GetDeafultEnemyAttack(enm_Dino);
            f_TestHP = dic_Stats[DinoStatsDef.Stats.HP];
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            
        }

        public void OnCollisionEnter(Collision collision)
        {

        }

        private void OnTriggerEnter(Collider other)
        {

        }

        private void InitStats()
        {
            DinoStatsDef stats = DinoSpecsDef.Instance().LookForStats(enm_Dino);

            for(int i=0;i<(int)DinoStatsDef.Stats.none; i++)
            {
                float value = 0;
                SetStatByLevel(stats, (DinoStatsDef.Stats)i,ref value);
                dic_Stats[(DinoStatsDef.Stats)i] = value;

               // Debug.Log($"STAT {(DinoStatsDef.Stats)i}  | Valor {value}");
            }
        }

        private void SetStatByLevel(DinoStatsDef _statDef,DinoStatsDef.Stats _statName, ref float _stat)
        {
            float init, end;
            _statDef.GetStats(_statName,out init,out end);
            _stat = init + Mathf.Floor(((end - init) / MAX_LEVEL) *( int_Level-1));
        }

        public void SetSkills()
        {

        }

        public void ExecuteAttack(int index)
        {
            if (isAttacking)
                return;

            string skill = lst_skills[index];

            try
            {
                SkillDef def= lst_SkillList.Where((x) => x._SkillName == skill).First();
                StartCoroutine(Attack(def));

            }
            catch (System.Exception e)
            {
                Debug.Log($"Error al llamar ataque {e.Message}");
            }
        }

        private IEnumerator Attack(SkillDef _skill)
        {

            TriggerEffects.AttackObject collider = _skill._EmitterOrCollider.GetComponent<TriggerEffects.AttackObject>();

            collider._Damage = (_skill._isPhysical) ? dic_Stats[DinoStatsDef.Stats.SABOR]: dic_Stats[DinoStatsDef.Stats.TEXTURA];

            collider.gameObject.SetActive(true);
            isAttacking = true;

            yield return _skill._ColdDown();

            collider.gameObject.SetActive(false);
            isAttacking = false;
        }

        public void GetDamage(float damage)
        {
            dic_Stats[DinoStatsDef.Stats.HP] -= damage;
            f_TestHP = dic_Stats[DinoStatsDef.Stats.HP];
            if (dic_Stats[DinoStatsDef.Stats.HP] <= 0)
            {
                print("I died");
            }
        }
    }
}

