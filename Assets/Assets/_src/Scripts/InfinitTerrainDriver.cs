using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitTerrainDriver : MonoBehaviour {
    public GameObject relativeToObject;
    // m/s
    public float maxSpeed;

    // 10 m/s => 36km/hour
    private const float MIN_MAX_SPEED = 10.0f;
    // m/s^2
    public float acceleration;

    public GameObject[] terrains;
    private int currentTerrainIndex = 0;

    private float currentSpeed = 0;
    private Vector3 direction = new Vector3(0.0f, 0.0f, 1.0f);
    // Use this for initialization
    void Start()
    {
        if (maxSpeed < MIN_MAX_SPEED)
        {
            maxSpeed = MIN_MAX_SPEED;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeedAndPosition(ref currentSpeed,
                               maxSpeed,
                               Time.deltaTime,
                               terrains);

        WrapTerrainTiles(relativeToObject, terrains);
    }
    public void WrapTerrainTiles(GameObject relativeCenter, GameObject[] terrains) {
        GameObject currentTerrain = terrains[currentTerrainIndex];
        Vector3 displacement = currentTerrain.transform.position
                                         - relativeCenter.transform.position;
        // check if train is past terrain tile;
        // if past terrain, wrap it around to the beginning
        if (Vector3.Dot(displacement, direction) >= 0.0f) {
            Debug.Log("wrapping");
            currentTerrain.transform.position -= direction * 256.0f * (2);
            currentTerrainIndex++;
            currentTerrainIndex = currentTerrainIndex % terrains.Length;
        }
    }
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
