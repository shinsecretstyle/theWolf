using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    [SerializeField]
    private int phaseID;
    private float phaseUpSpeed = 0.8f;
    private float phaseDownSpeed = 0.2f;

    public float speed;
    public float jumpPower;
    public float jumpCD = 1f;
    public float phaseLimit = 5f;
    public float phaseProcess = 0f;
    public bool canJump = true;
    public bool isInLight;
    public bool isOnGround;
    public bool isOnWall;

    [SerializeField]
    private bool canWallJump;
    

    public Slider phaseSlider;
    public Sprite phase1;
    public Sprite phase2;
    public Sprite phase3;
    public Sprite phase4;
    public GameObject groundChecker;
    SpriteRenderer sr;
    Rigidbody2D rb;
    PolygonCollider2D polygonCollider;

    void Start()
    {
        phaseID = 1;
        phaseSlider.maxValue = phaseLimit;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        speed = 4;
        jumpPower = 41;
    }

    void Update()
    {
        phaseSlider.value = phaseProcess;
        float move = Input.GetAxis("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(move * speed,rb.velocity.y);

        //rb.velocity = new Vector2(rb.velocity.x,y*2);

        JumpEvent();

        inTheLight();

    }



    //phaseによって属性値の設定
    private void setPhaseByID(int id)
    {
        
        if(id == 1)//phase1
        {
            sr.sprite = phase1;
            updateCollider();
            speed = 4;
            rb.mass = 1;
            canWallJump = false;
        }
        else if(id == 2)//phase2
        {
            speed = 6;
            sr.sprite = phase2;
            updateCollider();
            rb.mass = 1;
            canWallJump = true;
        }
        else if(id == 3)//phase3
        {
            sr.sprite = phase3;
            updateCollider();
            //massを増加し、車を押すことができる
            rb.mass = 5f;
            canWallJump = false;
        }
        else if(id == 4)//phase4
        {
            sr.sprite = phase4;
            updateCollider();
        }
    }

    //phaseによってCollierを更新する
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
            phaseProcess += Time.deltaTime * phaseUpSpeed;//フェーズの増加スピード掛け算
        }
        else if (!isInLight && phaseProcess > 0f)
        {
            phaseProcess -= Time.deltaTime * phaseDownSpeed;//フェーズの減少スピード掛け算
        }

        if (phaseProcess >= phaseLimit && phaseID < 4)
        {
            phaseID++;
            setPhaseByID(phaseID);
            phaseProcess = 0f;
        }
        else if (phaseProcess <= 0 && phaseID > 1) {
            phaseID--;
            setPhaseByID(phaseID);
            phaseProcess = phaseLimit;
        }
        else if (phaseProcess <= 0 && phaseID == 1)
        {
            phaseProcess = 0f;
        }

    }

    IEnumerator resetGroundChecker()
    {
        groundChecker.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        groundChecker.SetActive(true);
    }

    private void JumpEvent()
    {
        if (Input.GetButton("Jump") && canJump)
        {
            StartCoroutine(resetGroundChecker());
            canJump = false;
            //ジャンプの高さを維持するため、mass掛け算
            rb.AddForce(new Vector2(rb.velocity.x, jumpPower * 10 * rb.mass));
            StartCoroutine(resetJumpCD());
        }


        if (isOnGround)
        {
            canJump = true;
            StopCoroutine(resetJumpCD());
        }else if(canWallJump && isOnWall)
        {
            canJump = true;
            StopCoroutine(resetJumpCD());
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) {
            rb.velocity = Vector2.zero;
            //canJump = true;
        }
    }

    public void checkGround(bool isground)
    {
        isOnGround = isground;
    }
    
    public void checkWall(bool isWall)
    {
        isOnWall = isWall;
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
