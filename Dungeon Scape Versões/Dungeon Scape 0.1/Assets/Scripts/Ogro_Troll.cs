using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ogro_Troll : MonoBehaviour {
    private bool eLadoDireito;
    Animator animator;
    [SerializeField]
    private float velocidade = 0.1f;
    //public Transform transform;
     
  

    // Use this for initialization
    void Start()
    {
        eLadoDireito = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Limite")
        {
            MudarDirecao();
        }

       
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("ataqueInimigo");       
            //voltarAndar();
            if(velocidade < 7f){
                velocidade += 3f;
            }
            

            MudarDirecao();
        }

         /* if (col.gameObject.CompareTag("Player"))
        {
            
        } */
    }

    public void voltarAndar(){

        animator.SetTrigger("voltaAndar");
        
    }


    private void Mover()
    {
        transform.Translate(PegarDirecao() * (velocidade * Time.deltaTime));
        //animator.SetBool("Andando", true);
    }

    //Métodos

    private Vector2 PegarDirecao()
    {
        return eLadoDireito ? Vector2.right : Vector2.left;
    }

    private void MudarDirecao()
    {
        eLadoDireito = !eLadoDireito;
        this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
