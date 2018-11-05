using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigos : MonoBehaviour
{
    /* Variaveis para usar se precisar do inimigo pular
    private float time = 0.0f;

    public float timer;
    public float force;
    */
    private bool colidde = false;

    private float move = 2;


    void Start()
    {

    }


    void Update()
    {
        //Esse cógido até o "FIM". é para usar se precisar do inimgo pular
        //GetComponent<Rigidbody2D>().velocity = new Vector2(move, GetComponent<Rigidbody2D>().velocity.x);

        /*time += Time.deltaTime;
        if (time >= timer)
        {
            time = 0f;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }*/
        //FIM

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
        if (col.gameObject.CompareTag("Espeto"))
        {
            colidde = true;
        }
        if (col.gameObject.CompareTag("Bloco"))
        {
            colidde = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Espeto"))
        {
            colidde = false;
        }
        if (col.gameObject.CompareTag("Bloco"))
        {
            colidde = false;
        }
    }

}
