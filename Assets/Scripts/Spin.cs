using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject focalPoint;
    private Rigidbody rb_camera;
    public KeyCode spinKey = KeyCode.Q;
    public Cooldown cooldown;
    public float spinTorque = 350f;
    public float spinDuration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        rb_camera = focalPoint.GetComponent<Rigidbody>();
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
        rb_camera.AddTorque(new Vector3(0f, spinTorque, 0f));
        rb.AddForceAtPosition((-1.1f * spinTorque) * transform.right, new Vector3(0, 2, 4.75f));
        rb.AddRelativeForce( (7.5f * spinTorque) * Vector3.left , ForceMode.Force);
        StartCoroutine(SpinRoutine());        
    }

    IEnumerator SpinRoutine()
    {
        yield return new WaitForSeconds(spinDuration);
        rb_camera.angularVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddRelativeForce((1.5f * spinTorque) * Vector3.forward, ForceMode.Force);
        rb.AddRelativeForce((2.0f * spinTorque) * Vector3.left);
    }


}
