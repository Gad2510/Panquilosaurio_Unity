using System.Collections;
using System.Collections.Generic;
using Dinopostres.Events;
using Dinopostres.Definitions;
using UnityEngine;

namespace Dinopostres.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        

        public delegate void GameActions(ActionEvent ev);
        public static GameActions _OnDamage;
        public static GameActions _OnDead;
        
        private List<GameObject> lst_EnemyInLevel= new List<GameObject>();

        public List<GameObject> CurrentEnemies { get => lst_EnemyInLevel; }
        // Start is called before the first frame update
        void Awake()
        {
        }

        private void OnLevelWasLoaded(int level)
        {
            lst_EnemyInLevel.Clear();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RegisterEnemy(GameObject _enemy)
        {
            lst_EnemyInLevel.Add(_enemy);
        }

        public void RemoveEnemy(GameObject _enemy)
        {
            lst_EnemyInLevel.Remove(_enemy);
        }
    }
}
