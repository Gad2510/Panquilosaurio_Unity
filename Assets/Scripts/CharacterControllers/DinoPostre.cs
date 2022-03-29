using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.CharacterControllers
{
    public class DinoPostre : MonoBehaviour
    {
        public delegate void Skill(string name, object element);
        public Skill[] callbackSkills = new Skill[4];

        private Dictionary<string, object> dic_skillSet= new Dictionary<string, object> { 
            {"TestA",1},  
            {"TestB","Especial"},  
        };

        // Start is called before the first frame update
        void Awake()
        {
            callbackSkills[0] = TestA;
            callbackSkills[1] = TestB;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExecuteAttack(int index)
        {
            string[] skills= new string[dic_skillSet.Count];
            dic_skillSet.Keys.CopyTo(skills,0);

            callbackSkills[index].Invoke(skills[index], dic_skillSet[skills[index]]);
        }

        private void TestA(string msg, object num)
        {
            Debug.Log($"Test {msg} es {num}");
        }

        private void TestB(string msg, object str)
        {
            Debug.Log($"Test {msg} es {str}");
        }
    }
}

