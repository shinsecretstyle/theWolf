using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    [SerializeField]
    private int fadeID;

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
    public Sprite fade3;
    public Sprite fade4;
    SpriteRenderer sr;
    Rigidbody2D rb;
    PolygonCollider2D polygonCollider;
  


    void Start()
    {
        fadeID = 1;
        fadeSlider.maxValue = fadeLimit;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
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

        if (Input.GetButton("Jump") && canJump)
        {
            canJump = false;
            //ジャンプの高さを維持するため、mass掛け算
            rb.AddForce(new Vector2 (rb.velocity.x,jump * 10 * rb.mass));
            StartCoroutine(resetJumpCD());
        }

        inTheLight();
    }

    //fadeによって属性値の設定
    private void setFadeByID(int id)
    {
        
        if(id == 1)//fade1
        {
            sr.sprite = fade1;
            updateCollider();
            speed = 4;
            rb.mass = 1;
        }
        else if(id == 2)//fade2
        {
            speed = 6;
            sr.sprite = fade2;
            updateCollider();
            rb.mass = 1;
        }
        else if(id == 3)//fade3
        {
            sr.sprite = fade3;
            updateCollider();
            //massを増加し、車を押すことができる
            rb.mass = 5f;
        }
        else if(id == 4)//fade4
        {
            sr.sprite = fade4;
            updateCollider();
        }
    }

    //fadeによってCollierを更新する
    private void updateCollider()
    {
        int pathCount = sr.sprite.GetPhysicsShapeCount();
        polygonCollider.pathCount = pathCount;

        for (int i = 0; i < pathCount; i++)
        {
            List<Vector2> path = new List<Vector2>();
            sr.sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        }
    }

    private void inTheLight()
    {
        if (isInLight)
        {
            fadeProcess += Time.deltaTime * 0.8f;
        }
        else if (!isInLight && fadeProcess > 0f)
        {
            fadeProcess -= Time.deltaTime * 0.2f;
        }

        if (fadeProcess >= fadeLimit && fadeID < 4)
        {
            fadeID++;
            setFadeByID(fadeID);
            fadeProcess = 0f;
        }
        else if (fadeProcess <= 0 && fadeID > 1) {
            fadeID--;
            setFadeByID(fadeID);
            fadeProcess = fadeLimit;
        }
        else if (fadeProcess <= 0 && fadeID == 1)
        {
            fadeProcess = 0f;
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
