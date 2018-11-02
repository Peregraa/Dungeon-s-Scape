using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirCamera : MonoBehaviour {


    private Vector2 velocity;


    //Onde a smoothTime camera vai se movimentar
    public float smoothTimeY;
    public float smoothTimeX;


    public GameObject player;

	void Start () {
        //Acha o came objetc com a tag ''Player'', que é o jogador em si
        player = GameObject.FindGameObjectWithTag("Player");

	}
    void FixedUpdate()
    {
        //Transfere a posição x e y para o jogador
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
