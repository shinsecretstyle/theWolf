using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Dynamic;

public class PlayerWithAnim : MonoBehaviour
{
    [SerializeField]
    private float phaseUpSpeed = 1f;
    [SerializeField]
    private float phaseDownSpeed = 0.2f;
    [SerializeField]
    private int lastDir = 1;//1は右向け、-1は左向け

    public int phaseID;
    public float speed;
    public float jumpPower;
    public float jumpCD = 1f;
    float phaseLimit;
    public float phaseUpTime = 7f;//具体的に時間秒数
    public float phaseDownTime = 5f;//具体的に時間秒数
    public float phaseProcess = 0f;
    public bool canJump = true;
    public bool isInLight;
    public bool isOnGround;
    public bool isOnWall;

    [SerializeField]
    private bool canWallJump;

    public Slider phaseSlider;
    public RuntimeAnimatorController phase1;
    public RuntimeAnimatorController phase2;
    public RuntimeAnimatorController phase3;
    public GameObject groundChecker;
    SpriteRenderer sr;
    Rigidbody2D rb;
    PolygonCollider2D polygonCollider;
    Animator animator;

    void Start()
    {
        phaseLimit = phaseUpTime;
        phaseUpSpeed = phaseLimit/phaseUpTime;
        phaseDownSpeed = phaseLimit/phaseDownTime;
        phaseID = 1;
        phaseSlider.maxValue = phaseLimit;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        speed = 4;
        jumpPower = 41;
        animator.SetFloat("Phase1", 1f);
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.BGM);
    }

    void Update()
    {
        phaseSlider.value = phaseProcess;
        updateCollider();
        float move = Input.GetAxis("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        
        if (move > 0)//右向け移動
        {
            animator.SetFloat("Speed", move);
            animator.SetFloat("Right", 1f);
            animator.SetFloat("Left", 0f);
            animator.SetFloat("JumpUp", 0f);
            lastDir = 1;//右向け
        }
        else if (move == 0)//止まる状態
        {
            animator.SetFloat("Speed", 0);
            if(lastDir > 0)
            {
                animator.SetFloat("Right", 1f);
            }else animator.SetFloat("Left", 1f);
        }
        else//左向け移動
        {
            animator.SetFloat("Speed", -move);
            animator.SetFloat("Left", 1f);
            animator.SetFloat("Right", 0f);
            animator.SetFloat("JumpUp", 0f);
            lastDir = -1;//左向け
        }

        //JumpEvent();

        if (Input.GetButton("Jump") && canJump)
        {
            //StartCoroutine(resetGroundChecker());
            canJump = false;
            //ジャンプ音
            //SoundManager.Instance.PlaySE(SESoundData.SE.Jump);
            //ジャンプの高さを維持するため、mass掛け算
            animator.SetTrigger("Jump");
            Debug.Log("Jump");

            //空中二段ジャンプの高さを修正ためのコード
            Vector2 v = rb.velocity;
            v.y = 0f;
            rb.velocity = v;//落ちてるスピードを0に設定

            //ジャンプ
            rb.AddForce(new Vector2(rb.velocity.x, jumpPower * 10 * rb.mass));
            StartCoroutine(resetJumpCD());
        }

        inTheLight();

    }



    //phaseによって属性値の設定
    private void setPhaseByID(int id)
    {

        if (id == 1)//phase1
        {
            phaseProcess = 0f;
            animator.runtimeAnimatorController = phase1;
            speed = 4;
            rb.mass = 1;
            canWallJump = false;

        }
        else if (id == 2)//phase2
        {
            phaseProcess = 0f;
            animator.runtimeAnimatorController = phase2;
            speed = 6;
            rb.mass = 1;
            canWallJump = true;
        }
        else if (id == 3)//phase3
        {
            phaseProcess = 0f;
            animator.runtimeAnimatorController = phase3;
            rb.mass = 5f;//massを増加し、車を押すことができる
            canWallJump = false;
        }
        else if (id == 4)//phase4
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("Gameover");
            //ゲームオーバー関数を読み込む
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
        }
        else if (phaseProcess <= 0 && phaseID > 1)
        {
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
            //StartCoroutine(resetGroundChecker());
            canJump = false;
            //ジャンプ音
            //SoundManager.Instance.PlaySE(SESoundData.SE.Jump);
            //ジャンプの高さを維持するため、mass掛け算
            animator.SetTrigger("Jump");
            Debug.Log("Jump");
            rb.AddForce(new Vector2(rb.velocity.x, jumpPower * 10 * rb.mass));
            StartCoroutine(resetJumpCD());
        }


        if (isOnGround)
        {
            canJump = true;
            StopCoroutine(resetJumpCD());
        }
        else if (canWallJump && isOnWall)
        {
            canJump = true;
            StopCoroutine(resetJumpCD());
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            //canJump = true;
        }
        //Debug.Log("Collided with: " + collision.gameObject.name);
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
        if (collision.tag == "Moon")
        {
            isInLight = true;
        }
        if (collision.tag == "Goal")
        {
            SceneManager.LoadScene("Goal");
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
        if (inLight)
        {
            isInLight = true;
        }
        else isInLight = false;
    }
}