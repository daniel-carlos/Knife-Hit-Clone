using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Rigidbody2D rb2;
    public Collider2D mainCollider;

    public bool sleep = true;
    public bool dead = false;

    private GameplayController gameplay;
    

    [ContextMenu("Throw")]
    public void ThrowKnife(float throwSpeed, Transform spawnpoint, GameplayController gameplay = null)
    {
        if (dead) return;
        transform.parent = null;
        sleep = false;
        this.transform.position = spawnpoint.position;
        rb2.gravityScale = 0f;
        rb2.velocity = throwSpeed * Vector2.up;
        this.gameplay = gameplay;
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
        if(gameplay != null){
            gameplay.OnKnifeHitLog(this);
        }
    }

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
            gameplay.OnDefeat();
        }
    }
}
