using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDriver : MonoBehaviour
{
    public Rigidbody trainEngine;
    public CatmullRomSpline pathConstraint;


    private int segment = 0;

    private bool running = true;
    private float t = 0.0f;

    public float speed;
    // Use this for initialization
    void Start()
    {
        if (speed < 0.1f)
        {
            speed = 1.0f;
        }
        transform.position = pathConstraint.transform.position + pathConstraint.controlPoints[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (trainEngine != null && running)
        {
            // trainEngine.velocity = Vector3.forward * -speed;

            Vector3 position = pathConstraint.GetCurvePoint(segment, t)
                                             + pathConstraint.transform.position;
            Vector3 direction = pathConstraint.GetTangent(segment, t);
            direction.Normalize();
            trainEngine.MovePosition(position);
            trainEngine.MoveRotation(Quaternion.FromToRotation(-1.0f * Vector3.forward, direction));
            t += 0.003f;
            if (t >= 1.0f)
            {
                t = 0.0f;
                segment++;
            }
            if(segment == pathConstraint.GetSegmentCount()) {
                Halt();
            }
        }
    }
    public void Go()
    {
        running = true;
    }

    public void Halt()
    {
        running = false;
    }
}
