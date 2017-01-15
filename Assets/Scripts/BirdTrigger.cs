using UnityEngine;
using System.Collections;

public class BirdTrigger : MonoBehaviour {

	// Use this for initialization
	public GameObject BirdEggRed;
	public int force = 500;
	public int health = 25;

	private GameObject BirdEggInstance;


	private float posX;
	private float posY;

	public float Eagledelta = 300.0f;  // Amount to move left and right from the start point
	public float Eaglespeed = 2.0f; 
	private Vector3 EaglestartPos;

	private float EaglemaxValue = 80f; // or whatever you want the max value to be
	private float EagleminValue = 40f; // or whatever you want the min value to be
	private float EaglecurrentValue; // or wherever you want to start
	private float Eagledirection = 1f; 
	private float Eagleduration = 1f;
	private float Eagletimer = 0f;


	private AudioSource[] AudioSources; 
	private AudioSource BirdEggDrop;
	private AudioSource BirdHit;
	private AudioSource BirdExplode;
	private AudioSource BirdAngry;

	private void Start () {
		AudioSources = GetComponents<AudioSource>();

	
		BirdEggDrop = AudioSources [0];
		BirdHit = AudioSources [1];
		BirdExplode = AudioSources [2];
		BirdAngry = AudioSources [3];

		if (this.name == "Owlbird") {
			force = 2000;
			health = 200;
		} else if (this.name == "Eagle") {
			EaglecurrentValue = this.transform.position.x;
		}
	
	}

	// Update is called once per frame
	private void Update () {
		if (this.name == "Eagle") {
			EaglecurrentValue += Time.deltaTime * 10 * Eagledirection; // or however you are incrementing the position
			if (EaglecurrentValue >= EaglemaxValue) {
				Eagledirection *= -1;
				EaglecurrentValue = EaglemaxValue;

				this.transform.localRotation = Quaternion.Euler (0, 180, 0);

			} else if (EaglecurrentValue <= EagleminValue) {
				Eagledirection *= -1; 
				EaglecurrentValue = EagleminValue;
				this.transform.localRotation = Quaternion.Euler (0, 0, 0);
			}
			transform.position = new Vector3 (EaglecurrentValue, this.transform.position.y, this.transform.position.z);

			Eagletimer += Time.deltaTime;
			if (Eagletimer >= Eagleduration) {
				Eagletimer = 0f;
				BirdEggInstance = (GameObject)Instantiate (BirdEggRed, this.transform.position, Quaternion.identity);
				//BirdEggInstance.GetComponent<Rigidbody2D> ().AddForce (new Vector3(0,10,0) * force);
				playBirdEggDrop ();

			}
		}

	}


	// Fired when the player is within a certain range from the bird:
	private void OnTriggerEnter2D(Collider2D other) {
		posX = this.transform.position.x;
		posY = this.transform.position.y + (float)1;

		if (this.name == "Owlbird") {
			posY = this.transform.position.y + (float)4;
		}  else if (this.name == "BirdHouse") {
			posY = this.transform.position.y - (float)4;
			//posX = this.transform.position.x + (float)2;
		}


		if(other.gameObject.name == "Player")
		{
			for (int i = 0; i <= 2; i++) {
				BirdEggInstance = (GameObject)Instantiate (BirdEggRed, new Vector3 (posX, posY, 0f), Quaternion.identity);
				Vector3 VectorThree = other.transform.position - BirdEggInstance.transform.position;
				VectorThree = other.transform.InverseTransformDirection (VectorThree);

				BirdEggInstance.GetComponent<Rigidbody2D> ().AddForce (VectorThree * force);
				playBirdEggDrop ();
				playBirdAngry ();
			}
			
		}

	}
		
	private void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.name.Contains("Egg")) {
			
			if (this.tag.ToString() != other.gameObject.tag.ToString()) {
				if (this.name == "BirdHouse" || this.name.Contains ("bird")) {
					playBirdAngry ();
					Debug.Log ("BirdHouse OMG ANGRY!!!");

					playBirdHit ();
					//this.transform.Rotate(Vector3.forward * -90);
					health = health - (int)(other.rigidbody.mass);
					Destroy (other.gameObject);
					if (health <= 0) {
						playBirdExplode ();
						Debug.Log ("Destroyed");
						Destroy (this.gameObject, (float)BirdExplode.clip.length);
					}
					Debug.Log ("Health: " + health.ToString ());
					//posX = this.transform.position.x + (float)2;
				}
			}

		

		}
	}

	public void playBirdEggDrop(){
		//audioSource.clip = audioClip;
		if (!BirdEggDrop.isPlaying) {
			BirdEggDrop.PlayOneShot(BirdEggDrop.clip);
		}
	}

	public void playBirdHit(){
		//audioSource.clip = audioClip;
		if (!BirdHit.isPlaying) {
			BirdHit.PlayOneShot(BirdHit.clip);
		}
	}

	public void playBirdExplode(){
		//audioSource.clip = audioClip;
		if (!BirdExplode.isPlaying) {
			BirdExplode.PlayOneShot(BirdExplode.clip);
		}
	}

	public void playBirdAngry(){
		//audioSource.clip = audioClip;
		if (!BirdAngry.isPlaying) {
			BirdAngry.PlayOneShot(BirdAngry.clip);
		}
	}


}
