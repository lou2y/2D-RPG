using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2d;
    private AudioSource audioSource;

    public AudioClip JumpClip;

    public float Speed = 4f;
    public float Jumppower = 6f;

    private bool isFloor;

    public GameObject AttackObj;
    public float AttackSpeed = 3f;
    public AudioClip AttackClip;

    private bool justAttack, justJump;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        AttackCheck();
        JumpCheck();
    }
    private void FixedUpdate()
    {
        Attack();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isFloor = false;
        }
    }

    private void JumpCheck()
    {
        if (isFloor)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                justJump = true;
            }
        }
    }

    private void Jump()
    {
        if (justJump)
        {
            justJump = false;
            rigidbody2d.AddForce(Vector2.up * Jumppower, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            audioSource.PlayOneShot(JumpClip);
        }
    }

    private void AttackCheck()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            justAttack = true;
        }
    }

    private void Attack()
    {
        if(justAttack)
        {
            justAttack = false;

                animator.SetTrigger("Attack");
            audioSource.PlayOneShot(AttackClip);

            if (gameObject.name == "Warrior")
            {
                AttackObj.SetActive(true);
                Invoke("SetAttackObjInactive", 0.5f);
            }


            if (spriteRenderer.flipX)
            {
                GameObject obj = Instantiate(AttackObj, transform.position, Quaternion.Euler(0, 180f, 0));
                obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * AttackSpeed, ForceMode2D.Impulse);
                Destroy(obj, 3f);
            }
            else
            {
                GameObject obj = Instantiate(AttackObj, transform.position, Quaternion.Euler(0, 0, 0));
                obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * AttackSpeed, ForceMode2D.Impulse);
                Destroy(obj, 3f);
            }
        }
    }

    private void SetAttackObjInactive()
    {
        AttackObj.SetActive(false);
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
        }
    }


}
