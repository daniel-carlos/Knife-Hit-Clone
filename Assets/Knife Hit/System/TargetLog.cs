using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLog : MonoBehaviour
{
    [Header("Starting Configuration")]
    public float logRadius;

    [Header("Log Rotation")]
    public float angularSpeed = 45f;
    public float angularSpeedSmooth = 5f;

    public AnimationCurve angularSpeedCurve;


    [Header("Physics")]
    public Rigidbody2D rb2;

    [Header("Containers")]
    public GameObject stabbedKnifePrefab;

    [Header("FX")]
    public GameObject sparkPrefab;
    public float sparkLifetime = 2f;

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
        // TODO: apagar isso
        rb2.angularVelocity = angularSpeed * angularSpeedCurve.Evaluate(Time.time / angularSpeedSmooth % 1f);
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
}
