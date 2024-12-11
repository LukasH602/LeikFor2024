using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        //script fyrir takkan til að byrja leikin
        SceneManager.LoadScene("Verk5", LoadSceneMode.Single);
        data.score =0;

    }
}