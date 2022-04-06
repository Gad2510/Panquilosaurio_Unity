using UnityEngine;
using Dinopostres.TriggerEffects;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class SkillDef
    {
        [SerializeField]
        string str_SkillName;
        [SerializeField]
        bool isPhysical;
        [SerializeField]
        AttackObject AO_EmitterOrCollider;
        [SerializeField]
        float f_duration;

        WaitForSeconds w4s_pauseAction;

        public string _SkillName { get => str_SkillName; }
        public bool _isPhysical { get => isPhysical; }
        public AttackObject _EmitterOrCollider { get => AO_EmitterOrCollider; }

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