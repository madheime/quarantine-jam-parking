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
    public GameObject spinEffect;
    public float spinTorque = 350f;
    public float spinDuration = 0.2f;
    public float explosionRadius = 12f;
    public float explosionForce = 15f;

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
        rb.AddRelativeForce(((1.0f * spinTorque) * Vector3.back));
        
        rb_camera.AddTorque(new Vector3(0f, spinTorque, 0f));
        StartCoroutine(SpinRoutine());
        spinEffect.gameObject.SetActive(true);
        SpinExplode();
    }

    IEnumerator SpinRoutine()
    {
        yield return new WaitForSeconds(spinDuration);
        spinEffect.gameObject.SetActive(false);
        rb_camera.angularVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void SpinExplode()
    {
        Collider[] objects = UnityEngine.Physics.OverlapSphere(rb.position, explosionRadius);
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();
            if (r != null && !Rigidbody.Equals(r, rb))
            {
                r.AddExplosionForce(explosionForce, rb.position, explosionRadius);
            }
        }
    }
    
}
