using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPostions;
    public GameObject[] rooms; // 0: LR, 1: LRB, 2: LRT, 3: LRBT
    public GameObject player;

    private int direction;
    public float moveAmount = 10;


    private float timeBtwRoom;
    public float startTimeBtwRoom = 1f;

    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration;

    public LayerMask room;

    private bool downRecently;

    // Start is called before the first frame update
    void Start()
    {
        int randStartPos = Random.Range(0, startingPostions.Length);
        transform.position = startingPostions[randStartPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        Vector3 playerPos = new Vector3(transform.position.x + 2.5f, transform.position.y - 2f, transform.position.z);
        Instantiate(player, playerPos, Quaternion.identity);
        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && !stopGeneration)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    void Move()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
        int rand = Random.Range(0, rooms.Length);
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {
                newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                direction = Random.Range(1, 6);
                if (direction == 3 || direction == 4)
                {
                    direction = 2;
                }
                downRecently = false;
            } else
            {
                direction = 5;
            }
        } else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                direction = Random.Range(3, 6);
                downRecently = false;
            } else
            {
                direction = 5;
            }
        } if (direction == 5)
        {
            if (transform.position.y > minY)
            {
                Collider2D roomDetect = Physics2D.OverlapCircle(transform.position, 1, room);
                int roomType = roomDetect.GetComponent<RoomType>().type;
                if (roomType == 2 || roomType == 0)
                {
                    roomDetect.GetComponent<RoomType>().RoomDestruction();

                    int randBottomRoom = Random.Range(1, 4);
                    if (randBottomRoom == 2)
                    {
                        randBottomRoom = 1;
                    }
                    if (downRecently)
                    {
                        randBottomRoom = 3;
                    }
                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                }

                newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                rand = Random.Range(2, 4);
                direction = Random.Range(1, 6);
                downRecently = true;
            }
            else
            {
                //STOP LEVEL GENERATION
                stopGeneration = true;
                Debug.Log("StopGeneration");
            }
            
        }
        transform.position = newPos;
        if (!stopGeneration)
        {
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
    }
}
