using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build3Controller : MonoBehaviour
{
    [SerializeField] private Image resource1;
    [SerializeField] private Image resource2;
    [SerializeField] private Image resource3;
    [SerializeField] private Text build3Text;
    [SerializeField] private Text finishText;
    [SerializeField] private PlayerController playerRes;

    private bool textVisible;
    private float timeGame;

    void Start()
    {
        resource1.fillAmount = 0f;
        resource2.fillAmount = 0f;
        resource3.fillAmount = 0f;
    }

    private void Update()
    {
        timeGame += Time.deltaTime;
        finishText.text = "Благодарим Вас за полное прозиводство <color=magenta>ресурса</color> за <color=magenta>" + timeGame + "</color> секунд, необходимого для выживания";
        ProduceRes3();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(playerRes.checkerRes1 != CollectState.NONE && playerRes.checkerRes2 != CollectState.NONE)
            {
                resource1.fillAmount += Mathf.Lerp(0f, 1f, Time.deltaTime / 10);
                resource2.fillAmount += Mathf.Lerp(0f, 1f, Time.deltaTime / 10);
            }
        }
    }

    private void ProduceRes3()
    {
        resource3.fillAmount += Mathf.Lerp(0f, resource1.fillAmount + resource2.fillAmount, Time.deltaTime / 45);

        if (resource3.fillAmount != 1f)
        {
            resource1.fillAmount = Mathf.Lerp(resource1.fillAmount, 0f, Time.deltaTime / 10);
            resource2.fillAmount = Mathf.Lerp(resource2.fillAmount, 0f, Time.deltaTime / 10);
        }

        if(resource1.fillAmount > 0.05f && resource2.fillAmount > 0.05f)
            textVisible = false;

        if (resource1.fillAmount < 0.05f && resource2.fillAmount < 0.05f)
        {
            if (!textVisible)
            {
                build3Text.gameObject.SetActive(true);
                Invoke("OffVisibleText", 2f);
            }

            textVisible = true;
        }

        if (resource3.fillAmount == 1f)
        {
            OffVisibleText();
            finishText.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OffVisibleText()
    {
        build3Text.gameObject.SetActive(false);
    }
}
