using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inputs : MonoBehaviour {
	
    // Use this for initialization
    enum Direction {Left, Right, Up, Down};
    private List<Direction> lastInputs = new List<Direction> ();
    private List<Direction>[] dances = new List<Direction>[3];
	
    void Start () {
	// first move is last one
	dances [0] = new List<Direction> {Direction.Left, Direction.Right, Direction.Right};
	dances [1] = new List<Direction> {Direction.Left, Direction.Left};
	dances [2] = new List<Direction> {Direction.Left, Direction.Right, Direction.Left};
	Debug.Log ("start");
	Debug.Log (lastInputs.Count);
    }

    string directionToString (Direction d) {
	string s = "";
	switch(d){
	    case Direction.Down:
		s = "D ";
		break;
	    case Direction.Left:
		s = "L ";
		break;
	    case Direction.Right:
		s = "R ";
		break;
	    case Direction.Up:
		s = "U ";
		break;
	}
	return s;
    }

    // Update is called once per frame
    int findDance (){
		
	bool match = false;
	int danceID;

	for (danceID = 0; danceID < dances.Length; danceID++) {
	    List<Direction> dance = dances [danceID];
	    int i = lastInputs.Count-1;
	    match = true;

	    foreach (Direction direction in dance) {
		if(dance.Count > lastInputs.Count) {
		    match = false;
		    break;
		}

		if (direction != lastInputs [i]) {
		    match = false;
		    break;
		}
		i--;
	    }
	    if (match) {
		Debug.Log ("We have a match " + danceID);
		lastInputs.Clear();
		break;
	    }
	}	
	if (match) {
	    return danceID;
	} else {
	    return -1;
	}
    }

	
    void Update () {
	bool newInput = false;
	if(Input.GetKeyDown(KeyCode.LeftArrow)) {
	    lastInputs.Add (Direction.Left);
	    newInput = true;
	} else if(Input.GetKeyDown(KeyCode.RightArrow)) {
	    lastInputs.Add (Direction.Right);
	    newInput = true;
	} else if(Input.GetKeyDown(KeyCode.UpArrow)) {
	    lastInputs.Add (Direction.Up);
	    newInput = true;
	} else if(Input.GetKeyDown(KeyCode.DownArrow)){
	    lastInputs.Add (Direction.Down);
	    newInput = true;
	}

	if (!newInput) {
	    return;
	}

	if(lastInputs.Count >= 5) {
	    lastInputs.RemoveAt(0);	
	}

	findDance (); // returns the dance id or -1

	// string s = ">";
	// foreach (Direction d in lastInputs) {
	//     s += directionToString(d);
	// }
	// Debug.Log (s);

    }
	
}
