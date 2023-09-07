using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Dinopostres.Managers;
using Dinopostres.Definitions;
using Dinopostres.Events;
using System.Threading.Tasks;

namespace Dinopostres.CharacterControllers
{
    public class Player : Controller
    {
        public static Player PL_Instance;
        
        private Vector2 direction;

        private bool isDispacherOpen=false;
        private DinoSaveData currentData;
        public bool IsDispacheOpen { set => isDispacherOpen = value; }

        private new void Awake()
        {
            //Start Instance
            PL_Instance = this;
            
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            DinoSaveData info = ((GameModeINSTAGE)GameMode._Instance).GetActiveDino();
            CreateDinoInstance(info);

            gameObject.tag = "Player";

            //Stats Dino
            if (base.DP_current)
            {
                base.DP_current.IsPlayer = true;
                DP_current.InitStats(currentData._Level,currentData);
            }
            //init camera
            Camera.main.gameObject.AddComponent<CameraController>();

            //Bind Attacks
            GameMode._Instance.SetControllerFuntions(ControllersManager.PlayerActions.AttackA,
                ControllersManager.InputState.Perform, Controllers);
            GameMode._Instance.SetControllerFuntions(ControllersManager.PlayerActions.AttackB,
                ControllersManager.InputState.Perform, Controllers);
            GameMode._Instance.SetControllerFuntions(ControllersManager.PlayerActions.Dispacher,
                ControllersManager.InputState.Perform, Controllers);
        }

        protected override void OnDestroy()
        {
            GameMode._Instance.SetControllerFuntions(ControllersManager.PlayerActions.AttackA,
                ControllersManager.InputState.Perform, Controllers,false);
            GameMode._Instance.SetControllerFuntions(ControllersManager.PlayerActions.AttackB,
                ControllersManager.InputState.Perform, Controllers,false);
            GameMode._Instance.SetControllerFuntions(ControllersManager.PlayerActions.Dispacher,
                ControllersManager.InputState.Perform, Controllers,false);
            base.OnDestroy();
        }

        protected override void Update()
        {
            if(!isDispacherOpen)
                base.Update();

            MoveHealBar();
        }
        protected override void GetDead()
        {
            ((GameModeINSTAGE)(GameMode._Instance)).LoseLive();

            if ((GameMode._Instance._GameData._DinoInventory.Count- ((GameModeINSTAGE)(GameMode._Instance))._LivesInverse) >0)
            {
                GameManager._instance.PauseGame(true);
                OpenDispacher();
                isDead = true;
            }
        }
        protected override void GetHIT()
        {
            if(isDispacherOpen && !isDead)
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.dispacher,!isDispacherOpen);

            currentData._CurrentHealth = DP_current._CurrentHealth;
        }

        protected override void Movement()
        {
            Vector3 velocity = selfRigid.velocity;

            if (direction.magnitude > 0 && GameMode._Instance._Time>0)
                ChangeDirection();

            if(GameMode._Instance!=null)
                direction = GameMode._Instance.GetControllerValue(ControllersManager.PlayerActions.Movement).ReadValue<Vector2>();

            DP_current.SetAnimationVariable("f_Speed",direction.magnitude);

            velocity.x = direction.x;
            velocity.z = direction.y;



            selfRigid.velocity = velocity *f_Speed* (GameMode._Instance._Time*2f);
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
            if(!isDead)
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.dispacher,!isDispacherOpen);
        }

        public void SwitchDino(DinoSaveData _newDino)
        {
            
            if (DP_current != null && currentData._Dino!=_newDino._Dino)
            {
                
                Destroy(DP_current.gameObject);
                //Manda actualizar su estado de seleccion en la base de datos
                currentData._IsSelected = false;
                CreateDinoInstance(_newDino);
            }
            else if(currentData._Dino == _newDino._Dino)
            {
                SetInitStats(_newDino);
            }

            isDispacherOpen = false;
            
            //Aumenta el numero de llamadas de cambio de dino en la base de datos
            RecordEvent ev = new RecordEvent(1, "Switch dino", 2);
            GameMode.OnRecordEvent(ev);

            if (isDead)
            {
                GameManager._instance.PauseGame(false);
                isDead = false;
            }
        }

        public void CreateDinoInstance(DinoSaveData _newDino)
        {
            DinoPostre dino;
             dino = (Instantiate(EnemyStorage._Instance().Look4DinoDef(_newDino._Dino)._Prefab, transform) as GameObject).GetComponent<DinoPostre>();
            DP_current = dino;

            SetInitStats(_newDino);
        }

        public void SetInitStats(DinoSaveData _newDino)
        {
            DP_current.IsPlayer = true;
            DP_current.transform.localPosition = Vector3.zero;
            DP_current.InitStats(_newDino._Level);
            DP_current._CurrentHealth = _newDino._CurrentHealth;

            currentData = _newDino;
            currentData._IsSelected = true;
            f_Speed = (100 / DP_current._Peso);

            UpdateHealthBar();
        }
        
    }
}

