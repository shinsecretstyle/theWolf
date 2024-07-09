using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Dynamic;
using UnityEditor.Animations;

public class PlayerWithAnim : MonoBehaviour
{
    [SerializeField]
    private int phaseID;
    private float phaseUpSpeed = 0.8f;
    private float phaseDownSpeed = 0.2f;
    private int lastDir = 1;//�ŏ��͉E�Ɍ���

    public float speed;
    public float jumpPower;
    public float jumpCD = 1f;
    public float phaseLimit = 5f;
    public float phaseProcess = 0f;
    public bool canJump = true;
    public bool isInLight;
    public bool isOnGround;
    public bool isOnWall;
    //public AudioClip 月の光;
    //public AudioClip 無音;

    [SerializeField]
    private bool canWallJump;


    public Slider phaseSlider;
    public AnimatorController phase1;
    public AnimatorController phase2;
    public AnimatorController phase3;
    public GameObject groundChecker;
    SpriteRenderer sr;
    Rigidbody2D rb;
    PolygonCollider2D polygonCollider;
    Animator animator;
    AudioSource audioSource;

    void Start()
    {
        phaseID = 1;
        phaseSlider.maxValue = phaseLimit;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        speed = 4;
        jumpPower = 41;
        animator.SetFloat("Phase1", 1f);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        phaseSlider.value = phaseProcess;
        updateCollider();
        float move = Input.GetAxis("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        Debug.Log(rb.velocity+"   "+move);
        
        if (move > 0)//�E�����̏���
        {
            animator.SetFloat("Speed", move);
            animator.SetFloat("Right", 1f);
            animator.SetFloat("Left", 0f);
            animator.SetFloat("JumpUp", 0f);
            lastDir = 1;//�ړ���̌���
        }
        else if (move == 0)//�Î~��Ԃ̌�������
        {
            animator.SetFloat("Speed", 0);
            if(lastDir > 0)
            {
                animator.SetFloat("Right", 1f);
            }else animator.SetFloat("Left", 1f);
        }
        else//�������̏���
        {
            animator.SetFloat("Speed", -move);
            animator.SetFloat("Left", 1f);
            animator.SetFloat("Right", 0f);
            animator.SetFloat("JumpUp", 0f);
            lastDir = -1;//�ړ���̌���
        }

        //rb.velocity = new Vector2(rb.velocity.x,y*2);

        //JumpEvent();

        if (Input.GetButton("Jump") && canJump)
        {
            //StartCoroutine(resetGroundChecker());
            canJump = false;
            //�W�����v��
            //SoundManager.Instance.PlaySE(SESoundData.SE.Jump);
            //�W�����v�̍������ێ����邽�߁Amass�|���Z
            animator.SetTrigger("Jump");
            Debug.Log("Jump");
            rb.AddForce(new Vector2(rb.velocity.x, jumpPower * 10 * rb.mass));
            StartCoroutine(resetJumpCD());
        }


        inTheLight();

    }



    //phase�ɂ���đ����l�̐ݒ�
    private void setPhaseByID(int id)
    {

        if (id == 1)//phase1
        {
            //sr.sprite = phase1;
            //updateCollider();
            animator.runtimeAnimatorController = phase1;
            speed = 4;
            rb.mass = 1;
            canWallJump = false;
        }
        else if (id == 2)//phase2
        {
            animator.runtimeAnimatorController = phase2;
            speed = 6;
            //updateCollider();
            rb.mass = 1;
            canWallJump = true;
        }
        else if (id == 3)//phase3
        {
            animator.runtimeAnimatorController = phase3;
            //sr.sprite = phase3;
            //updateCollider();
            //mass�𑝉����A�Ԃ��������Ƃ��ł���
            rb.mass = 5f;
            canWallJump = false;
        }
        else if (id == 4)//phase4
        {
            //sr.sprite = phase4;
            //updateCollider();
        }
    }

    //phase�ɂ����Collier���X�V����
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
            phaseProcess += Time.deltaTime * phaseUpSpeed;//�t�F�[�Y�̑����X�s�[�h�|���Z
            //SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Moon);
            //if(audioSource !=null)
            //{
            //    AudioSource audioSource = GetComponents<AudioSource>()[0];
            //    audioSource.volume = 0.1f;
            //    audioSource.PlayOneShot (月の光);
            //    audioSource.loop = true;
            //}
        }
        else if (!isInLight && phaseProcess > 0f)
        {
            phaseProcess -= Time.deltaTime * phaseDownSpeed;//�t�F�[�Y�̌����X�s�[�h�|���Z
            //SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Off);
            //if(audioSource !=null)
            //{
            //    AudioSource audioSource = GetComponents<AudioSource>()[0];
            //    audioSource.volume = 0f;
            //    audioSource.PlayOneShot (無音);
            //    audioSource.loop = true;
            //}
        }

        if (phaseProcess >= phaseLimit && phaseID < 4)
        {
            phaseID++;
            setPhaseByID(phaseID);
            phaseProcess = 0f;
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
            //�W�����v��
            //SoundManager.Instance.PlaySE(SESoundData.SE.Jump);
            //�W�����v�̍������ێ����邽�߁Amass�|���Z
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