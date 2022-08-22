using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float movespeed;
	public float maxspeed;
	public float jumpforce;

	Rigidbody2D playerRB;

	[HideInInspector]
	public bool feetContact;

	void Awake()
	{
		playerRB = gameObject.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		float MoveHor = Input.GetAxisRaw("Horizontal");
		Vector2 movement = new Vector2(MoveHor * movespeed, 0);
        Debug.Log(movement);
		playerRB.AddForce(movement);
		if (playerRB.velocity.x > maxspeed)
		{
			playerRB.velocity = new Vector2(maxspeed, playerRB.velocity.y);
		}
		if (playerRB.velocity.x < -maxspeed)
		{
			playerRB.velocity = new Vector2(-maxspeed, playerRB.velocity.y);
		}
		if (Input.GetKeyDown(KeyCode.Space) && canJump())
		{
			playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
			playerRB.AddForce(new Vector2(0, jumpforce));
		}
	}

	bool canJump()
	{
		return feetContact;
	}
}
