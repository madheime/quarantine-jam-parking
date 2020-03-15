using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public Cooldown cooldown;
    private KeyCode dashKey = KeyCode.E;
    public float dashCountdown = 6;
    public float dashForce = 70f;

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
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
    }
}