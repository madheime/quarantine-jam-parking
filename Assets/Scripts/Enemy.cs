using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    public float speed = 7;
    public AudioClip deathSound1;
    public AudioClip deathSound2;
    public AudioClip boostSound;
    private AudioSource enemyAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemyAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        transform.LookAt(player.transform, Vector3.up);
        transform.Rotate(Vector3.up, 180);
        rb.AddForce(lookDirection * speed);
        if (transform.position.y < -3)
        {
            if (Random.Range(0, 1f) < 0.5)
            {
                enemyAudio.PlayOneShot(deathSound1, 1.0f);
            }
            else
            {
                enemyAudio.PlayOneShot(deathSound2, 1.0f);
            }
            
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            rb.AddForce(other.transform.forward * 100.0f, ForceMode.VelocityChange);
            Destroy(other.gameObject);
            enemyAudio.PlayOneShot(boostSound, 1.0f);
        }
    }
}
