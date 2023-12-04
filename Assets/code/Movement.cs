using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private float _dashSpeed = 20;
    private float _dashCooldown = 0;

    private Vector3 _input;

    private void Awake() {
        
        _rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        GatherInput();
        Dashing();
        Look();
        
    }

    private void Dashing()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            if(_dashCooldown <= 0) { 
                _rb.AddForce(transform.forward * _dashSpeed, ForceMode.Impulse);
                _dashCooldown = 2f;
            }
        }
    }

    private void countDown()
    {
        if(_dashCooldown > 0)
        {
            _dashCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
        countDown();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}




