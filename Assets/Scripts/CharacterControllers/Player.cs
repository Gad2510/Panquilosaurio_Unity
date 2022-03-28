using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player PL_Instance;

    DinoPostreAction InS_gameActions;

    private Rigidbody selfRigid;

    public Vector2 direction;

    // Start is called before the first frame update
    void Awake()
    {
        InS_gameActions = new DinoPostreAction();
        InS_gameActions.DinopostreController.Movement.performed += ctx => direction= ctx.ReadValue<Vector2>();
        InS_gameActions.DinopostreController.Movement.canceled += ctx => direction = ctx.ReadValue<Vector2>();
        selfRigid = GetComponent<Rigidbody>();
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

        velocity.x = direction.x;
        velocity.z = direction.y;

        selfRigid.velocity = velocity;
    }

    private void Controllers()
    {

    }

    public void SwitchDino()
    {

    }
}
