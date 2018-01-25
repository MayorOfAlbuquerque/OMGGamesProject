using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SinglePlayerMovement : MonoBehaviour {
    [SerializeField] float speed;

    private void Start()
    {
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 newPos;
        
        if (h != 0 || v != 0)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            newPos = calculate(transform.position, h, v, right, forward);
        }
        else newPos = transform.position;
        
        transform.position = newPos;
    }

    public Vector3 calculate(Vector3 position, float h, float v, Vector3 right, Vector3 forward)
    {
        Vector3 newPos = position + (right * h * speed * Time.deltaTime);
        newPos = newPos + (forward * v * speed * Time.deltaTime);
        newPos.y = 1.6f;
        return newPos;
    }
}
