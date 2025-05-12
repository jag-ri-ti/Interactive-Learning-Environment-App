using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class homeUI : MonoBehaviour
{
    public void scanner()
    {
        SceneManager.LoadScene("BodyScanner");
    }

    public void ARScene()
    {
        SceneManager.LoadScene("ARScene");
    }
}
