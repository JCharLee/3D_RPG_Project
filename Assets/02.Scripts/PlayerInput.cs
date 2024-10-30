using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Keyboard")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;

    [Header("Mouse")]
    public bool leftClick = false;
    public bool leftDrag = false;
    public bool rightClick = false;
    public bool rightDrag = false;

    float clickTime = 0f;

    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        jump = Input.GetButtonDown("Jump");

        MouseClick();
    }

    void MouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            clickTime += Time.deltaTime;
            if (clickTime > 0.2f)
                leftDrag = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftDrag = false;
            if (clickTime < 0.2f)
                StartCoroutine(Click(0));
            clickTime = 0f;
        }

        if (Input.GetMouseButton(1))
        {
            clickTime += Time.deltaTime;
            if (clickTime > 0.2f)
                rightDrag = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightDrag = false;
            if (clickTime < 0.2f)
                StartCoroutine(Click(1));
            clickTime = 0f;
        }

        Cursor.lockState = leftDrag || rightDrag ? CursorLockMode.Locked : CursorLockMode.None;
    }

    IEnumerator Click(int num)
    {
        if (num == 0)
            leftClick = true;
        else
            rightClick = true;

        yield return new WaitForSeconds(clickTime);

        leftClick = false;
        rightClick = false;
    }
}