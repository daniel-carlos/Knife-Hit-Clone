using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int value = 2;
    public GameObject splittedFruitPrefab;
    private GameplayController gameplay;

    // Start is called before the first frame update
    void Start()
    {
        gameplay = FindObjectOfType<GameplayController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Knife"){
            gameplay.CollectFruit(value);

            GameObject splitted = Instantiate(splittedFruitPrefab, transform.position, transform.rotation);
            Rigidbody2D[] parts = splitted.GetComponentsInChildren<Rigidbody2D>();
            parts[0].velocity = transform.right;
            parts[1].velocity = -transform.right;

            Destroy(gameObject);
        }
    }
}
