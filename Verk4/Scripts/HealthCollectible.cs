using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Þetta er HealthCollectible klasi sem stjórnar hegðun hlutar sem gefur leikmanni heilsu.
public class HealthCollectible : MonoBehaviour
{
    // Hljóðskrá sem spilast þegar hluturinn er tekinn upp.
    public AudioClip collectedClip; 

    // Keyrir þegar eitthvað fer inn í Collider svæði hlutarins.
    void OnTriggerEnter2D(Collider2D other)
    {
        // Reynir að sækja PlayerController á hlutnum sem rekst á.
        PlayerController controller = other.GetComponent<PlayerController>();

        // Athugar hvort:
        // 1. Controller sé ekki null (þ.e. þetta er leikmaður).
        // 2. Leikmaður sé ekki með fulla heilsu.
        if (controller != null && controller.health < controller.maxHealth)
        {
            controller.PlaySound(collectedClip); // Spilar hljóð þegar hluturinn er tekinn upp.
            controller.ChangeHealth(1); // Eykur heilsu leikmanns um 1.
            Destroy(gameObject); // Eyðir heilsu hlutnum úr leiknum.
        }
    }
}