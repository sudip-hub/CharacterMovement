using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Animator animator;

    private const string ANIM_PARAM_IS_WALKING = "isWalking";
    private const string ANIM_PARAM_IS_RUNNING = "isRunning";
    private const string ANIM_PARAM_VELOCITY = "Velocity";

    private float velocity = 0.0f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float deacceleration = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isWalking = animator.GetBool(ANIM_PARAM_IS_WALKING);
        bool isRunning = animator.GetBool(ANIM_PARAM_IS_RUNNING);

        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // // If player presses forward button while character is not moving.
        // if (forwardPressed && !isWalking)
        // {
        //     // Set isWalking bool to true.
        //     animator.SetBool(ANIM_PARAM_IS_WALKING, true);
        // }

        // // If player released forward button while character is moving.
        // if (!forwardPressed && isWalking)
        // {
        //     // Set isWalking bool to false.
        //     animator.SetBool(ANIM_PARAM_IS_WALKING, false);
        // }

        // // If player presses run button while pressing forward button. 
        // if (runPressed && forwardPressed)
        // {
        //     // Set isRunning bool to true.
        //     animator.SetBool(ANIM_PARAM_IS_RUNNING, true);
        // }

        // // If player releases run button or releases forward button. 
        // if (!runPressed || !forwardPressed)
        // {
        //     // Set isRunning bool to true.
        //     animator.SetBool(ANIM_PARAM_IS_RUNNING, false);
        // }

        if (forwardPressed && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deacceleration;
        }

        if (velocity < 0.0f)
        {
            velocity = 0.0f;
        }

        animator.SetFloat(ANIM_PARAM_VELOCITY, velocity);
    }
}
