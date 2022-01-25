using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class balon : MonoBehaviour
{
    float hiz = 2.0f;
    Color[] renkler;
    yonetici yonet;
    MeshRenderer gorunulurluk;
    public bool patlatildi = false;
    public GameObject patlamaEfecti;
    [SerializeField] List<string> harfler;
    [SerializeField] TMP_Text harfText;
    [SerializeField] List<AudioSource> sesler;


    public seslerharf BalonHarfi { get; private set; }

    public string harfAl()
    {
        if (harfler == null || harfler.Count <= 0)
        {
            return null;

        }
        int index = Random.Range(0, harfler.Count - 1);

        return harfler[index];



    }
    public enum seslerharf { e, b, t, se };

    Dictionary<seslerharf, AudioClip> seslerSozluk;
    [SerializeField] seslermodel[] sesmodelleri;

    private void SeslerSozlugunuOlustur()
    {
        if (sesmodelleri == null || sesmodelleri.Length <= 0)
            return;

        seslerSozluk = new Dictionary<seslerharf, AudioClip>();
        foreach (seslermodel model in sesmodelleri)
        {
            seslerSozluk.Add(model.harf, model.harfSes);
        }
    }
    private void Awake()
    {
        SeslerSozlugunuOlustur();

    }
    public AudioClip HarfSesiAl(seslerharf harf)
    {
        AudioClip ses = null;
        seslerSozluk.TryGetValue(harf, out ses);
        return ses;

    }










    private void OnEnable()
    {
        yonet = GameObject.Find("yonetici").GetComponent<yonetici>();
        gorunulurluk = gameObject.GetComponent<MeshRenderer>();

        renkDegisimi();
        CancelInvoke("sil");
        Invoke("sil", 3.0f);
    }


    private void OnDisable()
    {

    }

    void renkDegisimi()
    {

        string harf = harfAl();
        harfText.text = harf;

        renkler = new Color[4];

        renkler[0] = Color.red;

        renkler[1] = Color.blue;
        renkler[2] = Color.green;
        renkler[3] = Color.yellow;


        int rast = Random.Range(0, renkler.Length);
        gorunulurluk.material.color = renkler[rast];


    }
    void sil()
    {
        gameObject.SetActive(false);

        GameObject yEfect = Instantiate(patlamaEfecti, transform.position, Quaternion.identity);

        ParticleSystem.MainModule main = yEfect.GetComponent<ParticleSystem>().main;
        main.startColor = gorunulurluk.material.color;

        Destroy(yEfect, 0.5f);
        if (patlatildi == true)
        {
            if (gorunulurluk.material.color == renkler[0])
            {
                yonet.saniyeyiDegistir(-1);
                yonet.skoruDegistir(-10);
            }
            else
            {
                yonet.saniyeyiDegistir(1);
                yonet.skoruDegistir(10);
            }
        }
    }
    private void Update()
    {
        transform.Translate(0, -hiz * Time.deltaTime, 0);
    }
}
