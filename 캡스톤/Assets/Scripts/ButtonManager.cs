using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public GameObject Main;
    public GameObject Sel_mode;
    public GameObject Sin_Map;
    public GameObject Mul_Map;
    
    public void Game_start()
    {
        Main.SetActive(false);
        Sel_mode.SetActive(true);
    }
    public void Sel_sin()
    {
        Sel_mode.SetActive(false);
        Sin_Map.SetActive(true);

    }
    public void Sel_mul()
    {
        Sel_mode.SetActive(false);
        Mul_Map.SetActive(true);
    }
    public void Single_Map1()
    {
        SceneManager.LoadScene("Single Map1");
    }
    public void Multi_Map1()
    {
        SceneManager.LoadScene("Multi Map1");
    }
    public void Single_Map2()
    {
        SceneManager.LoadScene("Single Map2");
    }
    public void Multi_Map2()
    {
        SceneManager.LoadScene("Multi Map2");
    }
}
