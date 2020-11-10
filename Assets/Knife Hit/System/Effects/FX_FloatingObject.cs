using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_FloatingObject : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ThrowMe", Random.Range(0.1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameInvisible()
    {
        Invoke("ThrowMe", Random.Range(0.1f, 3f));
    }

    private void OnDisable()
    {
        CancelInvoke("ThrowMe");
    }

    private void OnDestroy()
    {
        CancelInvoke("ThrowMe");
    }

    void ThrowMe()
    {
        // yield return new WaitForSeconds(Random.Range(0f, time));
        Vector3 origin = Random.onUnitSphere;
        origin.z = 0f;
        origin = origin.normalized * 2;

        Vector3 destination = Random.onUnitSphere;
        origin.z = 0f;
        origin = origin.normalized * 12;

        Debug.DrawLine(origin, destination, Color.red, 2f);
        transform.position = origin;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = (destination - origin).normalized * speed;
        rb.angularVelocity = Random.Range(-45, 45);
    }
}
