using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Dinopostres.Definitions;


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
        string[] lst_skills = new string[4];
        int int_Level;
        float f_Hp,f_Textura,f_Sabor, f_Covertura,f_COnfite,f_Peso;

        // Start is called before the first frame update
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        public void SetSkills()
        {

        }

        public void ExecuteAttack(int index)
        {
            string skill = lst_skills[index];

            try
            {
                SkillDef def= lst_SkillList.Where((x) => x._SkillName == skill).First();
                Debug.Log("Prepare Attack");
                StartCoroutine(Attack(def));

            }
            catch (System.Exception e)
            {
                Debug.Log($"Error al llamar ataque {e.Message}");
            }
        }

        private IEnumerator Attack(SkillDef _skill)
        {
            Debug.Log($"Skill: {_skill._SkillName} | Collider: {((GameObject)_skill._EmitterOrCollider).name}");

            GameObject collider = (GameObject)_skill._EmitterOrCollider;

            collider.SetActive(true);

            yield return _skill._ColdDown();

            collider.SetActive(false);



        }
    }
}

