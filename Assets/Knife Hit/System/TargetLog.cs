using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLog : MonoBehaviour
{
    private bool rotate = true;

    [Header("Starting Configuration")]
    public float logRadius;
    public float fruitRadius;

    [Header("Level")]
    public GameplayLevel gameplayLevel;

    [Header("Physics")]
    public Rigidbody2D rb2;

    [Header("Containers")]
    public GameObject stabbedKnifePrefab;
    public GameObject fruitPrefab;

    [Header("FX")]
    public GameObject sparkPrefab;
    public float sparkLifetime = 2f;
    public BrokenLog brokenLogPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (rb2 == null) rb2 = GetComponent<Rigidbody2D>();


    }

    public void SetupStartingKnifes(int startingKnifes)
    {
        //Listar os angulos disponíveis
        List<float> availableAngles = new List<float>();
        for (int i = 0; i < 360; i += 5)
        {
            availableAngles.Add(i);
        }

        float[] knifeLocations = new float[startingKnifes];
        for (int i = 0; i < startingKnifes; i++)
        {
            int selectedAngle = Random.Range(0, availableAngles.Count);
            knifeLocations[i] = availableAngles[selectedAngle];
            availableAngles.RemoveAt(selectedAngle);
        }

        int fruitAmount = Random.Range(0, 3);
        float[] fruitLocations = new float[fruitAmount];

        for (int i = 0; i < fruitAmount; i++)
        {
            int selectedAngle = Random.Range(0, availableAngles.Count);
            fruitLocations[i] = availableAngles[selectedAngle];
            availableAngles.RemoveAt(selectedAngle);
        }



        for (int i = 0; i < knifeLocations.Length; i++)
        {
            GameObject startingStabbed = Instantiate(stabbedKnifePrefab,
                transform.position + Vector3.down * logRadius,
                Quaternion.identity, transform);
            float angle = knifeLocations[i];
            startingStabbed.transform.RotateAround(transform.position, Vector3.forward, angle);
        }
        for (int i = 0; i < fruitLocations.Length; i++)
        {
            GameObject startingStabbed = Instantiate(fruitPrefab,
                transform.position + Vector3.down * fruitRadius,
                Quaternion.identity, transform);
            float angle = fruitLocations[i];
            startingStabbed.transform.RotateAround(transform.position, Vector3.forward, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            rb2.angularVelocity = gameplayLevel.angularSpeed * gameplayLevel.angularSpeedCurve.Evaluate(Time.time / gameplayLevel.angularSpeedSmooth % 1f);
        }
        else
        {
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

            GetComponent<Animator>().SetTrigger("hit");
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

    internal void SetupStartingFruits(float[] startingFruits)
    {
        for (int i = 0; i < startingFruits.Length; i++)
        {
            GameObject fruit = Instantiate(fruitPrefab,
                transform.position + Vector3.down * fruitRadius,
                Quaternion.identity, transform);
            float angle = startingFruits[i];
            fruit.transform.RotateAround(transform.position, Vector3.forward, angle);
        }
    }
}
