using UnityEngine;

public class Monster : MonoBehaviour
{
    public float MonsterHP = 30;
    public float MonsterDamage = 2f;
    public float MonsterExp = 3;

    private float moveTime = 0;
    private float turnTime = 0;
    private bool isDie = false;

    public float MoveSpeed = 3f;
    public GameObject[] ItemObj;

    private Animator MonsterAnimator;

    private void Start()
    {
        MonsterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        MonsterMove();
    }

    private void MonsterMove()
    {
        if (isDie) return;

        moveTime += Time.deltaTime;

        if (moveTime <= turnTime)
        {
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        } else
        {
            turnTime = Random.Range(1, 5);
            moveTime = 0;

            transform.Rotate(0, 180, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie) return;

        if (collision.gameObject.tag == "Player")
        {
            MonsterAnimator.SetTrigger("Attack");
            GameManager.Instance.PlayerStat.HP -= MonsterDamage;
        }

        if (collision.gameObject.tag == "Attack")
        {
            MonsterAnimator.SetTrigger("Damage");
            MonsterHP -= collision.gameObject.GetComponent<Attack>().AttackDamage;

            if (MonsterHP <= 0)
            {
                MonsterDie();
            }
        }
    }

    private void MonsterDie()
    {
        isDie = true;

        MonsterAnimator.SetTrigger("Die");
        GameManager.Instance.PlayerStat.Exp += MonsterExp;

        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.5f);
    }

    private void OnDestroy()
    {
        int itemRandom = Random.Range(0, ItemObj.Length);
        if (itemRandom < ItemObj.Length)
        {
            Instantiate(ItemObj[itemRandom], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }
}
