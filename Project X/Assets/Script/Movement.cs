using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 25000 ;
    [SerializeField] float rotationThrust = 5000 ;
    

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame 
   
    void Update()
    { 
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
        }
    } 

    void ProcessRotation()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false; 
    }
}
 