using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLog : MonoBehaviour
{
    private bool rotate = true;

    [Header("Starting Configuration")]
    public float logRadius;

    [Header("Level")]
    public GameplayLevel gameplayLevel;

    [Header("Physics")]
    public Rigidbody2D rb2;

    [Header("Containers")]
    public GameObject stabbedKnifePrefab;

    [Header("FX")]
    public GameObject sparkPrefab;
    public float sparkLifetime = 2f;
    public BrokenLog brokenLogPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (rb2 == null) rb2 = GetComponent<Rigidbody2D>();


    }

    public void SetupStartingKnifes(float[] startingKnifes)
    {
        for (int i = 0; i < startingKnifes.Length; i++)
        {
            GameObject startingStabbed = Instantiate(stabbedKnifePrefab,
                transform.position + Vector3.down * logRadius,
                Quaternion.identity, transform);
            float angle = startingKnifes[i];
            startingStabbed.transform.RotateAround(transform.position, Vector3.forward, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            rb2.angularVelocity = gameplayLevel.angularSpeed * gameplayLevel.angularSpeedCurve.Evaluate(Time.time / gameplayLevel.angularSpeedSmooth % 1f);
        }else{
            rb2.angularVelocity = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Knife")
        {
            Knife knife = other.gameObject.GetComponent<Knife>();
            if (knife.dead) return;

            knife.SendMessage("OnHitLog");

            knife.gameObject.SetActive(false);
            GameObject stabbedKnife = Instantiate(stabbedKnifePrefab, knife.transform.position, knife.transform.rotation);
            stabbedKnife.transform.parent = transform;

            if (sparkPrefab != null)
            {
                GameObject spark = Instantiate(sparkPrefab,
                    other.contacts[0].point,
                    Quaternion.LookRotation(-other.contacts[0].normal));
                GameObject.Destroy(spark, sparkLifetime);
            }
        }
    }

    public void BreakLog()
    {
        BrokenLog brokenLog = Instantiate(brokenLogPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void StopRotating()
    {
        rotate = false;
    }
}
