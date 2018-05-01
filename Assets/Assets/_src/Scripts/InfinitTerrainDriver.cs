using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Infinit terrain driver.
/// Moves terrains attached to the script with constant acceleration.
/// Constant acceleration is accurate because a train is physically 
/// heavy in real life. 
/// </summary>
public class InfinitTerrainDriver : MonoBehaviour {
    
    /// <summary>
    /// Set this object to train.
    /// </summary>
    public Transform relativeToObject;

    /// <summary>
    /// Maximum speed the train can reach in m/s
    /// </summary>
    public float maxSpeed;

    /// <summary>
    /// The current speed.
    /// </summary>
    private float currentSpeed = 0;

    /// <summary>
    /// Lower limit for maximum speed, 10 m/s => 36km/hour
    /// </summary>
    private const float MIN_MAX_SPEED = 10.0f;

    /// <summary>
    /// Acceleration of the train in m/s^2
    /// </summary>
    public float acceleration;

    /// <summary>
    /// terrain wrapped in GameObject.
    /// the ordering is important.
    /// Object on 0th index is assumed to be where the train is positioned at
    /// the start of the game.
    /// </summary>
    public GameObject[] terrains;

    /// <summary>
    /// starting index.
    /// </summary>
    private int currentTerrainIndex = 0;

    private Vector3 initialOffset;

    [SerializeField]
    private Vector3 direction = new Vector3(0.0f, 0.0f, 1.0f);
    // Use this for initialization
    void Start()
    {
        if (maxSpeed < MIN_MAX_SPEED)
        {
            maxSpeed = MIN_MAX_SPEED;
        }
        initialOffset = relativeToObject.position - terrains[currentTerrainIndex].transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateSpeedAndPosition(ref currentSpeed,
                               maxSpeed,
                               Time.deltaTime,
                               terrains);

        WrapTerrainTiles(relativeToObject, terrains);
    }
    /// <summary>
    /// Checkes if the reference object is pasted the current terrain tile,
    /// if so, wrap the tile at the end to the beginning.
    /// </summary>
    /// <param name="relativeCenter">Relative center.</param>
    /// <param name="terrains">Terrains.</param>
    public void WrapTerrainTiles(Transform relativeCenter, GameObject[] terrains) {
        GameObject currentTerrain = terrains[currentTerrainIndex];
        Vector3 displacement = currentTerrain.transform.position
                                             - relativeCenter.position + initialOffset;
        // check if train is past terrain tile;
        // if past terrain, wrap it around to the beginning
        if (Vector3.Dot(displacement, direction) >= 0.0f) {
            Debug.Log("wrapping");
            currentTerrain.transform.position -= direction * 250.0f * terrains.Length;
            currentTerrainIndex++;
            currentTerrainIndex = currentTerrainIndex % terrains.Length;
        }
    }
    /// <summary>
    /// Updates the speed and position of terrains.
    /// </summary>
    /// <param name="speed">Speed.</param>
    /// <param name="maxSpeed">Max speed.</param>
    /// <param name="deltaTime">Delta time.</param>
    /// <param name="objects">Objects.</param>
    private void UpdateSpeedAndPosition(ref float speed,
                                        float maxSpeed,
                                        float deltaTime,
                                        GameObject[] objects)
    {
        // v(t) = u + at;
        // s(t) = ut + 1/2*a*t*s;
        currentSpeed += acceleration * deltaTime;
        currentSpeed = currentSpeed < maxSpeed ? currentSpeed : maxSpeed;
        Vector3 positionOffset = direction * (currentSpeed * deltaTime);
        foreach (GameObject o in objects)
        {
            o.transform.position += positionOffset;
        }
    }
}
