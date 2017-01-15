using UnityEngine;
using System.Collections;

public class CuckooEggController : MonoBehaviour {


	private AudioSource[] AudioSources; 
	public AudioSource Egghit;
	// Use this for initialization
	void Start () {
		AudioSources = GetComponents<AudioSource>();
		Egghit =  AudioSources [0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.name.Contains("BirdEgg")) {
			PlayerController.ScoreUpdate (other.rigidbody.mass);

			PlayerController.DestroyedEggs = PlayerController.DestroyedEggs + 1;
			PlayerController.BirdEggsText.text = "Destroyed eggs: " + PlayerController.DestroyedEggs.ToString ();

			PlayerController.ammunition = PlayerController.ammunition + 1;

			Destroy (other.gameObject);

			playEggHit ();
			Destroy (this.gameObject, (float)Egghit.clip.length);

			//Destroy (this.gameObject);
		}
			
	}


	public void playEggHit(){
		//audioSource.clip = audioClip;
		//if (!Egghit.isPlaying) {
			Egghit.PlayOneShot(Egghit.clip);
		//}
	}


}
