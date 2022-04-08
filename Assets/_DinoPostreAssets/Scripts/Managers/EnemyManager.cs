using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        private static List<GameObject> lst_EnemyInLevel= new List<GameObject>();

        public static List<GameObject> CurrentEnemies { get => lst_EnemyInLevel; }
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

        public static void RegisterEnemy(GameObject _enemy)
        {
            lst_EnemyInLevel.Add(_enemy);
        }

        public static void RemoveEnemy(GameObject _enemy)
        {
            lst_EnemyInLevel.Remove(_enemy);
        }
    }
}
