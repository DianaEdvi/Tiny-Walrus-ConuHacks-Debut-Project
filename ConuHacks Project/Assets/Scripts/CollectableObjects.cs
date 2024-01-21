using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjects : MonoBehaviour
{

    private int count = 0;
    public int numberOfObjects = 0;

    // Floating parameters
    public float floatHeight = 0.5f; // Adjust this value to control the float height
    public float floatSpeed = 1f;    // Adjust this value to control the float speed

    private void Update()
    {
        // Make the collectible object float up and down
        FloatObject();
    }

    private void FloatObject()
    {
        // Calculate the new Y position based on a sine wave
        float positionChange = transform.position.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, positionChange, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Call the function to handle the collection
            Collect();
        }
    }

    private void Collect()
    {
        gameObject.SetActive(false);
        count++;

        if(count == numberOfObjects){
            //level/prototype complete, game over 
        }


    }
}
