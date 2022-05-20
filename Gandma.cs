using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gandma : MonoBehaviour 
{
	public AudioSource jumpSound,eatCoin,die;

	Rigidbody2D myRigidbody;
	Animator myAnim;

	public float Speed = 1.0f;
	public float jumpForce;

	public bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	private bool KeyboJump = false;

	public static bool gameOver;

	// Use this for initialization
	void Start () 
	{
		myRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator> ();

		//change the keybo mode to coordinate
		StartCoroutine(changeMode());
		ConnectController.panGestureAction = null;
		ConnectController.swipeGestureAction = null;
		ConnectController.connectReturn = null;
		ConnectController.connectError = null;
		groundRadius = 0.4f;
		jumpForce = 560.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		if (grounded && Input.anyKeyDown) 
		{
			myRigidbody.AddForce (new Vector2 (0.0f, jumpForce));
			myAnim.SetBool ("Jump",true);
			jumpSound.Play();
		}else myAnim.SetBool ("Jump",false);

		if (KeyboJump) 
		{
			myAnim.SetBool ("Jump",true);
			KeyboJump = false;
		}

		if(transform.position.y <= -4f){myAnim.SetBool ("Fall", true);}
		else myAnim.SetBool("Fall",false);
		if (transform.position.x < -3) 
		{
			transform.Translate(Vector2.right*Time.deltaTime*0.2f);
		}
	}

	void FixedUpdate()
	{
		if (transform.position.y <= -8 || transform.position.x <= -9) {die.Play ();gameOver = true;} else gameOver = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Money") 
		{
			eatCoin.Play ();
			Score.money = Score.money + 1;
			PlayerPrefs.SetInt ("Money", Score.money);
			PlayerPrefs.Save ();
			Destroy (other.gameObject);
		}
	}

	IEnumerator changeMode()
	{
		yield return new WaitForSeconds(0.0f);
		KeyboConnectInterface.Log ("changeMode");
		//change the keybo mode to coordinate
		KeyboConnectInterface.ChangeKeyboMode (KeyboConnectInterface.KeyboType.Coordinate, (error) => 
			{
				KeyboConnectInterface.Log (error);
			});

		OnTap ();
	}


	public void OnTap()
	{
		KeyboConnectInterface.Log("OnTap");
		KeyboConnectInterface.SwitchTapGestureRecognizer (true, (state,type,x,y) => 
			{
				KeyboConnectInterface.Log("Tap In");

				if(state == (int)KeyboConnectInterface.TouchState.Begin && type != (int)KeyboConnectInterface.Taptype.Twice){
					KeyboConnectInterface.Log("Tap Success!");
					if (grounded) {
						myRigidbody.AddForce (new Vector2 (0.0f, jumpForce));
						KeyboJump = true;
						jumpSound.Play();
						KeyboConnectInterface.Log("Jump!");
					} else 
						KeyboJump = true;
				}
			});
	}
}
