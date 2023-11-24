using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnegyBoltControl : MonoBehaviour
{
    public GameObject Target_Player;

    public float Speed = 5f;
    public int Power = 10;

    Vector2 dir;
    Vector2 dirNo;
    public GameObject BoomEffect;
    Rigidbody2D rb;
    AudioSource EnegyBoltSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnegyBoltSound = GetComponent<AudioSource>();

        Target_Player = GameObject.FindGameObjectWithTag("Player");
        dir = Target_Player.transform.position - transform.position;
        dirNo = dir.normalized;

        rb.AddForce(dirNo * Speed * Time.deltaTime, ForceMode2D.Impulse);

        EnegyBoltSound.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(Power);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject go = Instantiate(BoomEffect, transform.position, Quaternion.identity);
        Destroy(go, 0.5f);
    }
}
