using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEngine.GraphicsBuffer;

public class LHS_Boss : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] float speed = 1;

    [Header("발사")]     //※ 애니메이션마다 다른 위치에서 나오는?
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePos;
    [SerializeField] float startFire = 5f;
    [SerializeField] float Delay = 1f;

    [Header("data")] //공격력이 필요? -> 모든 적들을 위해?
    [SerializeField] int hp = 100;
    public int attack = 20;
    public int addScore = 5;

    [Header("효과")]
    public GameObject effectfab;
    bool isEffect = false;

    //애니메이션을 위한
    GameObject target;
    Animator anim;
    Vector3 targetDir;

    [Header("보스")]
    public float flag = 1f;
    Vector2 direction;
    public float clampAngle = 14;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");

        //발사하는 곳 (코드로찾는 방법 - 자식)
        //firePos = transform.Find("FirePos");

        //반복하는 총알발사
        InvokeRepeating("CreateBullet", startFire, Delay);
    }

    void Update()
    {
        Rotation();
        Move();
    }

    void CreateBullet()
    {
        //총알생성
        Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
    }

    void Move()
    {
        float h = 0;
        float v = 0;

        //아래이동
        if (transform.position.y >= 2)
        {
            v = -1 * speed * Time.deltaTime;
        }

        else
        {
            //좌우이동 (하지말기)
            /*if (transform.position.x >= 0.75f || transform.position.x <= -0.75f)
            {
                flag *= -1;
            }
            h = flag * speed * Time.deltaTime;*/
        }

        Vector3 dir = new Vector3(h, v, 0);
        transform.position += dir;
        //transform.Translate(h, v, 0);

        //(보류) 리지드바디로 날라가게 ? // 다른 단계 켜지게?
        /*if (hp < 70)
        {
            //삭제
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        if (hp < 50)
        {
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }*/
    }

    //제한각주기

    //플레이어 방향으로 바라보기
    void Rotation()
    {
        if (target != null)
        {
            direction = new Vector2(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //제한각잡기 -> 뒤집히는 현상 문제
            //float angleClamped = Mathf.Clamp(angle, -clampAngle, clampAngle);

            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
    }

    //플레이어 공격에 따른 데미지
    public void Damage(int attack)
    {
        //공격받으면 깜빡이기
        hp -= attack;
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ColorChange", 0.1f);

        if (hp < 0)
        {
            if(!isEffect)
            {
                isEffect = true;

                DestroyEffect();

                //오디오재생
                LHS_AudioManager.instance.MonsterDie();
                LHS_GameManager.instance.ScoreAdd(addScore);
                
                // 게임종료
            }
        }
    }

    void DestroyEffect()
    {
        GameObject go = Instantiate(effectfab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어면 죽기
        if (collision.gameObject.CompareTag("Player"))
        {
            //플레이어 체력 깎기
            collision.gameObject.GetComponent<LHS_Player2Move>().Damage(attack);
            Debug.Log("플레이어 충돌");
        }
    }


    void ColorChange()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
