using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Þetta er Projectile klasi sem stjórnar hegðun skota (eða eldflauga) í leiknum.
public class Projectile : MonoBehaviour
{
    // Breyta sem geymir vísun á Rigidbody2D fyrir hreyfingu.
    Rigidbody2D rigidbody2d;

    // Awake keyrir fyrst þegar hlutinn er virkjaður og sækir Rigidbody2D íhlutinn.
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); 
    }

    // Update keyrir í hverri ramma og athugar hvort hlutinn sé of langt frá upphafsstað.
    void Update()
    {
        // Eyðir hlutnum ef hann er kominn yfir 100 einingar frá heimmiðju (til að spara minni).
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    // Aðferð til að skjóta eldflauginni í ákveðna átt með ákveðnum krafti.
    public void Launch(Vector2 direction, float force)
    {
        // Bætir krafti í tiltekna átt við Rigidbody2D svo eldflaugin hreyfist.
        rigidbody2d.AddForce(direction * force);
    }

    // Keyrir þegar eldflaugin rekst á annan hlut (Collider2D).
    void OnTriggerEnter2D(Collider2D other)
    {
        // Reynir að finna EnemyController íhlut á hlutnum sem rekst á.
        EnemyController enemy = other.GetComponent<EnemyController>();

        // Ef hluturinn hefur EnemyController, þá kallar hann á `Fix` aðferðina.
        if (enemy != null)
        {
            enemy.Fix();
        }

        // Eyðir eldflauginni eftir árekstur.
        Destroy(gameObject); 
    }
}