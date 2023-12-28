using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static event Action OnReachedFinishLine;
    private CharacterController playerController;

    [SerializeField] private GameObject _finishLine;

    [Header("Movement Variables")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _rotationSpeed = 120;

    private Vector3 _movement;


    [Header("Animations")]
    private Animator _anim;
    private bool _isRunning;



    private void Awake()
    {
        GetRefrences();

    }


    // Update is called once per frame
    void Update()
    {

        Movement();
        if(transform.position.z >= _finishLine.transform.position.z)
        {
            OnReachedFinishLine();
        }
    }

    private void GetRefrences()
    {
        playerController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    private void Movement()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionZ = Input.GetAxisRaw("Vertical");
        Vector3 _movement = new Vector3(directionX, 0, directionZ);
        _movement.Normalize();

        if (_movement == Vector3.zero)
        {
            _anim.SetBool("_isRunning", false);

        }
        else
        {
            _anim.SetBool("_isRunning", true);

            Quaternion toRotation = Quaternion.LookRotation(_movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        playerController.Move(_movement * _speed * Time.deltaTime);


    }


}
