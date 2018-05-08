using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOnTrigger : MonoBehaviour {

    public GameObject target;

	private void Start()
	{
        target?.SetActive(false);
	}
	private void OnTriggerEnter(Collider other)
	{
        target?.SetActive(true);
	}

	private void OnTriggerExit(Collider other)
	{
        target?.SetActive(false);
	}

	void Update()
	{
        if (target == null || Camera.main == null)
        {
            return;
        }
        if (target.activeInHierarchy)
        {
            var n = target.transform.position - Camera.main.transform.position;
            var newRotation = Quaternion.LookRotation(n) * Quaternion.Euler(0, 180, 0);
            target.transform.rotation = Quaternion.Slerp(target.transform.rotation, newRotation, Time.deltaTime * 0.98f);
        }
	}
}
