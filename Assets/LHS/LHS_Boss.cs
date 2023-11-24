using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEngine.GraphicsBuffer;

public class LHS_Boss : MonoBehaviour
{
    [Header("�̵�")]
    [SerializeField] float speed = 1;

    [Header("�߻�")]     //�� �ִϸ��̼Ǹ��� �ٸ� ��ġ���� ������?
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePos;
    [SerializeField] float startFire = 5f;
    [SerializeField] float Delay = 1f;

    [Header("data")] //���ݷ��� �ʿ�? -> ��� ������ ����?
    [SerializeField] int hp = 100;
    public int attack = 20;
    public int addScore = 5;

    [Header("ȿ��")]
    public GameObject effectfab;
    bool isEffect = false;

    //�ִϸ��̼��� ����
    GameObject target;
    Animator anim;
    Vector3 targetDir;

    [Header("����")]
    public float flag = 1f;
    Vector2 direction;
    public float clampAngle = 14;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");

        //�߻��ϴ� �� (�ڵ��ã�� ��� - �ڽ�)
        //firePos = transform.Find("FirePos");

        //�ݺ��ϴ� �Ѿ˹߻�
        InvokeRepeating("CreateBullet", startFire, Delay);
    }

    void Update()
    {
        Rotation();
        Move();
    }

    void CreateBullet()
    {
        //�Ѿ˻���
        Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
    }

    void Move()
    {
        float h = 0;
        float v = 0;

        //�Ʒ��̵�
        if (transform.position.y >= 2)
        {
            v = -1 * speed * Time.deltaTime;
        }

        else
        {
            //�¿��̵� (��������)
            /*if (transform.position.x >= 0.75f || transform.position.x <= -0.75f)
            {
                flag *= -1;
            }
            h = flag * speed * Time.deltaTime;*/
        }

        Vector3 dir = new Vector3(h, v, 0);
        transform.position += dir;
        //transform.Translate(h, v, 0);

        //(����) ������ٵ�� ���󰡰� ? // �ٸ� �ܰ� ������?
        /*if (hp < 70)
        {
            //����
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        if (hp < 50)
        {
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }*/
    }

    //���Ѱ��ֱ�

    //�÷��̾� �������� �ٶ󺸱�
    void Rotation()
    {
        if (target != null)
        {
            direction = new Vector2(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //���Ѱ���� -> �������� ���� ����
            //float angleClamped = Mathf.Clamp(angle, -clampAngle, clampAngle);

            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
    }

    //�÷��̾� ���ݿ� ���� ������
    public void Damage(int attack)
    {
        //���ݹ����� �����̱�
        hp -= attack;
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ColorChange", 0.1f);

        if (hp < 0)
        {
            if(!isEffect)
            {
                isEffect = true;

                DestroyEffect();

                //��������
                LHS_AudioManager.instance.MonsterDie();
                LHS_GameManager.instance.ScoreAdd(addScore);
                
                // ��������
            }
        }
    }

    void DestroyEffect()
    {
        GameObject go = Instantiate(effectfab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾�� �ױ�
        if (collision.gameObject.CompareTag("Player"))
        {
            //�÷��̾� ü�� ���
            collision.gameObject.GetComponent<LHS_Player2Move>().Damage(attack);
            Debug.Log("�÷��̾� �浹");
        }
    }


    void ColorChange()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
