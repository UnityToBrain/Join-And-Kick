using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager_1 : MonoBehaviour
{
    private bool MoveByTouch;
    private Vector3 Direction;
    private Rigidbody PlrRb;
    [SerializeField] private float runSpeed, velocity, swipeSpeed, roadSpeed;
    [SerializeField] private Transform road;
    private Animator StickMan_Anim;
    void Start()
    {
        PlrRb = transform.GetChild(0).GetComponent<Rigidbody>();
        StickMan_Anim = transform.GetChild(0).GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveByTouch = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            MoveByTouch = false;
        }

        if (MoveByTouch)
        {
            Direction = new Vector3(Mathf.Lerp(Direction.x,Input.GetAxis("Mouse X"), runSpeed * Time.deltaTime),0f);

            Direction = Vector3.ClampMagnitude(Direction, 1f);
            
            road.position = new Vector3(0f,0f,Mathf.SmoothStep(road.position.z, -100f,Time.deltaTime * roadSpeed));
            
            StickMan_Anim.SetFloat("run",1f);
        }
        else
        {
            StickMan_Anim.SetFloat("run",0f);
        }

        if (PlrRb.velocity.magnitude > 0.5f)
        {
            PlrRb.rotation = Quaternion.Slerp(PlrRb.rotation,Quaternion.LookRotation(PlrRb.velocity,Vector3.up), Time.deltaTime * velocity );
        }
        else
        {
            PlrRb.rotation = Quaternion.Slerp(PlrRb.rotation,Quaternion.identity, Time.deltaTime * velocity );
        }
    }

    private void FixedUpdate()
    {
        if (MoveByTouch)
        {
            Vector3 displacement = new Vector3(Direction.x,0f,0f) * Time.fixedDeltaTime;
            PlrRb.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSpeed,0f,0f) + displacement;
        }
        else
        {
            PlrRb.velocity = Vector3.zero;
        }
        
        //     foreach (var stickmanRb in rbList)
        //     {
        //         if (stickmanRb.velocity.magnitude > 0.5f)
        //         {
        //             foreach (var stickMan in teamMate)
        //                 stickMan.rotation = Quaternion.Slerp(stickMan.rotation, Quaternion.LookRotation(stickmanRb.velocity), Time.fixedDeltaTime * velocity);
        //         }
        //         else
        //         {
        //             foreach (var stickMan in teamMate)
        //                 stickMan.rotation = Quaternion.Slerp(stickMan.rotation, quaternion.identity, Time.fixedDeltaTime * velocity);
        //         }
        //
        //     }
        //
        //     
        // }
        //
        // private void FixedUpdate()
        // {
        //     if (MoveByTouch)
        //     {
        //         Vector3 displacement = new Vector3(Direction.x,0f,0f) * Time.deltaTime;
        //         
        //         foreach (var stickmanRb in rbList)
        //             stickmanRb.velocity = new Vector3(Direction.x * Time.deltaTime * SwipeSpeed,0f,0f) + displacement;
        //     }
        //     else
        //     {
        //         foreach (var stickmanRb in rbList)
        //             stickmanRb.velocity = Vector3.zero;
        //     }
        // }
    }
    
    
    
}
