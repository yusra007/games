using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class yonetici : MonoBehaviour
{
    public GameObject balon;
    public Text skor_txt;
    public Text saniye_txt;

    int skor = 0;
    int saniye = 50;
    List<GameObject> balonlar;
    public AudioSource ses;
    public GameObject yenidenoyna_pnl;



    void Start()
    {
        skor_txt.text = skor.ToString();
        saniye_txt.text = saniye.ToString();
        balonlar = new List<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            float rest = Random.Range(-3.5f, 3.5f);
            GameObject yBlon = Instantiate(balon, new Vector3(rest, 0, 0.1f), Quaternion.Euler(0, 0, 180.0f));
            balonlar.Add(yBlon);
            yBlon.SetActive(false);

        }
        InvokeRepeating("balonGoster", 0.0f, 1.0f);
        InvokeRepeating("saniyeAzalt", 0.0f, 1.0f);
    }
    void saniyeAzalt()
    {
        saniye--;
        saniye_txt.text = saniye.ToString();
        if (saniye <= 0)
        {
            yenidenoyna_pnl.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    public void skoruDegistir(int deger)
    {
        skor += deger;
        skor_txt.text = skor.ToString();
    }
    public void saniyeyiDegistir(int deger)
    {
        saniye += deger;
        saniye_txt.text = saniye.ToString();
    }
    private void balonGoster()
    {
        foreach (GameObject bl in balonlar)
        {
            if (bl.activeSelf == false)
            {
                bl.SetActive(true);
                float rest = Random.Range(-3.5f, 3.5f);
                bl.transform.position = new Vector3(rest, -3.0f, 1.0f);
                break;
            }
        }
    }
    public AudioSource balonPatlamSesi;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch parmak = Input.GetTouch(0);
            RaycastHit nesne;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(parmak.position), out nesne))
            {
                if (nesne.collider.tag == "balon")
                {
                    //ses.Play();
                    nesne.collider.GetComponent<balon>().patlatildi = true;


                    balon balon = nesne.collider.GetComponent<balon>();
                    AudioClip harfsesi = balon.HarfSesiAl(balon.seslerharf.e);
                    balonPatlamSesi.clip = harfsesi;
                    balonPatlamSesi.Play();
                    nesne.collider.gameObject.SetActive(false);

                }
            }

        }
    }
    public void yenidenoyna_btn()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

}
