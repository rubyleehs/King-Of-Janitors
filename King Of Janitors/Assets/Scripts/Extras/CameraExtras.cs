using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraExtras : MonoBehaviour
{
    public static float width;
    public static float height;

    new Transform transform;
    private Camera cam;

    protected virtual void Start()
    {
        if (cam == null) cam = GetComponent<Camera>();
        if (transform == null) transform = GetComponent<Transform>();

        if (height > 0) cam.orthographicSize = height;
        else height = cam.orthographicSize;

        width = height * cam.aspect;
    }

    public void SetHeight(float value)
    {
        height = value;
        width = height * cam.aspect;
        cam.orthographicSize = height;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, position.z);
    }
    public void SetPosition(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    public Vector2 GetMouseWorld2DPoint()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

}
