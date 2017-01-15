using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//public static int NofCuckooEggs = 0;

	//public float ExplosionPower;
	//public float ExplosionRadius;
	public static Stack CuckooEggInstances;
	public static int CuckooEggDropForce = -20;

	public float Speed = 0f;
	private float movex = 0f;
	private float movey = 0f;

	private float posX = 0f;
	private float posY = 0f;

	float attackSpeed = 0.1f;
	float cooldown;

	public GameObject prefabEgg;
	public GameObject CuckooEgg;
	private GameObject LastCuckooEgg;

	public GameObject EggShooter;
	private float EggShooterAngle;
	private RectTransform rtEggShooter;
	private float radiusEggShooter;

	//Healthpanel:
	public static int health = 100;
	public static int ammunition = 1000;
	private Image Healthbar;
	public static Text AmmunitionText;


	//Scorepanel:
	public static Text BirdEggsText;
	private static Text ScoreText;
	public static Text GoldenEggsText;
	public static float score = 0;
	public static int DestroyedEggs = 0;
	public static int GoldenEggsFound = 0;

	// GoldenEggs
	private int GoldenEggsInGameStart = 0;
	private int GoldenEggsInGameFound = 0;

	//Audio
	private AudioSource[] AudioSources; 
	public AudioSource CuckooEggDrop;
	private AudioSource CuckooEggExplosion;
	private AudioSource CuckooGameOver;
	private AudioSource BirdEggGoldScore;
	private AudioSource BirdWingflap;
	private AudioSource PlayerDamage;
	private AudioSource PlayerLaughing;

	// Use this for initialization
	void Start () {	

		AudioSources = GetComponents<AudioSource>();

		CuckooEggExplosion =  AudioSources [0];
		CuckooEggDrop = AudioSources [1];
		CuckooGameOver = AudioSources [2];
		BirdEggGoldScore = AudioSources [3];
		BirdWingflap = AudioSources [4];
		PlayerDamage = AudioSources [5];
		PlayerLaughing = AudioSources [6];

		CuckooEggInstances = new Stack ();
		EggShooter = GameObject.Find ("EggShooter");
		//rtEggShooter = (RectTransform)EggShooter.transform;
		//float tileWidth = EggShooter.GetComponent<SpriteRenderer>().bounds.size.x;

		//Vector3 objectSize = Vector3.Scale(transform.localScale, GetComponent().mesh.bounds.size);

		//radiusEggShooter = rtEggShooter.rect.width;
		//radiusEggShooter = tileWidth / 2;
		Debug.Log ("radius:" + radiusEggShooter.ToString ());
		EggShooter.transform.position = this.transform.position;
		//rt = (RectTransform)prefabEgg.transform;
		//playerHeight = rt.rect.height;
		//script = prefabEgg.GetComponent("ExplosionForce2D") as MonoBehaviour;
		Healthbar = GameObject.Find ("Healthbar").GetComponent<Image>();
		ScoreText = GameObject.Find ("Scoretext").GetComponent<Text>();

		BirdEggsText = GameObject.Find ("BirdEggsText").GetComponent<Text>();
		GoldenEggsText = GameObject.Find ("GoldenEggsText").GetComponent<Text> ();
		AmmunitionText = GameObject.Find ("Ammunitiontext").GetComponent<Text> ();

		GoldenEggsInGameStart = CountGoldenEggs ();
		GoldenEggsText.text = "Golden eggs found: " + GoldenEggsInGameFound.ToString() + "/" + GoldenEggsInGameStart.ToString();
		AmmunitionText.text = "Eggs: " + ammunition.ToString ();
	}
		
	// Update is called once per frame
	void Update () {
		movex = Input.GetAxis ("Horizontal");
		movey = Input.GetAxis ("Vertical");

		EggShooter.transform.position = this.transform.position;
		EggShooterAngle = EggShooter.transform.eulerAngles.z;

		posX = EggShooter.transform.position.x  + ((0.85f) * (Mathf.Cos(EggShooterAngle * Mathf.Deg2Rad )));
		//posX = EggShooter.transform.position.x  + (Mathf.Cos(EggShooterAngle) * 0.85f);
		//posX = EggShooter.transform.position.x  + Mathf.Cos(EggShooterAngle);
		//posY = this.transform.position.y - (playerHeight * 2);
		//posY = EggShooter.transform.position.y - (float)1;
		posY = EggShooter.transform.position.y + ((0.85f) * (Mathf.Sin(EggShooterAngle * Mathf.Deg2Rad )));
		//posY = EggShooter.transform.position.y  + (Mathf.Sin(EggShooterAngle) * 0.85f);
		//posY = EggShooter.transform.position.y  + Mathf.Sin(EggShooterAngle);
	
		if (Input.GetKey (KeyCode.A)) {
			movex = -1;

			//playBirdWingFlap ();
			//playSound ();
		} else if (Input.GetKey (KeyCode.D)) {
			movex = 1;
			//playBirdWingFlap ();
			//playSound ();
		} else if (Input.GetKey (KeyCode.W)) {
			movey = 1;
			//playBirdWingFlap ();
			//playSound ();
		} else if (Input.GetKey (KeyCode.S)) {
			movey = -1;
			//playBirdWingFlap ();
			//playSound ();
		} 



		if (Time.time >= cooldown) {
			if (Input.GetKey (KeyCode.Space)) {
				// Lay egg:
				//NofCuckooEggs = NofCuckooEggs + 1;
				//for (int i = 0; i <= 1; i++) {
				if (ammunition > 0) {
					CuckooEgg = (GameObject)Instantiate (prefabEgg, new Vector3 (posX, posY, 0f), Quaternion.identity);
					CuckooEgg.GetComponent<Rigidbody2D> ().velocity = (EggShooter.transform.up * CuckooEggDropForce);
					CuckooEggInstances.Push (CuckooEgg);
					//CuckooEgg.GetComponent<Rigidbody2D> ().velocity = (EggShooterAngle * CuckooEggDropForce);


					//CuckooEgg.GetComponent<Rigidbody2D> ().AddForce(this.transform.up * -50);
					//Physics2D.IgnoreCollision(CuckooEgg.GetComponent<Collider2D>(),  this.GetComponent<Collider2D>());

					Debug.Log ("Angle up:" + this.transform.up.ToString ());
					//CuckooEgg.transform.

					//CuckooEgg.name = "CuckooEgg" + NofCuckooEggs.ToString ();
					cooldown = Time.time + attackSpeed;
					//CuckooEggsText.text = "Cuckoo eggs:" + CuckooEggInstances.Count.ToString();
					playCuckooEggDrop ();
					ammunition = ammunition - 1;
					AmmunitionText.text = "Eggs: " + ammunition.ToString ();
				}
				//}
				//script.
				//Instantiate(egg, new Vector3(posX, posY, 0), Quaternion.identity);
				//AddSprite(eggTexture, movex, movey);
			}
		}
		//Rotate to the right:
		if (Input.GetKey (KeyCode.Z)) {

			EggShooter.transform.Rotate(0, 0, 5);
			//posX = 0f;
		} 

		//Rotate to the left:
		if (Input.GetKey (KeyCode.X)) {
			
			EggShooter.transform.Rotate(0, 0, -5);
		} 


		if (Input.GetKey (KeyCode.E)) {
			LastCuckooEgg = (GameObject)CuckooEggInstances.Peek (); 

			if (LastCuckooEgg != null) {
				Vector3 LastCuckooEggPosition = LastCuckooEgg.transform.position;
				float LastCuckooEggPositionX = LastCuckooEggPosition.x;
				float LastCuckooEggPositionY = LastCuckooEggPosition.y - (float)1.2;
				Vector2 ExplosionPosition = new Vector2 (LastCuckooEggPositionX, LastCuckooEggPositionY);

				Debug.Log (string.Format (LastCuckooEggPositionX + " " + LastCuckooEggPositionY));
				Rigidbody2DExt.CastAddExplosionForce(500f, ExplosionPosition, 8f, 0f, ForceMode2D.Impulse);

				playCuckooEggExplosion ();
				Destroy (LastCuckooEgg, (float)CuckooEggExplosion.clip.length);
				CuckooEggInstances.Pop ();
		
			}

		}
	
		FixedUpdate ();
	}
		
	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2 (movex * Speed, movey * Speed);
		//this.transform.position = new Vector3 (movex * Speed, movey * Speed, 0f);
	}


	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.name.Contains("BirdEgg")) {
			//this.transform.Rotate(Vector3.forward * -90)
			HealthUpdate((int)(other.rigidbody.mass));
		} 
		else if (other.gameObject.name.Contains("CuckooEggGold")) {
			ScoreUpdate(other.rigidbody.mass);
			GoldenEggUpdate ();
			HealthUpdate (-5);
			playBirdEggGoldScore();
			playPlayerLaughing ();
			Destroy (other.gameObject, (float)BirdEggGoldScore.clip.length);

			GoldenEggsInGameFound = GoldenEggsInGameStart - CountGoldenEggs();
		}

		Debug.Log ((float)((double)PlayerController.health / 100.0f));
		Debug.Log ((float)(PlayerController.health));

		if (health <= 0) {
			playCuckooGameOver ();
			//this.transform.r
			GameOver ();
		}
	}
		

	public void HealthUpdate(int damage)
	{

		Debug.Log ("damage: " + damage);
		if (damage > 0) {
		playPlayerDamage ();
		}

		health = health - damage;
		Healthbar.fillAmount = (float)((double)PlayerController.health / 100);


	}
		

	public static void ScoreUpdate(float newscore)
	{

		score = score + newscore;
		ScoreText.text = "Score: " + score.ToString ();
	}

	public void GoldenEggUpdate() {
		//GoldenEggsFound = GoldenEggsFound + 1;
		GoldenEggsText.text = "Golden eggs found: " + GoldenEggsInGameFound.ToString() + "/" + GoldenEggsInGameStart.ToString();

	}


	public int CountGoldenEggs() {

		int GoldenEggsInGame = 0;
		foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
		{
			if (gameObj.name.Contains("CuckooEggGold")) {
				GoldenEggsInGame = GoldenEggsInGame + 1;
			}		
		}
		return GoldenEggsInGame;
	}




	public void GameOver() {
		GameObject Canvasobject = GameObject.Find ("Canvas");
		GameObject PanelGameover = Canvasobject.transform.FindChild ("PanelGameover").gameObject;
		PanelGameover.SetActive (true);
		Time.timeScale = 0;
	}
		

	public void playCuckooEggDrop(){
		//audioSource.clip = audioClip;
		//if (!CuckooEggDrop.isPlaying) {
			CuckooEggDrop.PlayOneShot(CuckooEggDrop.clip);
		//}
	}


	public void playCuckooEggExplosion(){
		//audioSource.clip = audioClip;
		if (!CuckooEggExplosion.isPlaying) {
			CuckooEggExplosion.PlayOneShot(CuckooEggExplosion.clip);
		}
	}


	public void playCuckooGameOver(){
		//audioSource.clip = audioClip;
		if (!CuckooGameOver.isPlaying) {
			CuckooGameOver.PlayOneShot(CuckooGameOver.clip);
		}
	}

	public void playBirdEggGoldScore(){
		//audioSource.clip = audioClip;
		if (!BirdEggGoldScore.isPlaying) {
			BirdEggGoldScore.PlayOneShot(BirdEggGoldScore.clip);
		}
	}

	public void playBirdWingFlap(){
		//audioSource.clip = audioClip;
		if (!BirdWingflap.isPlaying) {
			BirdWingflap.PlayOneShot(BirdWingflap.clip);
		}
	}

	public void playPlayerDamage() {
		if (!PlayerDamage.isPlaying) {
			PlayerDamage.PlayOneShot(PlayerDamage.clip);
		}
	}

	public void playPlayerLaughing() {
		if (!PlayerLaughing.isPlaying) {
			PlayerLaughing.PlayOneShot(PlayerLaughing.clip);
		}
	}
		
}
