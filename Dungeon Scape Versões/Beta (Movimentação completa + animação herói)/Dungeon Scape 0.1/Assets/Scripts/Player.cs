using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //Var para realizar validação do pulo 
    bool isJumping = false;
    bool isOnFloor = false;
    private int maxJump;

    //Var para velocidade do herói e força do pulo
    public float jumpForce = 550f;
    public float speed = 5f;
    public float radius = 0.35f;

    //Var validando o chão
    public Transform groundCheck;
    public LayerMask whatIsGround;
    
    Rigidbody2D body;
    SpriteRenderer sprite;
    Animator anime;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se o herói está no chão
        isOnFloor = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);
        
        //Validando pulo e duplo pulo
        if (Input.GetButtonDown("Jump") && maxJump > 0)
            
            isJumping = true;

        //Herói toca no chão e ganha pulo
        if (isOnFloor) {
            maxJump = 1;
        }

        //Carregando a animação ao iniciar o jogo
        PlayerAnimation();

    }

    void FixedUpdate()
    {
        //MOVE - recebe quando as teclas <- -> são pressionadas
        float move = Input.GetAxis("Horizontal");
        //Velocidade e movimento do herói tanto no ar quanto no solo 
        body.velocity = new Vector2(move * speed, body.velocity.y);

        if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            Flip();
        }

        //Ação de pulo
        if (isJumping)
        {
            maxJump--;
            body.velocity = new Vector2(body.velocity.x, 0f);
            body.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip()
    {
        sprite.flipX = !sprite.flipX;
    }

    //Aumentando o alcance do groundCheck para evitar erros no pulo.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    //Validando animação do herói
    void PlayerAnimation()
    {
        anime.SetFloat("VelX", Mathf.Abs (body.velocity.x));
        anime.SetFloat("VelY", Mathf.Abs (body.velocity.y));
    }
}