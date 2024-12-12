using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float speed = 4000f;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("skjOtttttttta");       
           
            //GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            GameObject instBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidbody.AddForce(transform.forward * speed);
            Destroy(instBullet, 0.5f);//kúl eytt eftir 0.5sek
           
        }
    }
}
