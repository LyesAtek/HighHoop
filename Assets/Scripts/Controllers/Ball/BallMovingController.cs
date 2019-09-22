using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BallMovingController : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody rb;
    private Vector2 direction;
    private float moveSpeed = 10f;
    private CameraController cameraController;
    private Transform childTransform;
    private bool isMoving = false;
    private bool isEndLevel = false;
    private Vector2 startPos;
    private bool directionChosen;
    private bool paused = false;
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

    private void Update()
    {
		if (!paused)
		{
			if (isMoving && !isEndLevel)
			{
				transform.Translate(Vector3.forward * Time.deltaTime * 12f);
			}
			else if (isMoving && isEndLevel)
			{
				transform.Translate(Vector3.forward * Time.deltaTime * 12f);
			}
		}
    }
     void FixedUpdate()
      {
		if (!paused)
		{
			if (isMoving && !isEndLevel)
			{
				if (Input.GetMouseButton(0) || GamePlayManager.GetISAccelerometerMode())
				{
					if (GamePlayManager.GetISAccelerometerMode())
					{
						Vector3 movement = new Vector3(Input.acceleration.x * moveSpeed, 0.0f, 0.0f);
						rb.velocity = movement;
					}
					else
					{
						Touch touch = Input.GetTouch(0);
						if (touch.phase == TouchPhase.Moved)
						{
							Vector3 movement = new Vector3(touch.deltaPosition.x, 0.0f, 0.0f);
							rb.velocity = movement;
						}
						else if (touch.phase == TouchPhase.Stationary)
						{
							rb.velocity = Vector3.zero;
						}
					}

				}

			}
			else if (isMoving && isEndLevel)
			{
				// transform.Translate(Vector3.forward * Time.deltaTime * 10f);
				mousePosition = GetWorldPositionOnPlane(Vector3.zero, 0, 0);
				moveHorizontal();
			}
			else if (!isMoving || paused)
			{
				rb.velocity = Vector3.zero;
			}
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
         rb.velocity = new Vector2(direction.x * 120f, 0);
        
    
    }

    public void setPaused(bool value)
    {
        paused = value;
    }

    public bool getPaused()
    {
        return paused;
    }
    public void resetVelocity()
    {
        rb.velocity = rb.velocity = Vector3.zero;
    }
}
