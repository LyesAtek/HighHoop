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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (Input.GetMouseButton(0) && GameManager.GetBallState() == GameManager.State.InGame)
        {
            mousePosition.x = Input.mousePosition.x;
            mousePosition.y = (Screen.height - Input.mousePosition.y);
            mousePosition.z = 0f;
            mousePosition =  GetWorldPositionOnPlane(Input.mousePosition,0);
            direction = (mousePosition - transform.position).normalized;
           // print("MousePosition x : " +mousePosition.x + " TransformPosition x : " + transform.position.x);
            rb.velocity = new Vector2(direction.x * moveSpeed,0);
           
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
       
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(screenPosition.normalized, new Vector3(0, 0, z));
        float distance;
        
        xy.Raycast(ray, out distance);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        return ray.GetPoint(distance);
    }
}
