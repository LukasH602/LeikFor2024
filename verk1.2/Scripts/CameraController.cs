using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Vísun í leikmanns GameObject.
    public GameObject player;

    // Fjarlægð milli myndavélarinnar og leikmannsins.
    private Vector3 offset;

    // Start er kallað áður en fyrsta ramma uppfærslan fer fram.
    void Start()
    {
        // Reikna upphaflega fjarlægðina milli stöðu myndavélarinnar og stöðu leikmannsins.
        offset = transform.position - player.transform.position; 
    }

    // LateUpdate er kallað einu sinni á hverju ramma eftir að allar Update aðgerðir hafa verið framkvæmdar.
    void LateUpdate()
    {
        // Halda sömu fjarlægð milli myndavélarinnar og leikmannsins í gegnum leikinn.
        transform.position = player.transform.position + offset;  
    }
}
