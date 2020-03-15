using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 60.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            AudioListener.volume = 1 - AudioListener.volume;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
