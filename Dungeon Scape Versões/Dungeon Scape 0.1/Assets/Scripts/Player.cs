using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    bool keysDisabled = false;
    //Interface
    public Text TextLives;
    //vida
    public int vida = 3;
    //Checkpoint
    public GameObject lastCheckPoint;
    public int contadorColisaoInimigo = 0;
    bool vivo = true;

    //Personagem - Herói
    
    public float velocidade = 5f;
    Rigidbody2D corpo;
    //Var para trabalhar o FLIP
    SpriteRenderer sprite;

    //Var para validar o chão e a força do pulo
    public Transform groundCheck;
    bool isOnFloor = false;
    public LayerMask whatIsGround;
    public float puloForca = 600f;

    //Var Pulo
    bool estaPulo = false;
    private int maxPulo;

    //Agachar
    bool abaixar = false;

    //Var para validar chão
    public float radius = 0.35f;
    
    //Var de ataque
    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask layerEnemy;
    float timeNextAttack;

    //Var animação
    Animator animacao;

    // Use this for initialization
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animacao = GetComponent<Animator>();

        TextLives.text = vida.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Iniciando animações
        AnimacaoHeroi();
       
        //Validando toque no chão
        isOnFloor = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);
        animacao.SetBool("CuDoAndre", isOnFloor);

        if (keysDisabled == false)
        {
            //Validando Pulo e duplo pulo
            if (Input.GetKeyDown(KeyCode.UpArrow) && maxPulo > 0 && abaixar == false)
            {
                estaPulo = true;
            }

            //Evitando que o Herói voe
            if (isOnFloor)
            {
                maxPulo = 1;
            }

            //Criando ação de ataque e tempo de ataque
            if (timeNextAttack <= 0f && abaixar == false)
            {
                if (Input.GetKey(KeyCode.Space) && corpo.velocity == new Vector2(0, 0))
                {
                    animacao.SetTrigger("Attack");
                    timeNextAttack = 0.2f;
                }
            }
            else
            {
                timeNextAttack -= Time.deltaTime;
            }


            //Correr gradativamente e dimuir corrida gradativamente
            if (Input.GetKey(KeyCode.LeftShift) && velocidade < 7)
            {
                velocidade += 0.1f;
            }
            else if (!Input.GetKey(KeyCode.LeftShift) && velocidade > 5)
            {
                velocidade -= 0.1f;
            }

        }
   
    }

    void FixedUpdate()
    {

        if (keysDisabled == false)
        {
            //Movimento do Herói + velocidade do movimento
            float movimento = Input.GetAxis("Horizontal");
           
            //Impedindo que o Herói ande agachado
            if (abaixar == false)
            {
                //Velocidade e movimento do herói tanto no ar quanto no solo 
                corpo.velocity = new Vector2(movimento * velocidade, corpo.velocity.y);
            }

            if ((movimento > 0 && sprite.flipX == true) || (movimento < 0 && sprite.flipX == false))
            {
                Flip();
            }

            //Ação de pular
            if (estaPulo)
            {
                maxPulo--;
                corpo.velocity = new Vector2(corpo.velocity.x, 0f);
                corpo.AddForce(new Vector2(0f, puloForca));
                estaPulo = false;
            }

            //Fazendo personagem agachar
            if (corpo.velocity.x > 0 || Input.GetKeyDown(KeyCode.DownArrow) == false || corpo.velocity.y > 0)
            {
                abaixar = false;
            }

            else
            {
                abaixar = true;
            }

            animacao.SetBool("Duck", abaixar);
        }
    }

    void Flip()
    {
        //Direção que o Herói olha
        sprite.flipX = !sprite.flipX;

        //Direção que o Herói ataca.
        attackCheck.position = new Vector2(-attackCheck.localPosition.x, attackCheck.localPosition.y);
     
    }

    void PlayerAttack()
    {
        //Atacar mais de um inimigo
        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll(attackCheck.position, radiusAttack, layerEnemy);
        for (int i = 0; i < enemiesAttack.Length; i++)
        {
            enemiesAttack[i].SendMessage("EnemyHit");
            Debug.Log(enemiesAttack[i].name);
        }
    }

    //Aumentando o alcance do groundCheck para evitar erros no pulo.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }

    //Validando animação do herói
    void AnimacaoHeroi()
    {
        //Verificando velocidade do eixo X e Y
        animacao.SetFloat("VelX", Mathf.Abs(corpo.velocity.x));
        animacao.SetFloat("VelY", Mathf.Abs(corpo.velocity.y));
    }
    
    //Fazendo morte e checkpoint do personagem
    void OnCollisionEnter2D(Collision2D collision2d)
    {
        if (collision2d.gameObject.CompareTag("Espeto"))
        {
            if (vida >= 0)
            {
                vida--;
            }
            animacao.SetTrigger("Morreu");           

            if (vida >= 0)
            {
                TextLives.text = vida.ToString();
            }

            keysDisabled = true;


            if (vida == 0)
            {
                //GameOver
            }
        }
        
        if (collision2d.gameObject.CompareTag("Inimigo"))
        {

            


            if(vivo){
                
                contadorColisaoInimigo++;
                if(contadorColisaoInimigo == 3){
                    vivo = false;                     

                    if (vida >= 0) {
                        vida--; 
                    }
                    animacao.SetTrigger("Morreu"); 
                    if(vida >=0){
                        TextLives.text = vida.ToString(); 
                    }

                    keysDisabled = true;
                    contadorColisaoInimigo = 0;
                }
            
            }
            

            
            if (vida == 0) {
                //GameOver
            }
        }

    }

    public void moverCheckPoint()
    {
        if (vida >= 0)
        {
            keysDisabled = false;
            animacao.SetTrigger("Renasce");
            transform.position = lastCheckPoint.transform.position;
            vivo = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision2d)
    {
        // Fazendo Checkpoint
        if (collision2d.gameObject.CompareTag("checkpoint"))
        {
            lastCheckPoint = collision2d.gameObject;
        }

    }

}