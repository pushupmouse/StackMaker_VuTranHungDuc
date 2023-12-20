using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance;

    public static CameraFollow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<CameraFollow>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<CameraFollow>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed = 20f;

    private Transform target;
    
    private void FixedUpdate()
    {
        if(target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * speed);
        }
    }

    public void SetCameraTarget()
    {
        try
        {
            target = FindObjectOfType<Player>().transform;
        }
        catch (System.Exception)
        { 
            Debug.Log("No target for camera");
        }
        
    }

    public void DisableCameraTarget()
    {
        target = null;
    }
}
