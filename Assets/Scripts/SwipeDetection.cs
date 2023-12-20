using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SwipeDetection : MonoBehaviour
{
    private static SwipeDetection instance;

    public static SwipeDetection Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<SwipeDetection>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<SwipeDetection>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private int pixelDistToDetect = 20;
    private Player player;

    private Vector2 startPos;
    private bool fingerDown;

    private void Update()
    {
        if (player != null && !player.isMoving())
        {
            if (fingerDown == false && Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                fingerDown = true;
            }

            if (fingerDown)
            {
                //up
                if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
                {
                    fingerDown = false;
                    player.GetDirection(new Vector3(0, 0, 1));
                }
                //down
                else if (Input.mousePosition.y <= startPos.y - pixelDistToDetect)
                {
                    fingerDown = false;
                    player.GetDirection(new Vector3(0, 0, -1));
                }
                //right
                else if (Input.mousePosition.x >= startPos.x + pixelDistToDetect)
                {
                    fingerDown = false;
                    player.GetDirection(new Vector3(1, 0, 0));
                }
                //left
                else if (Input.mousePosition.x <= startPos.x - pixelDistToDetect)
                {
                    fingerDown = false;
                    player.GetDirection(new Vector3(-1, 0, 0));
                }
            }

            if (fingerDown && Input.GetMouseButtonUp(0))
            {
                fingerDown = false;
            }
        }
    }

    public void SetControlsTarget()
    {
        try
        {
            player = FindObjectOfType<Player>();
        }
        catch (System.Exception)
        {
            Debug.Log("No target for controls");
        }
        
    }

    public void DisableControlsTarget()
    {
        player = null;
    }
}
