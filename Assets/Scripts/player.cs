using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float speed;
    public float jump;
    public float jumpCD = 1f;
    float move;
    public float fadeLimit = 5f;
    public float fadeProcess = 0f;
    public bool canJump = true;
    //public LayerMask lightDectect;
    public bool isInLight;
    public Slider fadeSlider;
    public Sprite fade1;
    public Sprite fade2;

    SpriteRenderer sr;
    //public GameObject headLeft;
    //public GameObject headRight;
    //public GameObject footLeft;
    //public GameObject footRight;
    //public GameObject moon;


    Rigidbody2D rb;
  


    void Start()
    {
        fadeSlider.maxValue = fadeLimit;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        speed = 4;
        jump = 42;
    }

    void Update()
    {
        fadeSlider.value = fadeProcess;
        move = Input.GetAxis("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(move * speed,rb.velocity.y);
        //rb.velocity = new Vector2(rb.velocity.x,y*2);
    

        if(isInLight)
        {
            fadeProcess += Time.deltaTime * 0.8f;
        }else if(!isInLight && fadeProcess > 0f)fadeProcess -= Time.deltaTime * 0.2f;

        if(fadeProcess >= fadeLimit)
        {
            sr.sprite = fade2;
            //massを増加し、車を押すことができる
            rb.mass = 5f;
            fadeProcess = 0f;
        }

        if (Input.GetButton("Jump") && canJump)
        {
            canJump = false;
            //ジャンプの高さを維持するため、mass掛け算
            rb.AddForce(new Vector2 (rb.velocity.x,jump * 10 * rb.mass));
            StartCoroutine(resetJumpCD());
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) {
            rb.velocity = Vector2.zero;
        }
    }
    
    IEnumerator resetJumpCD()
    {
        yield return new WaitForSeconds(jumpCD);
        canJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Moon")
        {
            isInLight = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Moon")
        {
            isInLight = false;
        }
    }

    public void checkLight(bool inLight)
    {
        if(inLight)
        {
            isInLight = true;
        }else isInLight = false;
    }
}
