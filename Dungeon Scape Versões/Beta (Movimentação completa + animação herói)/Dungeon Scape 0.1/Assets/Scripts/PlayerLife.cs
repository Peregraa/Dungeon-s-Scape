using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PerdeVida(){
		// anim.SetTrigger("Morreu"); //Chamar no Animator a animação do Personagem morrendo como MORREU
		Manager.gm.SetVidas(-1);
	}

	public void Reset(){
		if(Manager.gm.GetVidas() >= 0){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
