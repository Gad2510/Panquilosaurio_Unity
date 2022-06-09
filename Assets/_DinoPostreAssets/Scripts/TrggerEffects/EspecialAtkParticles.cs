using UnityEngine;

namespace Dinopostres.TriggerEffects
{
    public class EspecialAtkParticles : AttackObject
    {
        [SerializeField]
        private float f_MaxDistance;
        [SerializeField]
        private AnimationCurve ac_AnimationCurve;

        private float f_counter;

        protected override void Awake()
        {
            base.Awake();
            //par_Particles = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            transform.localScale = Vector3.one* (f_MaxDistance * f_counter);

            f_counter =Mathf.Clamp01(f_counter+Time.deltaTime);
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.one;
            f_counter = 0;
        }
    }
}
