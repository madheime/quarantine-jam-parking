using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    private Rigidbody rb;
    public KeyCode flipKey = KeyCode.Q;
    public Cooldown cooldown;
    public float flipForceUp = 25f;
    public float flipForceBack = -1.05f;
    public float flipCameraSpeedup = 15f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown.isReady && Input.GetKeyDown(flipKey))
        {
            FlipPlayer();
            cooldown.Activate();
        }

    }

    void FlipPlayer()
    {
        rb.AddTorque(new Vector3(0f, 250f, 0f));
        StartCoroutine(FlipRoutine());
        
    }

    IEnumerator FlipRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        rb.angularVelocity = Vector3.zero;
    }


}
