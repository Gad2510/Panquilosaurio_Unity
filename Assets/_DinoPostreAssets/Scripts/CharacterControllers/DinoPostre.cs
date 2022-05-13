using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Dinopostres.Definitions;
using Dinopostres.Managers;
using Dinopostres.Events;

namespace Dinopostres.CharacterControllers
{
    public class DinoPostre : MonoBehaviour
    {
        [SerializeField]
        DinoDef.DinoChar enm_Dino;
        [SerializeField]
        List<SkillDef> lst_SkillList;
        [SerializeField]
        string[] lst_skills = new string[5];

        int int_Level=1;
        bool isAttacking=false;
        bool isPlayer;
        float f_maxHealth;
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
        public float CurrentHealth { get => dic_Stats[DinoStatsDef.Stats.HP]; set => dic_Stats[DinoStatsDef.Stats.HP] = value; }
        public float _Peso { get => dic_Stats[DinoStatsDef.Stats.PESO]; }
        public DinoDef.DinoChar _DinoChar { get => enm_Dino; }
        // Start is called before the first frame update
        void Awake()
        {
            lst_skills[4]=EnemyStorage._Instance().GetDeafultEnemyAttack(enm_Dino);
        }

        public void InitStats(int _level, DinoSaveData _def=null)
        {
            int_Level = _level;
            DinoStatsDef stats = DinoSpecsDef.Instance().LookForStats(enm_Dino);

            for(int i=0;i<(int)DinoStatsDef.Stats.none; i++)
            {
                float value = 0;
                SetStatByLevel(stats, (DinoStatsDef.Stats)i,ref value);
                dic_Stats[(DinoStatsDef.Stats)i] = value;

                //Debug.Log($"STAT {(DinoStatsDef.Stats)i}  | Valor {value}  - {isPlayer}");
            }
            f_maxHealth = dic_Stats[DinoStatsDef.Stats.HP];
            if(_def!=null)
                dic_Stats[DinoStatsDef.Stats.HP] = _def.CurrentHealth;
            

            f_TestHP = dic_Stats[DinoStatsDef.Stats.HP];
        }

        private void SetStatByLevel(DinoStatsDef _statDef,DinoStatsDef.Stats _statName, ref float _stat)
        {
            _stat = _statDef.CalculateCurrentValue(_statName,int_Level);
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
                Debug.Log($"Error al llamar de {enm_Dino} ataque {e.Message}");
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
                
                if (!isPlayer)
                {
                    Destroy(this.gameObject, 2f);
                }
            }
        }

        public float GetHeath()
        {
            return dic_Stats[DinoStatsDef.Stats.HP] / f_maxHealth;
        }
        public void GetRewards()
        {
            DinoDef dino = EnemyStorage._Instance().Look4DinoDef(enm_Dino);
            int i=0;
            int max = dino._Rewards.Sum((x) => x._Count);
            int ran = Random.Range(0, max);
            for (int count = 0; i < dino._Rewards.Count() && count<ran; i++)
            {
                count += dino._Rewards[0]._Count;
            }
            Debug.Log($"Index = {i-1} max = {max} random= {ran}");
            LevelManager._Instance._RewardManager.SpawnRewards(transform.position, dino._Rewards[i-1]._Ingredient);

        }
    }
}

