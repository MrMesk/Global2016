using UnityEngine;
using System.Collections;

public class BombManager : MonoBehaviour
{
	public int countDown = 3;
	public GameObject explosion;
	public LayerMask layer;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Space))
			CountDwn();
	}

	void CountDwn ()
	{
		countDown--;
		if(countDown == 1)
		{
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		if(countDown <= 0)
		{
			Explode();
		}
	}

	void Explode ()
	{
		Collider[] players = Physics.OverlapSphere(transform.position, 2f, layer);
		foreach (Collider c in players)
		{
			if(c.gameObject.tag == "Player")
			{
				c.gameObject.GetComponent<Player>().faireCrever();
			}
		}

		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
}
