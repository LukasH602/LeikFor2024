using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody leikmannsins.
    private Rigidbody rb; 

    // Breyta til að halda utan um safnað "PickUp" hluta.
    private int count;

    // Hreyfing meðfram X- og Y-ásum.
    private float movementX;
    private float movementY;

    // Hraði sem leikmaðurinn hreyfist.
    public float speed = 0;

    // UI textahlut fyrir að sýna fjölda safnaðra "PickUp" hluta.
    public TextMeshProUGUI countText;

    // UI hlut til að sýna sigurskýringuna.
    public GameObject winTextObject;

    // Start er kallað fyrir fyrstu ramma uppfærsluna.
    void Start()
    {
        // Fá og vista Rigidbody hlutann sem tengist leikmanninum.
        rb = GetComponent<Rigidbody>();

        // Initialize fjölda á núll.
        count = 0;

        // Uppfæra sýningu á fjölda.
        SetCountText();

        // Upphaflega stilla sigurtextann sem óvirkan.
        winTextObject.SetActive(false);
    }
 
    // Þessi aðgerð er kölluð þegar hreyfingarinput er greint.
    void OnMove(InputValue movementValue)
    {
        // Breyta input gildinu í Vector2 fyrir hreyfingu.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Vista X- og Y-samstæður hreyfingarinnar.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    // FixedUpdate er kallað einu sinni á hverju fastar ramma-uppfærslunni.
    private void FixedUpdate() 
    {
        // Búa til 3D hreyfingarsamsettningu með X- og Y-inputunum.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Beita krafti á Rigidbody til að hreyfa leikmanninn.
        rb.AddForce(movement * speed); 
    }

    void OnTriggerEnter(Collider other) 
    {
        // Athuga hvort hlutinn sem leikmaðurinn snerti hafi "PickUp" merkimiða.
        if (other.gameObject.CompareTag("PickUp")) 
        {
            // Óvirkja hlutinn sem snertist (láta hann hverfa).
            other.gameObject.SetActive(false);

            // Auka fjölda safnaðra "PickUp" hluta.
            count = count + 1;

            // Uppfæra sýningu á fjölda.
            SetCountText();
        }
    }

    // Aðgerð til að uppfæra sýndan fjölda safnaðra "PickUp" hluta.
    void SetCountText() 
    {
        // Uppfæra textann með núverandi fjölda.
        countText.text = "Fjöldi: " + count.ToString();

        // Athuga hvort fjöldinn hafi náð eða farið fram úr sigurskilyrðum.
        if (count >= 15)
        {
            // Sýna sigurtextann.
            winTextObject.SetActive(true);

            // Eyða óvinar GameObject.
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Eyða núverandi hlut
            Destroy(gameObject); 
     
            // Uppfæra sigurtextann til að sýna "Þú tapar!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "Þú tapar!";
        }
    }
}
