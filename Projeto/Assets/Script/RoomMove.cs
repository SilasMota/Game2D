using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour {

    public Vector2 cameraChangemax;
    public Vector2 cameraChangemin;
    public Vector3 playerChange;
    private CameraMovment cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main.GetComponent<CameraMovment>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger == false)
        {
            cam.minPosition += cameraChangemin;
            cam.maxPosition += cameraChangemax;
            other.transform.position += playerChange;
        }
    }
}
