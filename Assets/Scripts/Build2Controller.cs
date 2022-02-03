using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build2Controller : MonoBehaviour
{
    public bool build2Checker;
    public bool build2Stopper;

    [SerializeField] private Image resource1;
    [SerializeField] private Image resource2;
    [SerializeField] private PlayerController playerRes;
    [SerializeField] private Text[] build2Text;

    private bool textVisible, text1Visible;

    private int textHelper;

    void Start()
    {
        resource1.fillAmount = 0f;
        resource2.fillAmount = 0f;

        build2Checker = false;
        build2Stopper = false;

        textVisible = text1Visible = false;
    }

    private void Update()
    {
        ProduceRes2();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(playerRes.checkerRes1 != CollectState.NONE)
                resource1.fillAmount += Mathf.Lerp(0f, 1f, Time.deltaTime/5);

            if(playerRes.checkerRes2 != CollectState.FULL)
                resource2.fillAmount = Mathf.Lerp(resource2.fillAmount, 0f, Time.deltaTime/2);
        }
    }

    private void ProduceRes2()
    {
        resource2.fillAmount += Mathf.Lerp(0f, resource1.fillAmount, Time.deltaTime / 5);

        //for full resource of build2 to stop dropping resource
        if (resource2.fillAmount != 1f)
        {
            resource1.fillAmount = Mathf.Lerp(resource1.fillAmount, 0f, Time.deltaTime / 2);
            textVisible = false;
        }

        if(resource1.fillAmount > 0.05f)
            text1Visible = false;

        if (resource2.fillAmount < 0.05f)
            build2Checker = false;
        else
            build2Checker = true;

        if (resource1.fillAmount == 1f && resource2.fillAmount == 1f)
        {
            build2Stopper = true;
        }    
        else
        {
            build2Stopper = false;
        }

        if (resource2.fillAmount == 1f)
        {
            textHelper = 0;

            if (!textVisible)
            {
                build2Text[textHelper].gameObject.SetActive(true);
                Invoke("OffVisibleText", 2f);
            }

            textVisible = true;
        }

        if(resource1.fillAmount < 0.05f)
        {
            textHelper = 1;

            if (!text1Visible)
            {
                build2Text[textHelper].gameObject.SetActive(true);
                Invoke("OffVisibleText", 2f);
            }
            text1Visible = true;
        }
    }

    private void OffVisibleText()
    {
        build2Text[textHelper].gameObject.SetActive(false);
    }
}
