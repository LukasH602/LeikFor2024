using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Þetta er UIHandler klasi sem stjórnar notendaviðmóti í Unity.
public class UIHandler : MonoBehaviour
{
    // Einkabreyta fyrir heilsustikuna.
    private VisualElement m_Healthbar;

    // Singleton fyrir UIHandler svo hægt sé að nálgast það hvar sem er.
    public static UIHandler instance { get; private set; }

    // Tími sem samræðuskjárinn er sýnilegur.
    public float displayTime = 4.0f;

    // Einkabreyta fyrir samræðueininguna.
    private VisualElement m_NonPlayerDialogue;

    // Timer sem telur niður til að fela samræðuskjáinn.
    private float m_TimerDisplay;

    // Awake aðferðin keyrir fyrst og uppsetur singleton.
    private void Awake()
    {
        instance = this;
    }

    // Start keyrir þegar forritið byrjar og finnur UI element og setur upp byrjunarástand.
    private void Start()
    {
        // Nær í UIDocument og bindur heilsustikuna.
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f); // Stillir heilsustikuna í 100% til að byrja með.

        // Bindur samræðueininguna og gerir hana ósýnilega í byrjun.
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;

        // Stillir niðurteljara í neikvæðan gildis til að byrja með.
        m_TimerDisplay = -1.0f;
    }

    // Update keyrir í hverri ramma og athugar hvort eigi að fela samræðueininguna.
    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime; // Dregur frá tímanum sem eftir er.
            if (m_TimerDisplay < 0)
            {
                // Færir skjáinn aftur í ósýnilegt ástand þegar tíminn rennur út.
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    // Aðferð sem breytir stærð heilsustikunnar byggt á hlutfalli (percentage).
    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    // Aðferð til að sýna samræðueiningu og hefja niðurtalningu.
    public void DisplayDialogue()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex; // Sýnir eininguna.
        m_TimerDisplay = displayTime; // Endurstillir niðurtalningu.
    }
}