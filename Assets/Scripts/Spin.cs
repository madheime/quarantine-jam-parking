using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private Rigidbody rb;
    public KeyCode spinKey = KeyCode.Q;
    public Cooldown cooldown;
    public float spinTorque = 350f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown.isReady && Input.GetKeyDown(spinKey))
        {
            SpinPlayer();
            cooldown.Activate();
        }

    }

    void SpinPlayer()
    {
        rb.AddTorque(new Vector3(0f, spinTorque, 0f));
        rb.AddForce(spinTorque * Vector3.left , ForceMode.Force);
        StartCoroutine(SpinRoutine());        
    }

    IEnumerator SpinRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        rb.angularVelocity = Vector3.zero;
    }


}
