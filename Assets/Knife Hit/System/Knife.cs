using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Rigidbody2D rb2;
    public Collider2D mainCollider;

    public bool sleep = true;
    public bool dead = false;

    [ContextMenu("Throw")]
    public void ThrowKnife(float throwSpeed)
    {
        if (dead) return;
        rb2.gravityScale = 0f;
        rb2.velocity = throwSpeed * Vector2.up;
    }
    public void ThrowKnife(float throwSpeed, Transform spawnpoint)
    {
        if (dead) return;
        sleep = false;
        this.transform.position = spawnpoint.position;
        rb2.gravityScale = 0f;
        rb2.velocity = throwSpeed * Vector2.up;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (rb2 == null) rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sleep)
        {
            rb2.velocity = Vector2.zero;
            rb2.angularVelocity = 0f;
        }
        mainCollider.enabled = !sleep && !dead;
    }

    void OnHitLog()
    {
        if (dead) return;
        Debug.Log($"I hitted the log!", this);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "StabbedKnife")
    //     {
    //         Debug.Log($"I hitted another knife!", other.gameObject);
    //         dead = true;
    //         rb2.gravityScale = 1f;
    //     }
    // }

    void KillKnife()
    {
        dead = true;
        rb2.gravityScale = 1f;
        rb2.velocity = Vector2.down + Vector2.right * Random.Range(-1f, 1f);
        rb2.angularVelocity = 360f;
        Debug.Log(GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "StabbedKnife")
        {
            Debug.Log($"I hitted another knife!", other.gameObject);
            KillKnife();
        }
    }
}
