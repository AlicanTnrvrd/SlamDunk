using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    [Header("---LEVEL TEMEL OBJELERÝ")]
    [SerializeField] private GameObject Pota;
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject PotaBuyume;
    [SerializeField] private GameObject[] OzellikOlusmaNoktalari;
    [SerializeField] private AudioSource[] Sesler;
    [SerializeField] private ParticleSystem[] Efektler;
    SceneManager scene;


    [Header("---UI OBJELERÝ")]
    [SerializeField] private Image[] GorevGorselleri;
    [SerializeField] private Sprite GorevTamamSprite;
    [SerializeField] private int AtilmasiGerekenTop;
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI LevelAd; 
    int BasketSayisi;
    float ParmakPoxX;


    void Start()
    {
        LevelAd.text = "LEVEL : " + SceneManager.GetActiveScene().name;
        for (int i = 0; i < AtilmasiGerekenTop; i++)
        {
            GorevGorselleri[i].gameObject.SetActive(true);
        }

        //Invoke("OzellikOlussun", 3f);

    }
    void OzellikOlussun()
    {
        int RandomSayi = Random.Range(0, OzellikOlusmaNoktalari.Length - 1);
        PotaBuyume.transform.position = OzellikOlusmaNoktalari[RandomSayi].transform.position;
        PotaBuyume.SetActive(true);

    }


    void Update()
    {
        
        
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

                switch (touch.phase)
                {

                    case TouchPhase.Began:
                        ParmakPoxX = TouchPosition.x - Platform.transform.position.x;
                        break;
                    
                    case TouchPhase.Moved:
                        if ( TouchPosition.x - ParmakPoxX > -1.25 && TouchPosition.x - ParmakPoxX < 1.25)
                        {
                          Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(TouchPosition.x - ParmakPoxX 
                        , Platform.transform.position.y, Platform.transform.position.z), 5f);

                        }
                        break;

                }

            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Platform.transform.position.x > -1.5)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x - .3f
                        , Platform.transform.position.y, Platform.transform.position.z), .50f);

            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Platform.transform.position.x < 1.5)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x + .3f
                              , Platform.transform.position.y, Platform.transform.position.z), .50f);
            }

        

        
    }

    public void Basket(Vector3 Poz)
    {

        BasketSayisi++;
        GorevGorselleri[BasketSayisi - 1].sprite = GorevTamamSprite;
        Efektler[0].transform.position = Poz;
        Efektler[0].gameObject.SetActive(true);
        Efektler[0].Play();
        Sesler[1].Play();
        if (BasketSayisi == AtilmasiGerekenTop)
        {

            Debug.Log("Kazandýn");

        }
        if (BasketSayisi == 1)
        {
            OzellikOlussun();
        }
    }
    public void Kaybettin()
    {
        Sesler[2].Play();
        Paneller[2].SetActive(true);
        Debug.Log("Kaybettin");
        Time.timeScale = 0;
    }

    void Kazandin()
    {
        Debug.Log("KAZANDIN");
        Paneller[1].SetActive(true);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level" + 1));
        Sesler[3].Play();
        Time.timeScale = 0;
    }

    public void PotaBuyut(Vector3 Poz)
    {
        Efektler[1].transform.position = Poz;
        Efektler[1].gameObject.SetActive(true);
        Pota.transform.localScale = new Vector3(55, 55, 55);
        Sesler[0].Play();
    }

    public void Butonlarinislemleri(string Deger)
    {
        switch (Deger)
        {

            case "Durdur":
                Time.timeScale = 0;
                Paneller[0].SetActive(true);
                break;


            case "DevamEt":
                Time.timeScale = 1;
                Paneller[0].SetActive(false);
                break;

            case "Tekrar":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                //Paneller[0].SetActive(false);
                break;
            
            case "SonrakiLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 );
                Time.timeScale = 1;
                //Paneller[0].SetActive(false);
                break;

            case "Ayarlar":

                break;

            case "Cikis":
                Application.Quit();
                break;


        }

    }
}


