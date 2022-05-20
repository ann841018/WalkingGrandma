using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move: MonoBehaviour 
{
	public float Speed,Destory,Create,positionY;
	int RandomNumber;

	GameObject ObjectClone;
	public GameObject Object1, Object2,Object3;

	// Use this for initialization
	void Start () {RandomNumber = (int)Random.Range (1, 4);}

	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.Translate(Speed, 0f, 0f); 
		if (transform.position.x <= Destory) 
		{
			Vector2	position = new Vector2 (Create, positionY);
			if (RandomNumber == 1) 
			{
				ObjectClone = (GameObject)Instantiate (Object1, position, transform.rotation);
			} else if (RandomNumber == 2) 
			{
				ObjectClone = (GameObject)Instantiate (Object2, position, transform.rotation);
			} else if (RandomNumber == 3) 
			{
				ObjectClone = (GameObject)Instantiate (Object3, position, transform.rotation);
			} else {}
			Destroy (gameObject);
		}
	}
}
