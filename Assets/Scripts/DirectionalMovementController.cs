using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class DirectionalMovementController : MonoBehaviour
{
    private Animator animator;

    const string ANIM_PARAM_VELOCITY_X = "VelocityX";
    const string ANIM_PARAM_VELOCITY_Z = "VelocityZ";

    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float deacceleration = 0.5f;

    private float velocityX = 0.0f;
    private float velocityZ = 0.0f;
    private float maxWalkVelocity = 0.5f;
    private float maxRunVelocity = 2.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);

        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        ChangeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        animator.SetFloat(ANIM_PARAM_VELOCITY_X, velocityX);
        animator.SetFloat(ANIM_PARAM_VELOCITY_Z, velocityZ);
    }



    private void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // Increase forward velocity on forward pressed.
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += acceleration * Time.deltaTime;
        }

        //Decrease forward velocity.
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= deacceleration * Time.deltaTime;
        }

        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= acceleration * Time.deltaTime;
        }


        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += deacceleration * Time.deltaTime;
        }


        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += acceleration * Time.deltaTime;
        }

        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= deacceleration * Time.deltaTime;
        }
    }

    private void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        if (forwardPressed & runPressed & velocityZ > currentMaxVelocity)
        {
            // Reset to current max velocity.
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed & velocityZ > currentMaxVelocity)
        {
            velocityZ -= deacceleration * Time.deltaTime;

            if (velocityZ < currentMaxVelocity)
            {
                velocityZ = currentMaxVelocity;
            }
        }

        //Decrease forward velocity.
        if (!forwardPressed)
        {
            if (velocityZ < 0.0f)
            {
                velocityZ = 0.0f;
            }
        }

        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += deacceleration * Time.deltaTime;
            if (velocityX > -currentMaxVelocity)
            {
                velocityX = -currentMaxVelocity;
            }
        }

        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= deacceleration * Time.deltaTime;
            if (velocityX < currentMaxVelocity)
            {
                velocityX = currentMaxVelocity;
            }
        }

        if (!leftPressed && !rightPressed)
        {
            if (velocityX > 0.0f)
            {
                velocityX -= deacceleration * Time.deltaTime;
                if (velocityX < 0.0f)
                {
                    velocityX = 0.0f;
                }
            }

            if (velocityX < 0.0f)
            {
                velocityX += deacceleration * Time.deltaTime;

                if (velocityX > 0.0f)
                {
                    velocityX = 0.0f;
                }
            }
        }
    }
}
