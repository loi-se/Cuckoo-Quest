using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.UI.Image;


public class UIController : MonoBehaviour {
	Image Healthbar;

	// Use this for initialization
	void Start () {
		Healthbar = GameObject.Find ("Healthbar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log((float)(PlayerController.health / 100));
		//Healthbar.fillAmount = (float)(PlayerController.health / 100);
	}
}

