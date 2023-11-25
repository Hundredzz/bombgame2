using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    private Vector3 input;

    private void Update()
    {
        GetInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (input != Vector3.zero)
        {
            var relative = (transform.position + input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    private void Move()
    {
        rb.MovePosition(transform.position + (transform.forward * input.magnitude) * speed * Time.deltaTime);
    }
}






