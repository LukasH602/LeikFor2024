using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Þetta er EnemyController klasi sem stjórnar hegðun óvinar í 2D leik.
public class EnemyController : MonoBehaviour
{
    // Opinberar breytur sem hægt er að stilla í Inspector:
    public float speed; // Hraði óvinar.
    public bool vertical; // Á óvinurinn að hreyfast lóðrétt?
    public float changeTime; // Tíminn á milli þess að óvinur skiptir um stefnu.
    public ParticleSystem smokeEffect; // Reyk-agnakerfi fyrir bilaða óvini.

    // Einkabreyta fyrir innri stjórnun:
    Rigidbody2D rigidbody2d; // Stýrir eðlisfræði óvinarins.
    Animator animator; // Stýrir hreyfivísun óvinarins.
    float timer; // Teljari fyrir skiptingu á stefnu.
    int direction = 1; // Stefna óvinarins, 1 fyrir áfram, -1 fyrir afturábak.
    bool broken = true; // Ástand óvinarins, true ef hann er virkur, false ef hann er bilaður.

    // Upphafsstillingar óvinarins.
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); // Nær í Rigidbody2D íhlut.
        animator = GetComponent<Animator>(); // Nær í Animator íhlut.
        timer = changeTime; // Upphafstillir tímamælinn.
    }

    // Uppfærir tímamæli og skiptir um stefnu þegar tímamælir klárast.
    void Update()
    {
        timer -= Time.deltaTime; // Dregur frá tímamæli.

        if (timer < 0) // Ef tímamælir klárast:
        {
            direction = -direction; // Snýr stefnu við.
            timer = changeTime; // Endurstillar tímamælinn.
        }
    }

    // Uppfærir stöðu óvinarins í hverjum eðlisfræðiramma.
    void FixedUpdate()
    {
        if (!broken) // Ef óvinurinn er bilaður:
        {
            return; // Ekki gera neitt.
        }

        Vector2 position = rigidbody2d.position; // Nær í núverandi stöðu.

        if (vertical) // Ef óvinurinn hreyfist lóðrétt:
        {
            position.y = position.y + speed * direction * Time.deltaTime; // Uppfærir y-hnit.
            animator.SetFloat("Move X", 0); // Stillir lárétt hreyfingu á 0.
            animator.SetFloat("Move Y", direction); // Stillir lóðrétta hreyfingu.
        }
        else // Ef óvinurinn hreyfist lárétt:
        {
            position.x = position.x + speed * direction * Time.deltaTime; // Uppfærir x-hnit.
            animator.SetFloat("Move X", direction); // Stillir lárétta hreyfingu.
            animator.SetFloat("Move Y", 0); // Stillir lóðrétta hreyfingu á 0.
        }

        rigidbody2d.MovePosition(position); // Flytur óvininn í nýja stöðu.
    }

    // Þegar eitthvað rekst á óvininn.
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>(); // Athugar hvort leikmaður rekst á.

        if (player != null) // Ef það er leikmaður:
        {
            player.ChangeHealth(-1); // Minnkar heilsu leikmannsins um 1.
        }
    }

    // Lagar óvininn þegar kallað er á þessa aðgerð.
    public void Fix()
    {
        broken = false; // Setur óvin í bilað ástand.
        rigidbody2d.simulated = false; // Gerir eðlisfræði óvinar óvirka.
        animator.SetTrigger("Fixed"); // Spilar lagfæringarhreyfingu.
        smokeEffect.Stop(); // Stöðvar reyk.
    }
}