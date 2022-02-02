using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CollectState
{
    NONE,
    COLLECT,
    FULL
}

public class PlayerController : MonoBehaviour
{
    public CollectState checkerRes1;
    public CollectState checkerRes2;

    [SerializeField] private float m_Speed;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform positionResource1;
    [SerializeField] private Transform positionResource2;
    [SerializeField] private Image playerResource1;
    [SerializeField] private Image playerResource2;
    [SerializeField] private Build2Controller build2Res;

    private float moveInputX;
    private float moveInputZ;

    private CharacterController characterController;

    private Vector3 _tempPos;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        playerResource1.fillAmount = 0f;
        playerResource2.fillAmount = 0f;

        checkerRes1 = CollectState.NONE;
        checkerRes2 = CollectState.NONE;
    }

    void FixedUpdate()
    {
        moveInputX = joystick.Horizontal;
        moveInputZ = joystick.Vertical;
        characterController.Move(new Vector3( moveInputX * m_Speed, 0, moveInputZ * m_Speed));
    }

    private void Update()
    {
        //for UI player resources moving with player
        _tempPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        positionResource1.position = new Vector3(_tempPos.x - 10f, _tempPos.y, 0f);
        positionResource2.position = new Vector3(_tempPos.x + 10f, _tempPos.y, 0f);

        //for State player resource1
        if (playerResource1.fillAmount < 1f && playerResource1.fillAmount > 0f)
            checkerRes1 = CollectState.COLLECT;
        if (playerResource1.fillAmount == 1f)
            checkerRes1 = CollectState.FULL;
        if (playerResource1.fillAmount < 0.05f)
            checkerRes1 = CollectState.NONE;

        //for State player resource2
        if (playerResource2.fillAmount < 1f && playerResource2.fillAmount > 0f)
            checkerRes2 = CollectState.COLLECT;
        if (playerResource2.fillAmount == 1f)
            checkerRes2 = CollectState.FULL;
        if (playerResource2.fillAmount < 0.05f)
            checkerRes2 = CollectState.NONE;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Build1")
        {
            if (checkerRes1 != CollectState.FULL)
                playerResource1.fillAmount += Mathf.Lerp(0f, 1f, Time.deltaTime/2);
        }

        if(other.gameObject.tag == "Build2")
        {
            if(checkerRes1 != CollectState.NONE && !build2Res.build2Stopper)
                playerResource1.fillAmount = Mathf.Lerp(playerResource1.fillAmount, 0f, Time.deltaTime/2);

            if(checkerRes2 != CollectState.FULL && build2Res.build2Checker)
                playerResource2.fillAmount += Mathf.Lerp(0f, 1f, Time.deltaTime/5);
        }

        if(other.gameObject.tag == "Build3")
        {
            if(checkerRes1 != CollectState.NONE && checkerRes2 != CollectState.NONE)
            {
                playerResource1.fillAmount = Mathf.Lerp(playerResource1.fillAmount, 0f, Time.deltaTime / 2);
                playerResource2.fillAmount = Mathf.Lerp(playerResource2.fillAmount, 0f, Time.deltaTime / 2);
            }
        }
    }
}
