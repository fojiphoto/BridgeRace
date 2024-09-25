using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyJoystick;
using TMPro;

public class Character : Player
{
    [SerializeField] Joystick joystick;
    float xMovement;
    float zMovement;
    bool isSwipe = false;
    private void Start()
    {
        OnInitPlayer();
    }

    private void Update()
    {
        if(fall)
        {
            anim.SetTrigger("Fall");
        }
        else
        {
            CheckJoyStick();
        }
    }

    private void FixedUpdate()
    {
        if (fall)
        {
            anim.SetTrigger("Fall");
        }
        else
        {
            CharMove();
            RotatePlayer();
            CheckBrick();
            Debug.Log("currBrickOnBack" + currBrickOnBack);
        }
    }

    void RotatePlayer()
    {
        if(isSwipe)
        {
            if(zMovement > 0)
            {
                isMovingDown = false;
            } else
            {
                isMovingDown = true;
                canMove = true;
            }
            float angle = joystick.GetAngle();
            transform.rotation = Quaternion.Euler(0, angle, 0);
            anim.SetTrigger("Running");
        } else
        {
            anim.SetTrigger("Idle");
            return;
        }
    }

    void CheckJoyStick()
    {
        xMovement = joystick.Horizontal();
        zMovement = joystick.Vertical();
        if (Mathf.Abs(xMovement) >= 0.01f || Mathf.Abs(zMovement) >= 0.01f)
        {
            isSwipe = true;
        }
        else
        {
            isSwipe = false;
        }
    }

    void CharMove()
    {
        if(isSwipe && canMove)
        {
            rb.velocity = new Vector3(xMovement * speed, rb.velocity.y, zMovement * speed);
            if (zMovement <= 0f)
            {
                rb.AddForce(Vector3.down * 100f, ForceMode.Acceleration);
            }
        } else
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * 100f, ForceMode.Acceleration);
        }
    }
}
