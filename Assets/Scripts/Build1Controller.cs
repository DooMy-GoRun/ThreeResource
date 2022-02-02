using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build1Controller : MonoBehaviour
{
    [SerializeField] private Image resource1;
    [SerializeField] private PlayerController playerRes;
    [SerializeField] private Text build1Text;

    private bool textVisible;

    void Start()
    {
        resource1.fillAmount = 0f;

        textVisible = false;
    }

    void Update()
    {
            ProduceRes1();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(playerRes.checkerRes1 != CollectState.FULL)
                resource1.fillAmount = Mathf.Lerp(resource1.fillAmount, 0f, Time.deltaTime / 2);
        }
    }

    private void ProduceRes1()
    {
        if (resource1.fillAmount != 1f)
        {
            resource1.fillAmount += Mathf.Lerp(0f, 1f, Time.deltaTime / 10);
            textVisible = false;
        }

        if (resource1.fillAmount == 1f)
        {
            if(!textVisible)
            {
                build1Text.gameObject.SetActive(true);
                Invoke("OffVisibleText", 2f);
            }

            textVisible = true;
        }
            
    }

    private void OffVisibleText()
    {
        build1Text.gameObject.SetActive(false);
    }
}
