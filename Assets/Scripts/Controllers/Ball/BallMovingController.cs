using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BallMovingController : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody rb;
    private Vector2 direction;
    private float moveSpeed = 120f;
    private CameraController cameraController;
    private Transform childTransform;
    private bool isMoving = false;
    private bool isEndLevel = false;
    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        childTransform = transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
    }

   public void setIsMoving(bool value)
    {
        isMoving = value;
    }

    public void setIsEndLevel(bool value)
    {
        isEndLevel = value;
    }

    void FixedUpdate()
    {

        if (isMoving && !isEndLevel)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 12f);
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButton(0))
                {
                    if (Input.GetAxis("Mouse X") > 0f || Input.GetAxis("Mouse X") < 0)
                    {
                        Vector3 movement = new Vector3(Input.GetAxis("Mouse X") * moveSpeed, 0.0f, 0.0f);
                        rb.velocity = movement;
                       // rb.AddForce(movement * moveSpeed, ForceMode.Acceleration);
                    }
                    else 
                    {
                        rb.velocity = Vector3.zero;
                    }
                }

            }
            else
            {
                rb.velocity = Vector3.zero;
            }

        } else if (isMoving && isEndLevel)
        { 
              transform.Translate(Vector3.forward * Time.deltaTime * 12f);
              mousePosition = GetWorldPositionOnPlane(Vector3.zero, 0, 0);
              moveHorizontal();
        }else if (!isMoving)
        {
            rb.velocity = Vector3.zero;
        }

    }
   
    Vector3 GetWorldPositionOnPlane(Vector3 screenPosition,float y, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
       
        Plane xy = new Plane(screenPosition.normalized, new Vector3(0, y, z));
        
        float distance;
        
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
    void moveHorizontal()
    {
         direction = (mousePosition - transform.position).normalized;
         rb.velocity = new Vector2(direction.x * moveSpeed, 0);
        
    
    }
}
