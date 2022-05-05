using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Dinopostres.Managers;
using Dinopostres.Definitions;

namespace Dinopostres.CharacterControllers
{
    public class Player : Controller
    {
        public static Player PL_Instance;
        
        private Vector2 direction;

        private bool isDispacherOpen=false;
        private DinoSaveData currentData;
        public bool IsDispacheOpen { set => isDispacherOpen = value; }

        private void Awake()
        {
            //Start Instance
            PL_Instance = this;
            
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            DinoSaveData info = GameManager._instance.GetActiveDino();
            SwitchDino(info);

            base.Start();

            gameObject.tag = "Player";

            //Stats Dino
            if (base.DP_current)
            {
                base.DP_current.IsPlayer = true;
                DP_current.InitStats(currentData.Level,currentData);
            }
            //init camera
            Camera.main.gameObject.AddComponent<CameraController>();

            //Bind movement action
            GameManager._instance.InS_gameActions.DinopostreController.Movement.performed += ctx => direction = ctx.ReadValue<Vector2>();
            GameManager._instance.InS_gameActions.DinopostreController.Movement.canceled += ctx => direction = ctx.ReadValue<Vector2>();
            //Bind Attacks
            GameManager._instance.InS_gameActions.DinopostreController.AttackA.performed += Controllers;
            GameManager._instance.InS_gameActions.DinopostreController.AttackB.performed += Controllers;
            GameManager._instance.InS_gameActions.DinopostreController.Dispacher.performed += Controllers;
        }

        

        protected override void Update()
        {
            if(!isDispacherOpen)
                base.Update();

            MoveHealBar();
        }
        protected override void GetDead()
        {
            GameManager._instance.LoseLive();
            
        }
        protected override void GetHIT()
        {
            if(isDispacherOpen)
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.dispacher,!isDispacherOpen);

            currentData.CurrentHealth = DP_current.CurrentHealth;
        }

        protected override void Movement()
        {
            Vector3 velocity = selfRigid.velocity;

            if (direction.magnitude > 0 && GameManager._instance._TimeScale>0)
                ChangeDirection();

            velocity.x = direction.x;
            velocity.z = direction.y;



            selfRigid.velocity = velocity * GameManager._instance._TimeScale;
        }

        private void ChangeDirection()
        {
            Vector2 dir = direction.normalized;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            LeanTween.rotate(this.gameObject, new Vector3(0f, angle, 0f), 0.1f);
        }

        private void Controllers(InputAction.CallbackContext _ctx)
        {

            if (DP_current)
            {
                switch (_ctx.action.name)
                {
                    case "Attack A":
                        if (!isDispacherOpen)
                            DP_current.ExecuteAttack(0);
                        break;
                    case "Attack B":
                        if (!isDispacherOpen)
                            DP_current.ExecuteAttack(1);
                        break;
                    case "Dispacher":
                        OpenDispacher();
                        break;
                }
            }
        }

        private void OpenDispacher()
        {
            LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.dispacher,!isDispacherOpen);
        }

        public void SwitchDino(DinoSaveData _newDino)
        {
            if(DP_current!=null)
                Destroy(DP_current.gameObject);

            DinoPostre dino = (Instantiate(EnemyStorage._Instance().Look4DinoDef(_newDino.Dino)._Prefab, transform) as GameObject).GetComponent<DinoPostre>();
            DP_current = dino;
            DP_current.IsPlayer = true;
            DP_current.transform.localPosition = Vector3.zero;
            DP_current.InitStats(_newDino.Level);
            DP_current.CurrentHealth = _newDino.CurrentHealth;
            currentData = _newDino;
        }
        
    }
}

