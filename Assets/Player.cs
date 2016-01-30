﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
	public LayerMask blockingLayer;         //Layer on which collision will be checked.


	private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
	private float inverseMoveTime;          //Used to make movement more efficient.

	public float timerbeat;
	float timer;
	bool hasmoved = false;
											// Use this for initialization
	void Start ()
	{
		//Get a component reference to this object's BoxCollider2D
		boxCollider = GetComponent<BoxCollider2D>();

		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent<Rigidbody2D>();

		//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
		inverseMoveTime = 1f / moveTime;
		timer = 0f;
	}

	void Update()
	{
		timer += Time.deltaTime;

		if(timer >= (timerbeat * 0.8f) && !hasmoved )
		{
			int horizontal = 0;
			int vertical = 0;

			horizontal = (int)Input.GetAxisRaw("Horizontal");
			vertical = (int)Input.GetAxisRaw("Horizontal");

			if (horizontal != 0)
			{
				vertical = 0;
			}

			if (vertical != 0 || horizontal != 0)
			{
				hasmoved = true;
				AttemptMove(horizontal, vertical);
			}
		}
		if(timer > timerbeat)
		{
			//DamagePlayer
		}
		
	}

	protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
	{
		//Store start position to move from, based on objects current transform position.
		Vector2 start = transform.position;

		// Calculate end position based on the direction parameters passed in when calling Move.
		Vector2 end = start + new Vector2(xDir, yDir) * 0.8f;

		//Disable the boxCollider so that linecast doesn't hit this object's own collider.
		boxCollider.enabled = false;

		//Cast a line from start point to end point checking collision on blockingLayer.
		hit = Physics2D.Linecast(start, end, blockingLayer);

		//Re-enable boxCollider after linecast
		boxCollider.enabled = true;

		//Check if anything was hit
		if (hit.transform == null)
		{
			//If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
			StartCoroutine(SmoothMovement(end));

			//Return true to say that Move was successful
			return true;
		}

		//If something was hit, return false, Move was unsuccesful.
		return false;
	}

	//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
		//Square magnitude is used instead of magnitude because it's computationally cheaper.
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		//While that distance is greater than a very small amount (Epsilon, almost zero):
		while (sqrRemainingDistance > float.Epsilon)
		{
			//Find a new position proportionally closer to the end, based on the moveTime
			Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

			//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
			rb2D.MovePosition(newPostion);

			//Recalculate the remaining distance after moving.
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			//Return and loop until sqrRemainingDistance is close enough to zero to end the function
			yield return null;
		}
	}

	//The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
	//AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
	void AttemptMove(int xDir, int yDir)
	{
		//Hit will store whatever our linecast hits when Move is called.
		RaycastHit2D hit;
		bool isHit = false;
		//Set canMove to true if Move was successful, false if failed.
		bool canMove = Move(xDir, yDir, out hit);

		//Check if nothing was hit by linecast
		if (hit.transform == null)
			//If nothing was hit, return and don't execute further code.
			return;
		else
			isHit = true;

		//Get a component reference to the component of type T attached to the object that was hit
		

		//If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
		if (!canMove && isHit)

			//Call the OnCantMove function and pass it hitComponent as a parameter.
			return;
	}

}