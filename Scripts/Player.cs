using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float movespeed;

	public float jumpforce;

	Rigidbody2D playerRB;

	[HideInInspector]
	public bool feetContact;

    #region Animator variables
    private Animator m_animator;
	private FeetCollider m_groundSensor;
	private int m_facingDirection = 1;
	private bool m_grounded = false;
	private bool m_moving = false;
	#endregion

	void Awake()
	{
		playerRB = gameObject.GetComponent<Rigidbody2D>();
	}

    private void Start()
    {
		m_animator = GetComponent<Animator>();
		m_groundSensor = transform.Find("Feet").GetComponent<FeetCollider>();
	}

    void Update()
	{
		//Check if character just landed on the ground
		if (!m_grounded && m_groundSensor.State())
		{
			m_grounded = true;
			m_animator.SetBool("Grounded", m_grounded);
		}

		//Check if character just started falling
		if (m_grounded && !m_groundSensor.State())
		{
			m_grounded = false;
			m_animator.SetBool("Grounded", m_grounded);
		}

		float MoveHor = Input.GetAxisRaw("Horizontal");

		// Check if current move input is larger than 0 and the move direction is equal to the characters facing direction
		if (Mathf.Abs(MoveHor) > Mathf.Epsilon && Mathf.Sign(MoveHor) == m_facingDirection)
			m_moving = true;

		else
			m_moving = false;

		if (MoveHor > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
			m_facingDirection = 1;
		}

		else if (MoveHor < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
			m_facingDirection = -1;
		}

		Vector2 movement = new Vector2(MoveHor * movespeed, playerRB.velocity.y);

		playerRB.velocity = movement;

		m_animator.SetFloat("AirSpeedY", playerRB.velocity.y);

		// -- Animations -- 
		//Jump
		if (Input.GetKeyDown(KeyCode.Space) && m_grounded)
		{
			m_animator.SetTrigger("Jump");
			m_grounded = false;
			m_animator.SetBool("Grounded", m_grounded);
			playerRB.velocity = new Vector2(playerRB.velocity.x, jumpforce);
			m_groundSensor.Disable(0.2f);
		}

		//Run
		else if (m_moving)
			m_animator.SetInteger("AnimState", 1);

		//Idle
		else
			m_animator.SetInteger("AnimState", 0);
	}

	// Animation Events
	// placeholders for now
	void AE_runStop()
	{
	}

	void AE_footstep()
	{
	}

	void AE_Jump()
	{
	}

	void AE_Landing()
	{
	}
}
