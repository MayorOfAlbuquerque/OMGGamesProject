using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationDriver : MonoBehaviour {

    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float verticalInput = Input.GetAxis("Vertical");
        if(verticalInput > 0.3 || verticalInput < - 0.3) {
            animator.SetBool("IsWalking", true);
        } else {
            animator.SetBool("IsWalking", false);
        }
	}
}
