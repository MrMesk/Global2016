using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inputs : MonoBehaviour {
	
    // Use this for initialization
    public enum Direction {Left, Right, Up, Down, None};
    private List<Direction> lastInputs = new List<Direction> ();
    private List<Direction>[] dances = new List<Direction>[5];
	public enum DanceID {Bombe = 0, Wave = 1, Hide = 2, Confuse = 3, Dig = 4, None};
    void Start () 
	{
	// first move is last one
		dances [(int)DanceID.Bombe] = new List<Direction> {Direction.Down, Direction.Left, Direction.Right, Direction.Down};
		dances [(int)DanceID.Wave] = new List<Direction> {Direction.Right, Direction.Right, Direction.Up, Direction.Left, Direction.Left};
		dances [(int)DanceID.Hide] = new List<Direction> {Direction.Right, Direction.Down, Direction.Down, Direction.Right, Direction.Up};
		dances [(int)DanceID.Confuse] = new List<Direction> {Direction.Up, Direction.Left, Direction.Down, Direction.Right};
		dances [(int)DanceID.Dig] = new List<Direction> {Direction.Left, Direction.Up, Direction.Down, Direction.Right, Direction.Down};

	Debug.Log ("start");
	Debug.Log (lastInputs.Count);
    }


    // Update is called once per frame
    public DanceID findDance ()
	{
		bool match = false;
		int danceID;

		for (danceID = 0; danceID < dances.Length; danceID++)
		{
			List<Direction> dance = dances[danceID];
			int i = lastInputs.Count - 1;
			match = true;

			Debug.Log ("DanceID " + danceID);
			foreach (Direction direction in dance)
			{
				if (dance.Count > lastInputs.Count)
				{
					match = false;
					break;
				}

				Debug.Log ("danceID " + danceID + "> " + direction + " == " + lastInputs[i]);

				if (direction != lastInputs[i])
				{
					match = false;
					break;
				}
				i--;
			}
			if (match)
			{
				Debug.Log("We have a match " + danceID);
				Debug.Log("We have a match " + danceID);
				Debug.Log("We have a match " + danceID);
				Debug.Log("We have a match " + danceID);
				Debug.Log("We have a match " + danceID);
				Debug.Log("We have a match " + danceID);
				lastInputs.Clear();
				break;
			}
		}
		if (match)
		{
			return (DanceID)danceID;
		}
		else
		{
			return DanceID.None;
		}
	}


	public DanceID addDirection (Direction d)
	{
		lastInputs.Add(d);
		Debug.Log ("addDirection");
		if (lastInputs.Count >= 6)
		{
			lastInputs.RemoveAt(0);
		}
		string s = "";
		foreach(Direction dd in lastInputs){
			s += dd;
		}Debug.Log (s);

		return findDance();
	}
	
}
