using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLog : MonoBehaviour
{
    public float explosionPower = 3f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.velocity = Random.insideUnitSphere * explosionPower;
            rb.AddTorque(Random.insideUnitSphere, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
