﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject focalPoint;
    private Rigidbody rb;
    public float speed;
    private bool hasPowerup = false;
    private float powerupStrength = 20.0f;
    public GameObject powerupIndicator;
    //private float indicatorRotateSpeed = 60.0f;
    public AudioClip engineStarting;
    public AudioClip powerupSound;
    private AudioSource playerAudio;
    public AudioClip[] enemyHitSounds;
    public AudioClip poweredHit;
    public Text gameOverText;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        focalPoint = GameObject.Find("Focal Point");
        rb = GetComponent<Rigidbody>();
        playerAudio.PlayOneShot(engineStarting, 1.0f);
    }

    void Update()
    {
        transform.rotation = focalPoint.transform.rotation;
        float forwardInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position +
            new Vector3(0, -0.5f, 0);
        if (transform.position.y < -5)
        {
            GameObject spawnManager = GameObject.Find("SpawnManager");
            SpawnManager waveInfo = spawnManager.GetComponent<SpawnManager>();
            gameOverText.text = "Game Over\nYou survived " + (waveInfo.waveNumber - 1);
            if (waveInfo.waveNumber == 2)
            {
                gameOverText.text += " wave.\nPress 'R' to Restart";
            } else
            {
                gameOverText.text += " waves.\nPress 'R' to Restart";
            }
                
            gameOverText.gameObject.SetActive(true);
            focalPoint.GetComponent<Rigidbody>().AddTorque(new Vector3(0f, 50f, 0f));
            spawnManager.SendMessage("GameOver");
            Destroy(gameObject);
        }
        //powerupIndicator.transform.Rotate(Vector3.up, indicatorRotateSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            playerAudio.PlayOneShot(powerupSound, 1.0f);
        } else if (other.CompareTag("Boost"))
        {
            rb.AddForce(other.transform.forward * 100.0f, ForceMode.VelocityChange);
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupSound, 1.0f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerup)
            {
                playerAudio.PlayOneShot(poweredHit, 1.0f);
                Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position -
                    transform.position).normalized;
                enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            } else
            {
                int hitSoundIndex = Random.Range(0, enemyHitSounds.Length);
                playerAudio.PlayOneShot(enemyHitSounds[hitSoundIndex], 1.0f);
            }
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

}
