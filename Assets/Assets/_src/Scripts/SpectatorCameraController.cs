using UnityEngine;
using System.Collections;

public enum CameraMovementMode {
    Free,
    Follow,
}

[RequireComponent(typeof(Camera))]
public class SpectatorCameraController : MonoBehaviour
{
    private CameraMovementMode mode = CameraMovementMode.Free;
    private GameObject following;
    private Camera spectatorCamera;


    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    public bool lockCursor;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;

	// Use this for initialization
	void Start()
	{
        targetDirection = transform.localRotation.eulerAngles;
        spectatorCamera = GetComponent<Camera>();
        if(spectatorCamera == null) {
            spectatorCamera = Camera.main;
        }

        spectatorCamera.transform.position = new Vector3(0, 1.7f, 0);
	}

	// Update is called once per frame
	void Update()
	{
        UpdateCameraMode();
        switch(mode) {
            case CameraMovementMode.Free:
                UpdateFreeCam();
                break;
            case CameraMovementMode.Follow:
                UpdateFollowCam();
                break;
        }

	}
    void UpdateCameraMode() {
        if(Input.GetKeyDown(KeyCode.F)) {
            mode = CameraMovementMode.Free;
        } else if(Input.GetKeyDown(KeyCode.G)) {
            mode = CameraMovementMode.Follow;
        }
    }
    void UpdateFollowCam() {
        
    }
    void UpdateFreeCam() {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        var targetOrientation = Quaternion.Euler(targetDirection);

        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }
        float forwardAmount = Input.GetAxis("Vertical");
        float sideAmount = Input.GetAxis("Horizontal");
        Vector3 translation = spectatorCamera.transform.forward * forwardAmount
                                             + spectatorCamera.transform.right * sideAmount;
        spectatorCamera.transform.position += translation;

    }
}
