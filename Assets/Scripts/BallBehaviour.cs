using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallBehaviour : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    public float ballSpeed = 300f;
    private Rigidbody2D brb;
    private Vector2 bounce = new Vector2(0.5f , 1f);
    private bool isInGame = true;
    public ParticleSystem brickExplosion;
    public ParticleSystem Explosion;
    public int score = 0;
    private const int SCORE_PER_BRICK = 5;
    private const int SCORE_PER_DEATH = 20;
    public int lives = 3;
    public TextMeshProUGUI livesText;
    public CircleCollider2D ballCollider;
    public TextMeshProUGUI scoreText;
    public AudioSource brickaudio;
    public AudioSource explosionAudio;
    public GameObject LifePowerUp;
    public GameObject LengthPowerUp;
    public GameObject ExplosionPowerUp;
    public bool isExplosable;
    public bool isGameOver = false;

    public TextMeshProUGUI GameOverText;
    

    void Start()
    {
        brb = GetComponent<Rigidbody2D>();
        Invoke("StartGame",2f);
         
    }

    void StartGame()
    {
           brb.AddForce( bounce * ballSpeed);
    }
    void Update()
    {
        if(isGameOver)
        {
            GameOverText.text = "GAME OVER!";
            Invoke("LoadMainMenu",3f);
            return;
        }


        if(!isInGame)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                brb.AddForce(bounce * ballSpeed);
                isInGame = true;
            }
        }

       
        if(score < 0)
            score = 0;

       

    }

    void OnGUI()
    {
        
        scoreText.text = "SCORE: " + score.ToString();
        livesText.text = "LIVES: " + lives.ToString();         
    }

    void OnCollisionEnter2D(Collision2D other) {
        int chanceOfPowerUp = (int)UnityEngine.Random.Range(1,5);

        if(other.gameObject.CompareTag("LowerBoundary")){
            Debug.Log("DEATH!");
            isInGame = false;
            ball.transform.position = player.transform.position;
            brb.velocity = Vector2.zero;
            score -= SCORE_PER_DEATH;
            lives--;

            if(lives == 0)
            {
                GameOver();
            }

        }
        

        if(other.gameObject.CompareTag("Brick") )
        {
            ParticleSystem explosion;
            Destroy(other.gameObject);
           
            if(isExplosable)
            {
                explosionAudio.Play();
               isExplosable = false;
               explosion = Instantiate(Explosion , transform.position , transform.rotation);
                Destroy(explosion.gameObject , 2.5f);
               ResetBall(); 
            }
            else 
            {
                 brickaudio.Play();
                ParticleSystem Brickbreak = Instantiate(brickExplosion , transform.position , transform.rotation);
                    Destroy(Brickbreak.gameObject , 2.5f);
            }
            score += SCORE_PER_BRICK;

            GeneratePowerUp(chanceOfPowerUp);
        }
    }

    void GeneratePowerUp(int chance)
    {
        int whichPowerUp = (int)UnityEngine.Random.Range(1,5);

        if(chance == 1)
            switch(whichPowerUp)
            {
                case 1: GameObject lifePowerup = Instantiate(LifePowerUp , transform.position , transform.rotation);
                        break;
                case 2: GameObject lengthPowerUp = Instantiate(LengthPowerUp , transform.position , transform.rotation);
                        break;
                case 3: // TODO: Points Powerup
                        break;
                case 4: GameObject explosionPoweUp = Instantiate(ExplosionPowerUp , transform.position , transform.rotation);
                        break;

            }
    }

    void ResetBall()
    {
        Debug.Log("CALLED RESET");
        ballCollider.radius -= 0.2f;
        isExplosable = false;

    }

    void GameOver()
    {
        isGameOver = true;
    }

    void LoadMainMenu()
    {
        
    }


    
}
