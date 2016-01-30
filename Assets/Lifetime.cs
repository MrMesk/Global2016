using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour
{
	public GameObject spawn;
	public float lifetime;
	float timer;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if(timer>= lifetime)
		{
			Instantiate(spawn, this.transform.position, transform.rotation);
			Destroy(this.gameObject);
		}

	}
}
