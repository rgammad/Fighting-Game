using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jumpForce = 25;
    public float jumpDuration = .1f;
    public float jmpCD = .1f;


    private Rigidbody2D rbody;
    private Animator anim;

    private float horizontal;
    private float vertical;
    private float jmpF, jmpD;
    private bool jmpKey;
    private bool falling;
    private bool onGround;
    private bool walking;
    private bool shielding;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, 0);

        if (vertical > 0.0f)
        {
            if (!jmpKey)
            {
                jmpD += Time.deltaTime;
                jmpF += Time.deltaTime;
                if (jmpD < jumpDuration)
                {
                    rbody.velocity = new Vector2(rbody.velocity.x, jmpF);
                }
                else
                {
                    jmpKey = true;
                }
            }
        }

        falling = (!onGround && vertical < 0.0f);
        shielding = (onGround && vertical < 0.0f);
        if (!shielding)
            rbody.transform.Translate(movement * moveSpeed * Time.deltaTime);
        else
            rbody.velocity = Vector2.zero;
        walking = (horizontal != 0);


    }

    void UpdateAnimator()
    {
        anim.SetBool("OnGround", this.onGround);
        anim.SetBool("Shield", this.shielding);
        anim.SetBool("Falling", this.falling);
        anim.SetFloat("Movement", horizontal);
        anim.SetBool("walking", walking);
    }

    private void JumpCD()
    {
        jmpKey = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGround = true;

            jmpD = 0;
            jmpF = jumpForce;
            falling = false;
            Invoke("JumpCD", jmpCD);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
