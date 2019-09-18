using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BallMovingController : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody rb;
    private Vector2 direction;
    private float moveSpeed = 100f;
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

            if (Input.GetMouseButton(0) && !isEndLevel)
            {
                if (!cameraController.getBallIsSuperBouncing())
                {
                    mousePosition = GetWorldPositionOnPlane(Input.mousePosition, transform.position.y, 0);
                }
                else
                {
                    mousePosition = GetWorldPositionOnPlane(Input.mousePosition, childTransform.position.y, 0);

                }

                moveHorizontal();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

        }
        
        
        if( isEndLevel)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 12f);
            mousePosition = GetWorldPositionOnPlane(Vector3.zero, transform.position.y, 0);
            moveHorizontal();
        }
        
            
        

    }

     Vector3 GetWorldPositionOnPlane(Vector3 screenPosition,float y, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(screenPosition.normalized, new Vector3(0, y, z));
        
        float distance;
        
        xy.Raycast(ray, out distance);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        return ray.GetPoint(distance);
    }

    void setMousePosition(float x)
    {
        mousePosition.x = x;
        mousePosition.y = 0f;
        mousePosition.z = 0f;
    }

    void moveHorizontal()
    {
        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, 0);
    }
}
