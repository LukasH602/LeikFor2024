using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Vector2 = UnityEngine.Vector2;


public class PlayerController : MonoBehaviour
{

    public static event Action OnCollected;

    // Variables
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;

    public float speed = 3.0f;

    public GameObject projectilePrefab;

    public TMP_Text UIHandler;
    public TMP_Text scoretext;
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;
    Vector2 moveDirection = new Vector2(1, 0);

    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        // animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

        // þetta er tekið frá verkefni 4
        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }


        if (currentHealth == 0){
            SceneManager.LoadScene("dead scene", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        
    }


    // tekið frá verkefni 4
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Death"){
            SceneManager.LoadScene("dead scene", LoadSceneMode.Single);
            data.score -= 1;
            //scoretext.text = "Score: "+ data.score;
        }
        if (other.gameObject.tag == "End"){
            SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
            data.score -= 1;
        }
        if (other.gameObject.tag == "food"){
            ChangeHealth(1);
            Destroy(other.gameObject);
        }
        // if (other.gameObject.tag == "gem"){
        //    OnCollected?.Invoke();
        //    Destroy(other.gameObject);
        // }
        // vinnur ef þú snertir þetta
        if (other.gameObject.tag == "end"){
            SceneManager.LoadScene("end scene", LoadSceneMode.Single);
        }
        
    }

    // tekið frá verk 4
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.1f, UnityEngine.Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, -1, maxHealth);
        UIHandler.text = "Health: "+currentHealth;
    }

}