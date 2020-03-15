using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    private Image clock;
    public Color cooldownColor = Color.green;
    public bool isReady = false;
    private float counter;
    public float cooldown = 10;

    // Start is called before the first frame update
    void Start()
    {
        clock = this.GetComponent<Image>();
        clock.color = cooldownColor;
        counter = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
       if (counter < 0)
       {
            isReady = true;
            counter = 0;
            clock.fillAmount = 0;
       }
       else if (counter > 0)
       {
            counter -= Time.deltaTime;
            clock.fillAmount = Mathf.Max((counter / cooldown), 0);
       }
    }

    public void Activate()
    {
        if (isReady)
        {
            isReady = false;
            counter = cooldown;
            clock.fillAmount = 1;
        }
    }


}
