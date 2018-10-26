﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {


	public static Manager gm;

	int vidas = 2;
	// Use this for initialization
	void Awake () {
		
		if(gm == null){
			gm = this;
			DontDestroyOnLoad(gameObject);
			
		}

		else{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetVidas(int vida){
		vidas += vida;
	}
	public int GetVidas(){
		return vidas;
	}
}
