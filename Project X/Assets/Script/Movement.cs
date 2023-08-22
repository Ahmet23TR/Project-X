using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables


    [SerializeField] float mainThrust = 100f ;
    [SerializeField] float rotationThrust = 1f ;
    [SerializeField] AudioClip mainEngine;    
    [SerializeField] ParticleSystem mainBoost1;
    [SerializeField] ParticleSystem mainBoost2;
    [SerializeField] ParticleSystem mainBoost3;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }
   
    void Update()
    { 
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    { 
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoost1.isPlaying)
        {
            mainBoost1.Play();
            mainBoost2.Play();
            mainBoost3.Play();
        }
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainBoost1.Stop();
        mainBoost2.Stop();
        mainBoost3.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(-rotationThrust);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(rotationThrust);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    void StopRotating()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; 
    }
}
 