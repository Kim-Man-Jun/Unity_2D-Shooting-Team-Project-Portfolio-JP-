using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WJ_BossPart1 : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float startWaitTime;
    private float waitTime;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    //
    public float left_ss = -1f;
    public float right_es = 1f;
    public float StartTime = 1; //����
    //
    public int HP = 4000;
    public float Delay = 0.5f;
    public int drop = 90;
    //

    public Transform moveSpot;

    public Transform Head_Pos1;
    public Transform Head_Pos2;
    public Transform arms_Pos1;
    public Transform arms_Pos2;

    public GameObject Head_Bullet;
    public GameObject arms_Bullet;
    public GameObject Item = null;
    public GameObject BossExplosion;
    //
    GameObject Spot;
    Rigidbody2D rb;
    public GameObject FinalBoss2;
    public float SpotSpeed = 1;

    void Start()
    {
        Spot = GameObject.Find("MoveSpot");
        rb = GetComponent<Rigidbody2D>();
        waitTime = startWaitTime;
        Invoke("Hide", 3.5f);
        Invoke("CreateBullet", 5); 
        Invoke("CreateArmsBullet", 10);
    }
    void Hide()
    {
        //���� �ؽ�Ʈ ��ü�̸� �˻��ؼ� ����
        GameObject.Find("TextBossWarning").SetActive(false);
    }
    private void FixedUpdate()
    {
        float dis = Vector3.Distance(Spot.transform.position, transform.position);
        if (dis > 1.2f)
        {
            Vector3 dir = Spot.transform.position - transform.position;
            dir = dir.normalized;
            float vx = dir.x * moveSpeed;
            float vy = dir.y * moveSpeed;
            rb.velocity = new Vector2(vx, vy);
        }
    }
    void CreateBullet()
    {
        Instantiate(Head_Bullet, Head_Pos1.position, Quaternion.identity);
        Instantiate(Head_Bullet, Head_Pos2.position, Quaternion.identity);
        Invoke("CreateBullet", 1.8f) ;
    }

    void CreateArmsBullet()
    {
        Instantiate(arms_Bullet, arms_Pos1.position, Quaternion.identity);
        Instantiate(arms_Bullet, arms_Pos2.position, Quaternion.identity);
        Invoke("CreateArmsBullet", 5f);
    }

    public void Damage(int Attack)
    {
        HP -= Attack;
        if (HP <= 0)
        {
            Instantiate(BossExplosion, transform.position, Quaternion.identity);
            ItemDrop();
            Destroy(gameObject);
            Instantiate(FinalBoss2, transform.position, Quaternion.identity);
        }
    }
    public void ItemDrop()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= drop)
        {
            Instantiate(Item, Head_Bullet.transform.position, Quaternion.identity);
        }
    }
}
