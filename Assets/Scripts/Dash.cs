using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public Cooldown cooldown;
    public KeyCode dashKey = KeyCode.W;
    public float dashCountdown = 6;
    public float dashDistance = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown.isReady && Input.GetKeyDown(dashKey))
        {
            DashPlayer();
            cooldown.Activate();
        }

    }

    void DashPlayer()
    {
        rb.position += Vector3.up + Vector3.ProjectOnPlane(rb.velocity, Vector3.up) * dashDistance;
    }
}