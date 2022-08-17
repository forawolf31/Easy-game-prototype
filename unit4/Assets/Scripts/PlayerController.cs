using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    public float powerupStrength = 15;
    public float speed = 5.0f;
    
    public GameObject powerupIndicator;
    public bool hasPowerup;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();   
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdoenRoutine());
        }
    }

    IEnumerator PowerupCountdoenRoutine()
    {
        yield return new WaitForSeconds(6);
        hasPowerup = false; 
        powerupIndicator.gameObject.SetActive(false);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy") && hasPowerup)
        {
           Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
           Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized; 
            
            
            Debug.Log("Collided with:  " + collision.gameObject.name + " with power up set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }   
    }
}
