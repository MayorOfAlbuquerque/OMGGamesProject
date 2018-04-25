using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Compass : MonoBehaviour
{
    [Tooltip("Object to point toward")]
    public GameObject toward;
    [Tooltip("Compass will show when player is outside the range and hide when player is within the range to the target")]
    public float distanceCutoff;

    [Tooltip("Eye position")]
    public Transform eye;

    private MeshRenderer meshRenderer;
    void Start()
	{
        distanceCutoff = distanceCutoff < 0.001f ? 0.001f : distanceCutoff;
        meshRenderer = GetComponent<MeshRenderer>();
	}
	void Update()
    {
        if (toward != null)
        {
            transform.LookAt(toward.transform);
            Vector3 forward = transform.forward;
            forward.y = 0;
            transform.forward = forward;
        }
        if (isVisibleOnMainCamera(toward))
        {
            meshRenderer.enabled = false;
        }
        else
        {
            meshRenderer.enabled = true;
        }
    }
    bool isVisibleOnMainCamera(GameObject obj)
    {
        Vector3 eyePosition = eye?.position ?? Vector3.zero;
        Vector3 screenCoord = Camera.main.WorldToViewportPoint(toward.transform.position);
        Vector3 lineOfSight = toward.transform.position - eyePosition;
        float distance2 = Vector3.Dot(lineOfSight, lineOfSight);
        Debug.Log("distance2:" + distance2);
        if (distance2 < distanceCutoff * distanceCutoff)
        {
            return true;
        }
        bool onScreen = screenCoord.z > 0 && screenCoord.x > 0 && screenCoord.x < 1 && screenCoord.y > 0 && screenCoord.y < 1;
        if(!onScreen) {
            return false;
        }
        RaycastHit result;
        if (!Physics.Raycast(eyePosition, lineOfSight, out result))
        {
            return false;
        }
        if (!object.ReferenceEquals(result.transform.gameObject, toward))
        {
            return false;
        }

        return true;
    }
}
