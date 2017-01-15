using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//public GameObject player;

	private AudioSource[] AudioSources; 
	public AudioSource Levelmusic;
	public AudioSource Levelambience;

	private Vector3 playerpos;
	private Transform player;

	private float rightBound;
	private float leftBound;
	private float topBound;
	private float bottomBound;

	private SpriteRenderer spriteBounds;

	private float minFov = 15f;
	private float maxFov = 90f;
	private float sensitivity = 10f;

	private float vertExtent;
	private float horzExtent;

	// Use this for initialization
	void Start () {
		AudioSources = GetComponents<AudioSource>();

		Levelmusic = AudioSources [0];

		if (AudioSources.Length > 1) {
			Levelambience = AudioSources [1];
			playLevelambience ();
		} 

		playLevelmusic ();
		//if (Application.loadedLevelName = 1) {

		//}
		//this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

		//target = 
		FocusCamera();

	}
	
	// Update is called once per frame
	void Update () {

//		if (Input.GetAxis("Mouse ScrollWheel") > 0)
//		{
//			Camera.main.orthographicSize--;
//			FocusCamera ();
//		}
//
//		if (Input.GetAxis("Mouse ScrollWheel") < 0)
//		{
//			if (Camera.main.orthographicSize <= (spriteBounds.sprite.bounds.max.x))
//			{
//				Camera.main.orthographicSize++;
//			FocusCamera ();
//			}
//		}



		playerpos = player.transform.position;
		playerpos.x = Mathf.Clamp(playerpos.x, leftBound, rightBound);
		playerpos.y = Mathf.Clamp(playerpos.y, bottomBound, topBound);
		playerpos.z = transform.position.z;
		transform.position = playerpos;

		if (!Levelmusic.isPlaying) {
			playLevelmusic ();
		}


		if (AudioSources.Length > 1) {
			if (!Levelambience.isPlaying) {
				playLevelambience ();
			}
		}

//		float fov = Camera.main.fieldOfView;
//		fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
//		fov = Mathf.Clamp(fov, minFov, maxFov);
//		Debug.Log ("ScrollWheel: " + fov.ToString ());
//		Camera.main.fieldOfView = fov;

	
	
	}

	// Keep camera within the background boundaries:
	public void FocusCamera()
	{
		vertExtent = Camera.main.orthographicSize;  
		horzExtent = (vertExtent * Screen.width) / Screen.height;

		spriteBounds = GameObject.Find("Background").GetComponentInChildren<SpriteRenderer>();
		player = GameObject.Find ("Player").transform;

		leftBound = spriteBounds.bounds.min.x + horzExtent;
		rightBound = spriteBounds.bounds.max.x - horzExtent;
		bottomBound = spriteBounds.bounds.min.y + vertExtent;
		topBound = spriteBounds.bounds.max.y - vertExtent;    

	}



	public void playLevelmusic(){
		//audioSource.clip = audioClip;
		if (!Levelmusic.isPlaying) {
			Levelmusic.PlayOneShot(Levelmusic.clip);
		}
	}

	public void playLevelambience(){
		//audioSource.clip = audioClip;
		if (!Levelambience.isPlaying) {
			Levelambience.PlayOneShot(Levelambience.clip);
		}
	}

}
