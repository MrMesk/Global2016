using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public enum Curses {Invisible, Confused};
	public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
	public float time = 0f;
	public float timerbeat = 1f;

	public LayerMask blockingLayer;         //Layer on which collision will be checked.

	private Vector2 position;
	private Inputs inputs;
    private Hashtable curses;
    private KeyCode keyCodeLeft, keyCodeRight, keyCodeUp, keyCodeDown;
    
	private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
	private float inverseMoveTime;          //Used to make movement more efficient.

	public bool hasmoved = false;
	// Use this for initialization

	public void setKeyCode (KeyCode _keyCodeLeft,
	                 KeyCode _keyCodeRight,
	                 KeyCode _keyCodeUp,
	                 KeyCode _keyCodeDown){

		keyCodeLeft = _keyCodeLeft;
		keyCodeRight = _keyCodeRight;
		keyCodeUp    = _keyCodeUp;
		keyCodeDown  = _keyCodeDown;
	}

	void Start ()
	{
		//Get a component reference to this object's BoxCollider2D
		boxCollider = GetComponent<BoxCollider2D>();
		inputs = GetComponent<Inputs>();
		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent<Rigidbody2D>();

		//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
		inverseMoveTime = 1f / moveTime;
		curses = new Hashtable ();

		Curse curseInvisible = new Curse ();
		curseInvisible.type = Curse.Type.Invisible;
		curses.Add(Curse.Type.Invisible, curseInvisible);

		Curse curseConfused = new Curse ();
		curseConfused.type = Curse.Type.Confused;
		curses.Add(Curse.Type.Confused, curseConfused);
	}

	public Vector2 getPosition()
	{
		return position;
	}

	void Update ()
	{
		time += Time.deltaTime;

		if (time < (timerbeat * 0.8f) || hasmoved) {
			return;
		}

		if (Input.GetKeyDown (keyCodeLeft)) { // left
			inputs.addDirection (Inputs.Direction.Left);
			position += Vector2.left;
			hasmoved = true;
		} else if (Input.GetKeyDown (keyCodeRight)) { // right
			inputs.addDirection (Inputs.Direction.Right);
			position += Vector2.right;
			hasmoved = true;
        } else if (Input.GetKeyDown (keyCodeUp)) { // up
			inputs.addDirection (Inputs.Direction.Up);
			position += Vector2.up;
			hasmoved = true;
		} else if (Input.GetKeyDown (keyCodeDown)) { // down
			inputs.addDirection (Inputs.Direction.Down);
			position += Vector2.down;
			hasmoved = true;
		}

//		if (time >= timerbeat) {
//			time = 0f;
//		}
	}


    void setCurseInvisible () {	
		Curse curseInvisible = (Curse)curses[Curse.Type.Invisible];
	    curseInvisible.timer = 3;
    }
    
    void setCurseConfused () 
	{
	    Curse curseInvisible = (Curse)curses[Curse.Type.Confused];
	    curseInvisible.timer = 2;
    }

    void updateCurses ()
	{
	    foreach (Curse curse in curses){
			if (curse.timer > 0){
			    curse.timer--;
			}
	    }
    }
    


	public bool Move (int xDir, int yDir)
	{
	
		StartCoroutine(SmoothMovement(position));
		hasmoved = false;
		time = 0;
		return true;
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


}
