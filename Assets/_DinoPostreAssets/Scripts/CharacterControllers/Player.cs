using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dinopostres.CharacterControllers
{
    public class Player : Controller
    {
        public static Player PL_Instance;

        private DinoPostreAction InS_gameActions;
        
        

        private Vector2 direction;
        private float lastAngle;
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();

            //Stat Instance
            PL_Instance = this;

            //Stats Dino
            if (base.DP_current)
            {
                base.DP_current.IsPlayer = true;
            }

            InS_gameActions = new DinoPostreAction();
            //Bind movement action
            InS_gameActions.DinopostreController.Movement.performed += ctx => direction = ctx.ReadValue<Vector2>();
            InS_gameActions.DinopostreController.Movement.canceled += ctx => direction = ctx.ReadValue<Vector2>();
            //Bind Attacks
            InS_gameActions.DinopostreController.AttackA.performed += Controllers;
            InS_gameActions.DinopostreController.AttackB.performed += Controllers;

        }

        private void OnEnable()
        {
            InS_gameActions.Enable();
        }
        private void OnDisable()
        {
            InS_gameActions.Disable();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Movement()
        {
            Vector3 velocity = selfRigid.velocity;

            if (direction.magnitude > 0)
                ChangeDirection();

            velocity.x = direction.x;
            velocity.z = direction.y;



            selfRigid.velocity = velocity;
        }

        private void ChangeDirection()
        {
            Vector2 dir = direction.normalized;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            LeanTween.rotate(this.gameObject, new Vector3(0f, angle, 0f), 0.1f);
        }

        private void Controllers(InputAction.CallbackContext ctx)
        {
            //print(ctx.action.name.ToString());
            if (DP_current)
            {
                switch (ctx.action.name)
                {
                    case "Attack A":
                        DP_current.ExecuteAttack(0);
                        break;
                    case "Attack B":
                        DP_current.ExecuteAttack(1);
                        break;
                }
            }
        }

        public void SwitchDino()
        {

        }

        
    }
}

