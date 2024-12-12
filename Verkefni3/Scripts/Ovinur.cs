using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;
using TMPro;

public class Ovinur : MonoBehaviour
{
    public static int health = 30;
    public Transform player;
    private  TextMeshProUGUI texti;
    private Rigidbody rb;
    private Vector3 movement;
    public float hradi = 5f;
    // Start is called before the first frame update
    void Start()
    {
        texti= GameObject.Find("Text2").GetComponent<TextMeshProUGUI>();
        rb = this.GetComponent<Rigidbody>();
        texti.text = "Líf " + health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 stefna = player.position - transform.position;
        stefna.Normalize();
        movement = stefna;
    }
    private void FixedUpdate()
    {
        Hreyfing(movement);
    }
    void Hreyfing(Vector3 stefna)
    {
        rb.MovePosition(transform.position + (stefna * hradi * Time.deltaTime));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag=="Player")
        {
            Debug.Log("Leikmaður fær í sig óvin");
            TakeDamage(5);
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("health er núna" + (health).ToString());
        health -= damage;
        texti.text = "Líf " + health.ToString();
        if (health <= 0)
        {
            health = 30;
            SceneManager.LoadScene("deyja", LoadSceneMode.Single);
            
        }

    }
    

}
