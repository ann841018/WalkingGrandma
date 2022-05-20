using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
	public float countTimeStart,countTimeEnd;

	float apparent=1;//透明度
	int count;

    // Use this for initialization
    void Start(){gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);}

    // Update is called once per frame
    void FixedUpdate()
    {
		count = count + 1;
		if (count >= countTimeStart && count <= countTimeEnd) {apparentout();}
		else if (count > countTimeEnd)
        {
            apparent = 0;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, apparent);
        }
		if (count >= 6000)
        {
            apparent = 1;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, apparent);
			count = 0;
        }
    }

    public void apparentout()
    {
		apparent = apparent - 1/(countTimeEnd-countTimeStart);
		if (apparent <= 1/(countTimeEnd-countTimeStart)) { apparent = 0; }
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, apparent);
    }
}