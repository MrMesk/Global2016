using UnityEngine;
using System.Collections;

public class Tempo : MonoBehaviour {

    public float tempo = 0.65f;
    public float resize = 1.2f;
    float timer = 0;

    Vector3 sizeInit;

	// Use this for initialization
	void Start () {
        sizeInit = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	    if (timer > tempo * 0.8f )
        {
            transform.localScale = sizeInit * resize;
        }
        if (timer > tempo)
        {
            transform.localScale = sizeInit;
            timer = 0;
        }

        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);

        timer += Time.deltaTime;
	}
}
