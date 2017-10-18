using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb2d;
	public float speed;
	private int count;
	private float forceSpeed = 100f;

	public Text CountText;
	public Text WinText;
	public Text Timer;

	private float timer  = 30;
	void Start()
	{
		WinText.text = "";
		rb2d = GetComponent<Rigidbody2D> ();
		count = 0;
		SetCount ();
	}

	void FixedUpdate () 
	{
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");
		//Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		Vector2 mobileMovement = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
		bool isBooting = CrossPlatformInputManager.GetButton ("Boost");
		Debug.Log ((isBooting?forceSpeed:1));
		rb2d.AddForce (mobileMovement*speed*(isBooting?forceSpeed:1f));

		TimerCountDown ();
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("PickUP")){
			//other.gameObject.SetActive(false);
			Destroy(other.gameObject);
			count++;
			SetCount ();
		}
	}

	void SetCount()
	{
		CountText.text = "Count: " + count.ToString ();
		if(count>=16)
		{
			WinText.text = "You win!!";
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

	}

	void TimerCountDown()
	{
		timer -= Time.deltaTime;
		Timer.text ="Time Left: "+ (int)timer;

		if (timer <= 0) {
			WinText.text = "You lose!!";
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
