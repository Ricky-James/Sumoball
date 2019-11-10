using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))] //Player input
public class player3Camera : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public InputManager input;

    // Start is called before the first frame update
    void Start()
    {
        if (input == null) input = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position;

        this.transform.LookAt(target);

       /// target.Rotate(this.transform.position, input.direction() * 10);

    }
}
