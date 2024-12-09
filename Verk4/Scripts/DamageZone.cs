using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Þetta er DamageZone klasi sem stjórnar svæði sem veldur skaða á leikmanni.
public class DamageZone : MonoBehaviour
{
    // Keyrist þegar einhver hlutur er stöðugt innan Trigger svæðis þessa hlutar.
    void OnTriggerStay2D(Collider2D other)
    {
        // Reynir að sækja PlayerController í hlutnum sem er í svæðinu.
        PlayerController controller = other.GetComponent<PlayerController>();

        // Ef það er leikmaður innan svæðisins (controller er ekki null):
        if (controller != null)
        {
            controller.ChangeHealth(-1); // Dregur 1 frá heilsu leikmannsins.
        }
    }
}