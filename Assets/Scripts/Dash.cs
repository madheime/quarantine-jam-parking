using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private Vector3 vel;
    public Cooldown cooldown;
    private KeyCode dashKey = KeyCode.E;
    public float dashForce = 200f;
    public float dashDuration = 0.15f;

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
        vel = rb.velocity;
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        StartCoroutine(DashRoutine());
    }

    IEnumerator DashRoutine()
    {
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = vel;
    }
}