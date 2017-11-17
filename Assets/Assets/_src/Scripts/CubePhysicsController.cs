using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class CubePhysicsController : InteractableObjectController, IGvrPointerHoverHandler
{
    public const float MIN_DISTANCE_FROM_CAMERA = 0.8f;
    private new Rigidbody rigidbody;
    private Vector3 raycastPosition;
    private Vector3 cameraPosition;
    private Vector3 cameraDirection;
    private bool pickingUp = false;
    public float distanceFromCamera;
	// Use this for initialization
	void Start()
	{
        rigidbody = GetComponent<Rigidbody>();
        if(distanceFromCamera < MIN_DISTANCE_FROM_CAMERA) {
            distanceFromCamera = MIN_DISTANCE_FROM_CAMERA;
        }
	}

    
	// Update is called once per frame
	void Update()
	{
        if(!pickingUp) {
            return;
        }
        rigidbody.transform.position = distanceFromCamera * cameraDirection + cameraPosition;
	}

    public override void OnClick()
    {
        rigidbody.velocity = 5 * Vector3.up;
    }

    public override void OnKeyDown(KeyCode code)
    {
        if(code != KeyCode.Mouse1) 
        {
            return;
        }
        rigidbody.isKinematic = true;
        pickingUp = true;
    }

    public override void OnKeyUp(KeyCode code)
    {
        if(code != KeyCode.Mouse1) {
            return;
        }
        rigidbody.isKinematic = false;
        pickingUp = false;
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        if (pickingUp)
        {
            cameraPosition = eventData.enterEventCamera.transform.position;
            cameraDirection = eventData.enterEventCamera.transform.forward;
        } else {
            raycastPosition = eventData.pointerCurrentRaycast.worldPosition;
            cameraPosition = eventData.enterEventCamera.transform.position;
            cameraDirection = eventData.enterEventCamera.transform.forward;
        }
    }
}
