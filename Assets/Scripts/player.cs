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
    // Start is called before the first frame update
    void Start()
    {
        fadeSlider.maxValue = fadeLimit;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        speed = 4;
        jump = 42;
    }

    // Update is called once per frame
    void Update()
    {
        fadeSlider.value = fadeProcess;
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed,rb.velocity.y);

        if(isInLight)
        {
            fadeProcess += Time.deltaTime * 0.8f;
        }else if(!isInLight && fadeProcess > 0f)fadeProcess -= Time.deltaTime * 0.2f;

        if(fadeProcess >= fadeLimit)
        {
            sr.sprite = fade2;
            fadeProcess = 0f;
        }

        if (Input.GetButton("Jump") && canJump)
        {
            canJump = false;
            rb.AddForce(new Vector2 (rb.velocity.x,jump * 10));
            StartCoroutine(resetJumpCD());
        }

        //RaycastHit2D hit1 = Physics2D.Raycast(headLeft.transform.position,moon.transform.position - transform.position,Mathf.Infinity);
        //RaycastHit2D hit2 = Physics2D.Raycast(headRight.transform.position, moon.transform.position - transform.position, Mathf.Infinity);
        //RaycastHit2D hit3 = Physics2D.Raycast(footLeft.transform.position, moon.transform.position - transform.position, Mathf.Infinity);
        //RaycastHit2D hit4 = Physics2D.Raycast(footRight.transform.position, moon.transform.position - transform.position, Mathf.Infinity);

        //if ((hit1.collider != null && hit1.collider.CompareTag("Moon")) 
        //    || (hit2.collider != null && hit2.collider.CompareTag("Moon"))
        //    || (hit3.collider != null && hit3.collider.CompareTag("Moon"))
        //    || (hit4.collider != null && hit4.collider.CompareTag("Moon")))
        //{
        //    isInLight = true;
        //}else isInLight = false;

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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 direction = (moon.transform.position - headLeft.transform.position).normalized;
    //    Gizmos.DrawRay(headLeft.transform.position, direction * 100);
    //}
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
}
