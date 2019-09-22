using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    private GameObject ballChildren;
    private Vector3 offsetBall;
    private Vector3 offsetBallChildren;
    private bool ballIsSuperBouncing = false;
    // Start is called before the first frame update
    void Start()
    {
        ballChildren = ball.transform.GetChild(0).gameObject;
        offsetBall = transform.position - ball.transform.position;
        offsetBallChildren = transform.position - ballChildren.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!ballIsSuperBouncing)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ball.transform.position.z + offsetBall.z);

        }
        else
        {
            transform.position = new Vector3(transform.position.x, ballChildren.transform.position.y + offsetBallChildren.y, ballChildren.transform.position.z + offsetBallChildren.z);

        }

    }

    public void setBallIsSuperBouncing(bool value)
    {
        ballIsSuperBouncing = value;
    }

    public bool getBallIsSuperBouncing()
    {
        return ballIsSuperBouncing;
    }
}
