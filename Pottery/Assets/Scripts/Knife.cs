using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float hitDamage;
    [SerializeField] private Wood wood;

    [SerializeField] private ParticleSystem woodFx;
    private ParticleSystem.EmissionModule woodFxEmission;
    private Rigidbody knifeRb;
    private Vector3 movementVector;
    private bool isMoving = false;
    void Start()
    {
        knifeRb = GetComponent<Rigidbody>();
        woodFxEmission = woodFx.emission;
    }


    void Update()
    {
        isMoving = Input.GetMouseButton(0);
        if(isMoving)
        {
            movementVector = new Vector3(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"),0) * movementSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate() 
    {
        if(isMoving)
        {
            knifeRb.position += movementVector; 
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        Coll coll = other.collider.GetComponent<Coll>();
        if(coll != null)
        {
            //hit collider
            woodFxEmission.enabled = true;
            woodFx.transform.position = other.contacts[0].point;
            
            coll.HitCollider(hitDamage);
            wood.Hit(coll.index,hitDamage);
        }    
    }

    private void OnCollisionExit(Collision other) 
    {
        woodFxEmission.enabled = false;    
    }
}
