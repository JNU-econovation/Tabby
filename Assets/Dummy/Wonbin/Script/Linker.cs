using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Linker : MonoBehaviour
{
    public static Linker _instance;
    public List<Animal> animals;
    public int[] gogoAnimalIndexes;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void GOGObattle()
    {
        //MapbuttonManager에서 gogoAnimalIndexes가져오기
        foreach (int i in gogoAnimalIndexes)
        {
            if(i!=-1)
                animals.Add(Spawner.animals[i]);
        }
        SceneManager.LoadScene("HS_Mission");
    }
}
