using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject focalPoint;
    private Rigidbody rb;
    public float speed;
    private bool hasPowerup = false;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    private float indicatorRotateSpeed = 60f;
    public FixedJoystick joystick;

    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical") + joystick.Vertical;
        rb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position +
            new Vector3(0, -0.5f, 0);
        powerupIndicator.transform.Rotate(Vector3.up, indicatorRotateSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position -
                transform.position).normalized;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}
