<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimgos_voadores : MonoBehaviour {

    private bool colidde = false;

    private float move = 2;
    void Start () {
		
	}

	void Update () {
        //Coloca a uma velocidade de 2 no inimigo para ele se mover 
        GetComponent<Rigidbody2D>().velocity = new Vector2(move, GetComponent<Rigidbody2D>().velocity.y);
        //se ele colidir com outro elemento que possui Collider ele chama o Flip();
        if (colidde)
        {
            Flip();
        }
    }

    private void Flip()
    {
        //multiplica 2 por -1, vira -2, então ele inverte a direção do inimigo
        move *= -1;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        colidde = false;
    }

    // o inimigo só vai dar flip se colidir com os elementos tag ESPETO E BLOCO
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bloco_cenario"))
        {
            colidde = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bloco_cenario"))
        {
            colidde = false;
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimgos_voadores : MonoBehaviour {

    private bool colidde = false;

    private float move = 2;
    void Start () {
		
	}

	void Update () {
        //Coloca a uma velocidade de 2 no inimigo para ele se mover 
        GetComponent<Rigidbody2D>().velocity = new Vector2(move, GetComponent<Rigidbody2D>().velocity.y);
        //se ele colidir com outro elemento que possui Collider ele chama o Flip();
        if (colidde)
        {
            Flip();
        }
    }

    private void Flip()
    {
        //multiplica 2 por -1, vira -2, então ele inverte a direção do inimigo
        move *= -1;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        colidde = false;
    }

    // o inimigo só vai dar flip se colidir com os elementos tag ESPETO E BLOCO
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bloco_cenario"))
        {
            colidde = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bloco_cenario"))
        {
            colidde = false;
        }
    }
}
>>>>>>> master
