using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dinopostres.CharacterControllers
{
    public class Player : MonoBehaviour
    {
        public static Player PL_Instance;

        private DinoPostreAction InS_gameActions;
        private DinoPostre DP_current;
        private Rigidbody selfRigid;

        private Vector2 direction;

        // Start is called before the first frame update
        void Awake()
        {
            InS_gameActions = new DinoPostreAction();
            //Bind movement action
            InS_gameActions.DinopostreController.Movement.performed += ctx => direction = ctx.ReadValue<Vector2>();
            InS_gameActions.DinopostreController.Movement.canceled += ctx => direction = ctx.ReadValue<Vector2>();
            //Bind Attacks
            InS_gameActions.DinopostreController.AttackA.performed += Controllers;
            InS_gameActions.DinopostreController.AttackB.performed += Controllers;
            //Get references
            selfRigid = GetComponent<Rigidbody>();
            DP_current = GetComponentInChildren<DinoPostre>();

            if (!DP_current)
            {
                //Select a default if is startgame else the first found in the dispacher
            }
        }

        private void OnEnable()
        {
            InS_gameActions.Enable();
        }
        private void OnDisable()
        {
            InS_gameActions.Disable();
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            Vector3 velocity = selfRigid.velocity;

            Vector2 dir = direction.normalized;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            selfRigid.MoveRotation(Quaternion.Euler(0f, angle, 0f));

            velocity.x = direction.x;
            velocity.z = direction.y;



            selfRigid.velocity = velocity;
        }

        private void Controllers(InputAction.CallbackContext ctx)
        {
            print(ctx.action.name.ToString());
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

