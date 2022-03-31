using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        List<Enemy> lst_EnemyInLevel= new List<Enemy>();

        public List<Enemy> CurrentEnemies { get => lst_EnemyInLevel; }
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
    }
}
