using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drop_Monster_wj : MonoBehaviour
{
    [SerializeField]
    GameObject textBossWarning; //보스등장 오브젝트

    public Transform ms1;
    public Transform ms2;
    public Transform ms3;
    public GameObject Drop_Bullet;
    public GameObject Drop_Ms_Bullet;
    public GameObject Item = null;
    public GameObject DeadBoss; 
    public GameObject FinalBoss1;
    public GameObject BossExplosion;
    //
    public int HP = 350;
    public int drop = 15;
    public int flag = 1;
    public float moveSpeed = 2.5f;
    public float delay = 2.0f;
    //

    public float left_ss = -1f;
    public float right_es = 1f;
    public float StartTime = 1; //시작

    void Start()
    {
        Invoke("CreateBullet", 0.5f);
        Invoke("CreateMS_Bullet", delay); 
    }

    void CreateBullet()
    {
        Instantiate(Drop_Bullet, ms1.transform.position, Quaternion.identity);
        Invoke("CreateBullet", delay);
    }

    void CreateMS_Bullet()
    {
        Instantiate(Drop_Ms_Bullet, ms2.transform.position,Quaternion.identity);
        Instantiate(Drop_Ms_Bullet, ms3.transform.position, Quaternion.identity);
        Invoke("CreateMS_Bullet", delay);
    }


    void Update()
    {
        if (transform.position.x >= 2.0f)
        {
            flag *= -1;
        }
        if (transform.position.x <= -2.0f)
        {
            flag *= -1;
        }

        transform.Translate(flag * moveSpeed * Time.deltaTime, 0, 0);
    }

    public void ItemDrop()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= drop)
        {
            Instantiate(Item, ms1.position, Quaternion.identity);
        }
    }
    public void Damage(int Attack)
    {
        SpawnBoss Spawn = FindObjectOfType<SpawnBoss>();
        HP -= Attack;
        if (HP <= 0)
        {
            Instantiate(BossExplosion, transform.position, Quaternion.identity);
            ItemDrop();
            Destroy(gameObject);
            textBossWarning.SetActive(true);
            Instantiate(FinalBoss1, transform.position, Quaternion.identity);       
        }
    }
        
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
