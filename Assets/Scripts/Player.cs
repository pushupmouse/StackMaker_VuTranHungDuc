using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float playerSize = 1f;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private GameObject brickStack;
    [SerializeField] private float brickHeight;
    [SerializeField] private GameObject playerBody;

    private Vector3 endPos;
    private Vector3 moveDirection;
    private Ray rayDirection;
    private RaycastHit hit;
    private List<Brick> bricksCollected = new List<Brick>();
    private Brick brick;
    private int currentColor;
    

    private void Start()
    {
        endPos = transform.position;
        UIManager.Instance.SetScore(0);
    }

    private void FixedUpdate()
    {   
        CheckWall();
        Move();
        isMoving();
    }

    private void CheckWall()
    {
        rayDirection = new Ray(transform.position, moveDirection);
        Physics.Raycast(rayDirection, out hit, wallMask);

        if (hit.collider != null)
        {
            //direction to move up or down
            if (moveDirection.z != 0)
            {
                endPos.z = hit.collider.transform.position.z - moveDirection.z * playerSize;
            }
            //direction to move right or left
            else if (moveDirection.x != 0)
            {
                endPos.x = hit.collider.transform.position.x - moveDirection.x * playerSize;
            }
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.fixedDeltaTime);
    }

    public void GetDirection(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public bool isMoving()
    {
        if (transform.position == endPos)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag(MyConst.Tag.BRICK_TAG))
        {
            brick = collider.GetComponent<Brick>();

            if (bricksCollected.Count == 0)
            {
                currentColor = (int)brick.color;
            }

            if((int)brick.color == currentColor)
            {
                bricksCollected.Add(brick);
                brick.GetComponent<BoxCollider>().enabled = false;
                brick.transform.position = new Vector3(brickStack.transform.position.x, brickStack.transform.position.y + brickHeight * bricksCollected.Count, brickStack.transform.position.z);
                playerBody.transform.position = new Vector3(playerBody.transform.position.x, playerBody.transform.position.y + brickHeight, playerBody.transform.position.z);
                brick.transform.SetParent(brickStack.transform);
                UIManager.Instance.SetScore(bricksCollected.Count);
            }
        }

        if(collider.CompareTag(MyConst.Tag.ENDPOINT_TAG))
        {
            UIManager.Instance.EnablePanel(UIManager.Instance.victoryPanel);
            UIManager.Instance.DisableScore();
            UIManager.Instance.SetFinalScore(bricksCollected.Count);
            SwipeDetection.Instance.DisableControlsTarget();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(MyConst.Tag.BRIDGE_TAG))
        {
            if(bricksCollected.Count > 0)
            {
                brick = bricksCollected[0];
                brick.transform.SetParent(collision.transform);
                brick.transform.position = collision.transform.position + new Vector3(0, 0.5f, 0);
                playerBody.transform.position = new Vector3(playerBody.transform.position.x, playerBody.transform.position.y - brickHeight, playerBody.transform.position.z);
                brickStack.transform.position = new Vector3(brickStack.transform.position.x, brickStack.transform.position.y - brickHeight, brickStack.transform.position.z);
                bricksCollected.RemoveAt(0);

                collision.gameObject.tag = MyConst.Tag.BRICKED_TAG;
            }

        }
    }
}
