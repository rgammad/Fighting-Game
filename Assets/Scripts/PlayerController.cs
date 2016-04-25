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
    private bool falling = false;
    private bool onGround = true;
    private bool walking = false;
    private bool shielding = false;
    private bool[] attack = new bool[2];
    private float[] attackTimer = new float[2];
    private int[] timesPressed = new int[2];
    public float attackRate = .3f;


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AttackInput();
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

        if (Input.GetButton("Shield"))
        {
            shielding = true;
            rbody.velocity = Vector2.zero;
        }
        else
        {
            shielding = false;
            rbody.transform.Translate(movement * moveSpeed * Time.deltaTime);

        }
        walking = (horizontal != 0);


    }

    void UpdateAnimator()
    {
        anim.SetBool("OnGround", this.onGround);
        anim.SetBool("Shield", this.shielding);
        anim.SetBool("Falling", this.falling);
        anim.SetFloat("Movement", horizontal);
        anim.SetBool("walking", walking);
        anim.SetBool("Attack1", attack[0]);
    }


    private void AttackInput()
    {
        if (Input.GetButtonDown("Attack1"))
        {
            attack[0] = true;
            attackTimer[0] = 0;
            timesPressed[0]++;
        }

        if (attack[0])
        {
            attackTimer[0] += Time.deltaTime;
            if (attackTimer[0] > attackRate || timesPressed[0] >= 4)
            {
                attackTimer[0] = 0;
                attack[0] = false;
                timesPressed[0] = 0;
            }
        }

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
