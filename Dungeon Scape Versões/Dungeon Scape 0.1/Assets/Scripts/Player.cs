using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class Player:MonoBehaviour {
    
    //Var para realizar validação do pulo 
    bool isJumping = false; 
    bool isOnFloor = false; 
    private int maxJump; 
    bool keysDisabled = false; 

    //Agachar
    bool duck = false; 

    //Interface
    public Text TextLives; 

   
    //vida
    public int vida = 3; 
    //Checkpoint
    public GameObject lastCheckPoint; 

    [Header("Movement Variables")]
    //Var para velocidade do herói e força do pulo
    public float jumpForce = 550f; 
    public float speed = 5f; 
    public float radius = 0.35f; 
    //Var validando o chão
    public Transform groundCheck; 
    public LayerMask whatIsGround; 

    [Header("Attack Variables")]
    public Transform attackCheck; 
    public float radiusAttack; 
    public LayerMask layerEnemy; 
    float timeNextAttack; 

    Rigidbody2D body; 
    SpriteRenderer sprite; 
    Animator anime; 

    // Use this for initialization
    void Start() {
        body = GetComponent < Rigidbody2D > (); 
        sprite = GetComponent < SpriteRenderer > (); 
        anime = GetComponent < Animator > (); 
        TextLives.text = vida.ToString(); 
    }

    // Update is called once per frame
    void Update() {
        //Verifica se o herói está no chão
        isOnFloor = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround); 
        
        if (keysDisabled == false) {

            //Validando pulo e duplo pulo
            //if (Input.GetButtonDown("Jump") && maxJump > 0) 
            if (Input.GetKeyDown(KeyCode.UpArrow) && maxJump > 0 && duck == false)
                isJumping = true; 
        

            //Herói toca no chão e ganha pulo - EVITA que o HEROI voe
            if (isOnFloor) {
                maxJump = 1; 
            }


            //Attack do personagem - validando quando atacar
            if (timeNextAttack <= 0f && duck == false) {

                
                //if (Input.GetButtonDown("Fire1") && body.velocity == new Vector2(0, 0))
                if (Input.GetKey(KeyCode.Space) && body.velocity == new Vector2(0, 0)) {
                    anime.SetTrigger("Attack"); 
                    timeNextAttack = 0.2f; 
                }
            }
            else {
                timeNextAttack -= Time.deltaTime; 
            }


            //Correr
            if (Input.GetKey(KeyCode.LeftShift) && speed < 7) {
                speed += 0.1f; 
            }else if ( ! Input.GetKey(KeyCode.LeftShift) && speed > 5) {
                speed -= 0.1f; 
            }

        }
        //Carregando a animação ao iniciar o jogo
        PlayerAnimation(); 

    }

    void FixedUpdate() {

        if (keysDisabled == false) {
        
            //MOVE - recebe quando as teclas <- -> são pressionadas
            float move = Input.GetAxis("Horizontal"); 


            //Impedindo que o Herói anda agaichado
            if (duck == false) {
                //Velocidade e movimento do herói tanto no ar quanto no solo 
                body.velocity = new Vector2(move * speed, body.velocity.y); 
            }

            if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false)) {
                Flip(); 
            }

            //Ação de pulo
            if (isJumping) {
                maxJump--; 
                body.velocity = new Vector2(body.velocity.x, 0f); 
                body.AddForce(new Vector2(0f, jumpForce)); 
                isJumping = false; 
            }

            //Fazendo personagem abaixar
        
            if (Input.GetAxis("Vertical") < 0) {
                duck = true; 
            }
            else {
                duck = false; 
            }
            anime.SetBool("Duck", duck); 
        }
    }

    void Flip() {
        sprite.flipX =  ! sprite.flipX; 
        //Direcionando o lado do ataque com a espada.
        attackCheck.position = new Vector2( - attackCheck.localPosition.x, attackCheck.localPosition.y); 
    }

    void PlayerAttack() {
        //Atacar mais de um inimigo
        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll(attackCheck.position, radiusAttack, layerEnemy); 
        for (int i = 0; i < enemiesAttack.Length; i++) {
            enemiesAttack[i].SendMessage("EnemyHit"); 
            Debug.Log(enemiesAttack[i].name); 
        }
    }


    //Aumentando o alcance do groundCheck para evitar erros no pulo.
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(groundCheck.position, radius); 
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack); 
    }

    //Validando animação do herói
    void PlayerAnimation() {
        anime.SetFloat("VelX", Mathf.Abs (body.velocity.x)); 
        anime.SetFloat("VelY", Mathf.Abs (body.velocity.y)); 
    }





    //Fazendo morte e checkpoint do personagem
    void OnCollisionEnter2D(Collision2D collision2d) {
        if (collision2d.gameObject.CompareTag("Espeto")) {
            if (vida >= 0) {
            vida--; 
            }
            anime.SetTrigger("Morreu"); 
            if(vida >=0){
                TextLives.text = vida.ToString(); 
            }
            keysDisabled = true;


            if (vida == 0) {
                //GameOver
            }
        }

          //Quando criar os inimigos, colocar a tag "Inimigo" neles.
        //if (collision2d.gameObject.CompareTag("Inimigo")) {
          //  vida--; 
            //anime.SetTrigger("Morreu"); 


            
       //     if (vida == 0) {
                //GameOver
         //   }
      //  }

    }

        public void moverCheckPoint() {
                             
            if(vida>=0){   
                keysDisabled = false;
                anime.SetTrigger("Renasce");
                transform.position = lastCheckPoint.transform.position;                 
            }
        }

        void OnTriggerEnter2D(Collider2D collision2d) {
        // Fazendo Checkpoint
        if (collision2d.gameObject.CompareTag("checkpoint")) {
            
                 
                lastCheckPoint = collision2d.gameObject;
                
        }
        
    }

}