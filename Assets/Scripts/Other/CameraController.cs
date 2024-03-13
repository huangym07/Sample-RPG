using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 5;
    private Transform playerTransform;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        offset = Camera.main.transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = offset + playerTransform.position;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView += scroll * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 30, 70);
    }
}
