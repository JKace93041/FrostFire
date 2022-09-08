using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletDecal;
    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private float timeToDestory = 3f;


    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestory);

    }
    private void OnDisable()
    {
        
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position,target) < .01f) // checks for if their is a hit to save time
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0); //retrives all the points hit and stops at the first one
        GameObject.Instantiate(bulletDecal, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal)); //spawns the object in relation to a normal so it faces the right way 
        Destroy(gameObject);
    }


}
