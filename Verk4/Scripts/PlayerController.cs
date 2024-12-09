using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Þetta er PlayerController klasi sem stjórnar leikmanni í 2D umhverfi.
public class PlayerController : MonoBehaviour
{
    // Hreyfiaðgerð úr Input System.
    public InputAction MoveAction;

    // Rigidbody2D fyrir hreyfingu leikmannsins.
    Rigidbody2D rigidbody2d;

    // Hreyfivektor sem stjórnar stefnu leikmannsins.
    Vector2 move;

    // Hraði leikmannsins.
    public float speed = 3.0f;

    // Hámarksheilsu leikmannsins.
    public int maxHealth = 5;

    // Núverandi heilsu leikmannsins.
    public int health { get { return currentHealth; } }
    int currentHealth;

    // Breytur fyrir ósýnileikann þegar leikmaður tekur skaða.
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    // Animator fyrir hreyfivísun og árásir.
    Animator animator;

    // Vektor sem geymir síðustu stefnu leikmannsins.
    Vector2 moveDirection = new Vector2(1, 0);

    // Prefab fyrir skot leikmannsins.
    public GameObject projectilePrefab;

    // Hljóðspilari.
    AudioSource audioSource;

    // Upphafsstillingar.
    void Start()
    {
        MoveAction.Enable(); // Virkja Input Action fyrir hreyfingu.
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Setja heilsu í hámark.
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Uppfærir leikmannsstjórnun í hverjum ramma.
    void Update()
    {
        // Les hreyfiviðskipan frá notanda.
        move = MoveAction.ReadValue<Vector2>();

        // Uppfærir stefnu ef leikmaður er að hreyfa sig.
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize(); // Gæta að einingavektor.
        }

        // Uppfærir breytur fyrir Animator.
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // Stjórnar ósýnileika leikmannsins.
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false; // Leikmaður verður sýnilegur aftur.
            }
        }

        // Athugar hvort leikmaður vill skjóta.
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        // Athugar hvort leikmaður vill leita að NPC.
        if (Input.GetKeyDown(KeyCode.X))
        {
            FindFriend();
        }
    }

    // Uppfærir staðsetningu leikmannsins í FixedUpdate fyrir stöðuga hreyfingu.
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    // Breytir heilsu leikmannsins.
    public void ChangeHealth(int amount)
    {
        if (amount < 0) // Ef leikmaður tekur skaða.
        {
            if (isInvincible)
            {
                return; // Gildir ekki ef leikmaður er ósýnilegur.
            }
            isInvincible = true; // Gerir leikmann ósýnilegan í ákveðinn tíma.
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit"); // Spilar högg-hreyfingu.
        }

        // Uppfærir heilsu og tryggir að hún sé innan marka.
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        // Uppfærir heilsustiku í notendaviðmóti.
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    // Skýtur skoti í átt leikmanns.
    void Launch()
    {
        // Býr til skot á réttum stað með Prefab.
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        // Gefur skotinu stefnu og kraft.
        projectile.Launch(moveDirection, 300);

        // Spilar skothreyfingu.
        animator.SetTrigger("Launch");
    }

    // Finnur NPC (Non-Player Character) í stefnu leikmannsins.
    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                UIHandler.instance.DisplayDialogue(); // Kveikir á samræðu.
            }
        }
    }

    // Spilar hljóð.
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}