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
    public bool isMirror; //カーブミラーの光
    public bool InvisibleWall = false; //番犬に使われている透明な壁
    public bool isBark = false; //吠えるアニメーション
    public bool isSleep = false; //寝るアニメーション
    public bool isSleep0;
    public bool Ladder = false; //はしご
    public bool isCapture = false;//警察に捕まった時
    public bool isPoliceArea = false; //警察のエリア
    public bool isLayDown = false; //車の下やしゃがんで通る道でバグを引き起こさないようにしゃがみ状態を維持する
    public bool isPush = false;
    public bool Dognear; //犬が近いとき
    public bool Dogfar;
    float seconds;
    public bool isCrouch;
    
    [Header("FadeManager")] public FadeManager fade;


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
        jumpPower = 35;
        rb.mass = 1f;
        animator.SetFloat("Phase1", 1f);
        GameObject obj1 = GameObject.Find("Player");
        GameObject obj2 = GameObject.Find("Enemy Dog");
        //obj1,2を決める
        isSleep0 =true;
        //SoundManager.Instance.PlayBGM(BGMSoundData.BGM.BGM);
    }

    void Update()
    {
        phaseSlider.value = phaseProcess;
        updateCollider();
        float move = Input.GetAxis("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        GameObject obj1 = GameObject.Find("Player");
        GameObject obj2 = GameObject.Find("Enemy Dog");
        float distance = Vector3.Distance(obj1.transform.position,obj2.transform.position);
        //obj1,2の距離を算出
        //Debug.Log("Distance between Object1 and Object2:" + distance);
        
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

        if (Input.GetButton("Jump") && canJump &&!isCrouch)
        {
            //StartCoroutine(resetGroundChecker());
            canJump = false;
            //ジャンプ音
            //SoundManager.Instance.PlaySE(SESoundData.SE.Jump);
            //ジャンプの高さを維持するため、mass掛け算
            animator.SetTrigger("Jump");
            groundChecker.SetActive(false);
            StartCoroutine(resetGroundChecker());
            Debug.Log("Jump");

            //空中二段ジャンプの高さを修正ためのコード
            Vector2 v = rb.velocity;
            v.y = 0f;
            rb.velocity = v;//落ちてるスピードを0に設定

            //ジャンプ
            rb.AddForce(new Vector2(rb.velocity.x, jumpPower * 10 * rb.mass));
            //StartCoroutine(resetJumpCD());
        }

        if(Input.GetKey(KeyCode.LeftShift))
        //しゃがみアニメーション
        {
            animator.SetBool("LayDown",true);
            isCrouch = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {

            animator.SetBool("LayDown",false);
            isCrouch = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)&&!isLayDown)
        {
            animator.SetBool("Crouch",true);
            isCrouch = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)&&!isLayDown)
        {
            animator.SetBool("Crouch",false);
            isCrouch = false;
        }
        if(Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Ladder",true);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Ladder",false);
        }
        if(Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Ladder",true);
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Ladder",false);
        }

        inTheLight();

        if (!canJump)
        {
            groundCheck();
        }

        if(distance<10)
        //obj1,2の距離が10より小さい
        {
            Debug.Log("10以下");
            Dognear = true;
            Dogfar = false;
            isSleep0 = true;
        }
        else if(distance >10)
        //10より大きい
        {
            Debug.Log("10以上");
            Dogfar = true;
            Dognear = false;
            isSleep0 = false;
            isSleep = true;
        }

        if(Dognear && animator.runtimeAnimatorController == phase1)
        //10より近く、フェーズが1のとき
        {
            Debug.Log("吠える犬アニメーション");
            isBark = true;
            isSleep0 = false;
            isSleep = false;
        }
        else if(Dognear && animator.runtimeAnimatorController == phase2)
        //〃フェーズ２
        {
            Debug.Log("吠える犬アニメーション");
            isBark = true;
            isSleep0 = false;
            isSleep = false;
        }
        else if(Dognear && animator.runtimeAnimatorController == phase3)
        //〃フェーズ3
        {
            Debug.Log("寝る犬アニメーション");
            isSleep = true;
            isBark = false;
            isSleep0 = false;
        }
        else
        {
            isBark = false;
            isSleep0 = true;
        }


    }


    private void groundCheck()
    {
        if (isOnGround)
        {
            canJump = true;
            animator.SetTrigger("JumpOver");
        }
    }


    //phaseによって属性値の設定
    private void setPhaseByID(int id)
    {

        if (id == 1)//phase1
        {
            phaseProcess = 0f;
            animator.runtimeAnimatorController = phase1;
            transform.localScale = new Vector3(0.1f,0.1f,0.1f);//サイズ
            speed = 4;
            rb.mass = 1f;
            canWallJump = false;
            InvisibleWall = false;
            jumpPower = 35;
            isPush = false;

        }
        else if (id == 2)//phase2
        {
            phaseProcess = 0f;
            animator.runtimeAnimatorController = phase2;
            transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            speed = 6;
            rb.mass = 1f;
            canWallJump = true;
            InvisibleWall= false;
            jumpPower = 35;
            isPush = false;
        }
        else if (id == 3)//phase3
        {
            phaseProcess = 0f;
            animator.runtimeAnimatorController = phase3;
            transform.localScale = new Vector3(0.13f,0.13f,0.13f);//フェーズ3では少しデカくしている
            rb.mass = 120f;//massを増加し、車を押すことができる
            canWallJump = false;
            InvisibleWall = true;
            isBark = false;
            jumpPower = 41;
            isPush = true;
        }
        else if (id == 4)//phase4
        {
            fade.StartFadeIn();
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
            if(isMirror)
            //ミラーの光に触れているとき
            {
                phaseProcess += Time.deltaTime * phaseUpSpeed*1;
            }

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
        if (collision.tag == "Mirror")
        //カーブミラーに触れたとき
        {
            Debug.Log("Mirror");
            isMirror = true;
        }

        /*if(collision.gameObject.CompareTag("LayDown"))
        {
            animator.SetBool("Crouch",true);
            animator.SetBool("LayDown",false);
        }
        */


        if(collision.gameObject.CompareTag("Ladder"))
        {
            //animator.SetBool("LadderMove",true);
        }

        if(collision.gameObject.CompareTag("Police")&&animator.runtimeAnimatorController == phase3)
        //フェーズ3で警察の前を通った時
        {
            
            transform.position = new Vector3(201,0,0);
            transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            phaseID =1;
            phaseProcess =0f;
            animator.runtimeAnimatorController = phase1;
            speed =0;
            rb.mass =1f;
            canWallJump =false;
            InvisibleWall =false;
            isPush = false;
            jumpPower =0;
            Debug.Log("Police");
            StartCoroutine(ResetSpeedjumpPower());
            fade.StartFadeIn();
            //SceneManager.LoadScene("Police CP");
        }

        if(collision.gameObject.CompareTag("Dog")&&animator.runtimeAnimatorController == phase1)
        //番犬アニメーション
        {
            //isBark = true;
        }
        else if(collision.gameObject.CompareTag("Dog")&&animator.runtimeAnimatorController == phase2)
        {
            //isBark = true;
        }
        else if(collision.gameObject.CompareTag("Dog")&&animator.runtimeAnimatorController == phase3)
        {
            //isBark = false;
            Debug.Log("False");
        }
        else
        {
            //isBark = false;
        }

        if (collision.tag == "Goal")
        {
            fade.StartFadeIn();
            SceneManager.LoadScene("New ED");
        }
        if (collision.tag == "Enemy")
        {
            fade.StartFadeIn();
            transform.position = new Vector3(57,0,0);
        }
    }

    IEnumerator ResetSpeedjumpPower()
    //警察に捕まった後の動作
    {
        animator.SetBool("isCapture",true);
        yield return new WaitForSeconds(5f);//五秒後
        animator.SetBool("isCapture",false);
        speed=4;
        jumpPower=35;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("Ladder"))
         //はしごアニメーション
        {
            animator.SetBool("LadderMove",true);
        }

        if(collision.gameObject.CompareTag("LayDown"))
        {
            animator.SetBool("LayDown",true);
            isLayDown = true;
            isCrouch = true;
            //animator.SetBool("Crouch",false);
        }

        if(collision.gameObject.CompareTag("PoliceArea")&&animator.runtimeAnimatorController == phase1)
        //警察にアニメーション
        {
            isPoliceArea = false;
        }
        else if(collision.gameObject.CompareTag("PoliceArea")&&animator.runtimeAnimatorController == phase2)
        {
            isPoliceArea = false;
        }
        else if(collision.gameObject.CompareTag("PoliceArea")&&animator.runtimeAnimatorController == phase3)
        {
            isPoliceArea = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Moon")
        {
            isInLight = false;
        }

        if(collision.gameObject.CompareTag("LayDown"))
        {
            animator.SetBool("LayDown",false);
            animator.SetBool("Crouch",false);
            isCrouch = false;
            isLayDown = false;
        }

        if(collision.gameObject.CompareTag("Dog"))//番犬アニメーション
        {
            isBark = false;
        }
        if(collision.gameObject.CompareTag("PoliceArea"))
        {
            isPoliceArea = false;
        }

        if(collision.gameObject.CompareTag("Ladder"))//はしごアニメーション
        {
            animator.SetBool("LadderMove",false);
        }
        if (collision.tag == "Mirror")
        //カーブミラー
        {
            Debug.Log("MirrorOut");
            isMirror = false;
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