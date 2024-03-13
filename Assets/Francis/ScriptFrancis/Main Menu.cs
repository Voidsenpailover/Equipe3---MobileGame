using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject creditScreen;
    public GameObject firstchooseScreen;
    //public LevelManager levelManager;
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);/*En cas de Scene Manager*/
        SceneManager.LoadSceneAsync(1);/*Copier coller d'un morceau de code dans le script Level Manager*/

    }
    public void ShowCredit()
    {
        creditScreen.SetActive(true);
        firstchooseScreen.SetActive(false);
    }
    public void HideCredit()
    {
        creditScreen.SetActive(false);
        firstchooseScreen.SetActive(true);
    }
        

    public void Quitter()
    {
        Application.Quit();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
