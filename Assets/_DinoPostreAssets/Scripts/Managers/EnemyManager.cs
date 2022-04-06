using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dinopostres.CharacterControllers;

namespace Dinopostres.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        private static List<Enemy> lst_EnemyInLevel= new List<Enemy>();

        public static List<Enemy> CurrentEnemies { get => lst_EnemyInLevel; }
        // Start is called before the first frame update
        void Start()
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

        public static void RegisterEnemy(Enemy _enemy)
        {
            lst_EnemyInLevel.Add(_enemy);
        }

        public static void RemoveEnemy(Enemy _enemy)
        {
            lst_EnemyInLevel.Remove(_enemy);
        }
    }
}
