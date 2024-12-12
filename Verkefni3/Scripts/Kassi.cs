using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Kassi : MonoBehaviour
{
 
    public static int count=0;
    public GameObject sprenging;
    private TextMeshProUGUI countText;
    void Start()
    {
        countText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        //sprenging= GetComponent<Explosion>
        count = 0;
        SetCountText();
    }
    private void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        if (count == 5)
        {
            SceneManager.LoadScene("endir", LoadSceneMode.Single);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "kula")
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
            Debug.Log("varð fyrir kúlu");
            count = count + 1;//færð stig
            SetCountText();//kallar í aðferðina
            Sprengin();
        }
    }
    void SetCountText()//hér er aðferðin
    {
        countText.text = "Stig: " + count.ToString();
    }
    void Sprengin()
    {
        Instantiate(sprenging, transform.position, transform.rotation);
    }
}
