using UnityEngine;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class SkillDef
    {
        [SerializeField]
        private string str_SkillName;
        [SerializeField]
        private bool isPhysical;
        [SerializeField]
        private GameObject AO_EmitterOrCollider;
        [SerializeField]
        private float f_duration;

        private WaitForSeconds w4s_pauseAction;

        public string _SkillName { get => str_SkillName; }
        public bool _isPhysical { get => isPhysical; }
        public GameObject _EmitterOrCollider { get => AO_EmitterOrCollider; }

        public WaitForSeconds _ColdDown()
        {
            if (w4s_pauseAction == null)
            {
                w4s_pauseAction = new WaitForSeconds(f_duration);
            }

            return w4s_pauseAction;
        }
    }
}