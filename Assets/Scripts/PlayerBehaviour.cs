using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public float slideSpeed = 10f;
    private bool isScaled = false;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(xAxis , 0f , 0f);
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) )
        {
            transform.position += (move*Time.deltaTime*slideSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Vector3 scaleBy = new Vector3(2,0,0);

         if(other.gameObject.CompareTag("LifePowerUp"))
        {
            Destroy(other.gameObject);
            GameObject.Find("Ball").GetComponent<BallBehaviour>().lives ++;
            
        }

        if(other.gameObject.CompareTag("LengthPowerUp"))
        {
            Destroy(other.gameObject);
            
            if(!isScaled)
            {
                transform.localScale += scaleBy;
                isScaled = true;
                Invoke("Reset",5f);
            }
            else
            {
                GameObject.Find("Ball").GetComponent<BallBehaviour>().score += 5;
            }
        }

        if(other.gameObject.CompareTag("ExplosionPowerUp"))
        {
            Destroy(other.gameObject);

            if( !GameObject.Find("Ball").GetComponent<BallBehaviour>().isExplosable)
            {
                GameObject.Find("Ball").GetComponent<BallBehaviour>().isExplosable = true;
                GameObject.Find("Ball").GetComponent<BallBehaviour>().ballCollider.radius += 0.2f;
            }
            else
            {
                GameObject.Find("Ball").GetComponent<BallBehaviour>().score += 5;
            }
        }
    
    }


    void Reset()
    {
        Vector3 scaleBy = new Vector3(2,0,0);
        transform.localScale -= scaleBy;
        isScaled = false;
    }
}
