using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerControll : MonoBehaviour {

	public float speed=6.0F;
	public float speed2=10.0F;
	private Rigidbody rb;
	private bool Jumping = false;
	private int score = 0;
	public Text scoreText;
	private Vector3 originalPosition ;
	private Quaternion originalRotation;
	public Text timerText;
	private float secondsCount;
	private bool isOverGame = false;
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		scoreText.text = "0";
		originalPosition = transform.position;
		originalRotation = transform.rotation;
	}

	void FixedUpdate ()
	{
		if (!isOverGame) {
			
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			//Debug.Log ("Hrizontal " + moveHorizontal);
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			if (Input.GetKeyDown (KeyCode.Space)) {
				rb.velocity = Vector3.up * speed2;
			}

			rb.AddForce (movement * speed);

			if (transform.position.y < 0) {
				Debug.Log ("Game over");
				transform.position = originalPosition;
				transform.rotation = originalRotation;
				if (rb != null) {
					rb.velocity = Vector3.zero;
					rb.angularVelocity = Vector3.zero;
				}
				//set timer UI
				timerText.text = "Game over " + (int)secondsCount + " s";
				isOverGame = true;
			} else {
				if (score < 210) {
					UpdateTimerUI ();
				}
			}

			if (score == 210) {
				timerText.text = "Complete with " + (int)secondsCount + " s";
			}
		} else {
			if (Input.GetKeyDown (KeyCode.Space)) {
				isOverGame = false;
				secondsCount = 0.0f;
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}

	//call this on update
	public void UpdateTimerUI(){
		//set timer UI
		secondsCount += Time.deltaTime;
		timerText.text = "Timer " + (int)secondsCount + " s";
	 
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);
			score+=10;
			scoreText.text = score.ToString ();
		}
	}
}