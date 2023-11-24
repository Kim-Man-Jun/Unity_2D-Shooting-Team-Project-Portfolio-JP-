using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int HP = 100;
    public float Speed = 5f;
    public float Delay = 1f;
    public Transform ms;
    public Transform ms2;
    public GameObject bullet;
    public static int RandomPos;

    public GameObject BoomEffect;

    public GameObject Item = null;

    public float Direction;     //1�� ��� ������, -1�� ��� ����
    float DirectionNum = 0;  //1, -1�� ����  
    public float MovingTime;    //enmeystart�� ���۵ǰ� ���� �󸶳� �������� üũ
    float culTime = 0;

    public bool EnemyStart = false;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateBullet", 0.5f, Delay);
        animator = GetComponent<Animator>();
    }

    void Add()
    {
        culTime += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //�� �¿�� ������
        if (EnemyStart == true)
        {
            if (Direction == 1)
            {
                Add();

                if (culTime >= MovingTime)
                {
                    DirectionNum = 1;
                    animator.Play("Enemy_Soldier_Right");
                    culTime = 0;
                }
            }

            if (Direction == -1)
            {
                Add();

                if (culTime >= MovingTime)
                {
                    DirectionNum = -1;
                    animator.Play("Enemy_Soldier_Left");
                    culTime = 0;
                }
            }
        }

        //�� �Ϲ����� �̵�
        transform.Translate(DirectionNum * Time.deltaTime, -Speed * Time.deltaTime, 0);

        //���� �� ������ ������ ����
        if (gameObject.transform.position.x > 2.8f)
        {
            Destroy(gameObject);
        }

        if (gameObject.transform.position.x < -2.8f)
        {
            Destroy(gameObject);
        }

        if (gameObject.transform.position.y < -25.5f)
        {
            Destroy(gameObject);
        }
    }

    //�Ϲݸ� �ѽ��
    void CreateBullet()
    {
        if (EnemyStart == true)
        {
            if (ms != null)
            {
                Instantiate(bullet, ms.position, Quaternion.identity);
            }

            if (ms2 != null)
            {
                Instantiate(bullet, ms2.position, Quaternion.identity);
            }
        }
    }

    //�Ϲݸ� ������ ó��
    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    //�Ϲݸ� �װ� �� �� ����
    private void OnDestroy()
    {
        GameObject go = Instantiate(BoomEffect, transform.position, Quaternion.identity);
        Destroy(go, 0.5f);

        

        if (Item != null)
        {
            Instantiate(Item, transform.position, Quaternion.identity);
        }
    }
}
