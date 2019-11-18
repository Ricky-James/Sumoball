using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTarget : MonoBehaviour
{
    public Transform playerTarget;

    private float rotSpeed = 1.5f;
    private float distance = 10f;

    float xPos, zPos;

    private float timer = 0;

    private Vector3 targetOffset = new Vector3(-10, 1.5f, -10);


    void Update()
    {

        float direction = Input.GetAxisRaw("Horizontal");
        timer -= Time.deltaTime * rotSpeed * direction;


        xPos = Mathf.Cos(timer) * distance;
        zPos = Mathf.Sin(timer) * distance;
        targetOffset = new Vector3(xPos, 1.5f, zPos);


        transform.localPosition = playerTarget.position + targetOffset;



     
    }


}
