using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyGet : MonoBehaviour
{

    public GameObject KeyCop;
    public OnSearchView onSearch;

    //private bool KeyGetFlg = false;



    public GameObject TutorialTar;
    public GameObject NextTutorialTar;

    public Image Bou;

    private bool OneEnter = true;
    private bool TarEnter = true;

    private float SaveTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        onSearch = KeyCop.GetComponent<OnSearchView>();
        Bou = Bou.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OneEnter && onSearch.keyflg)
        {
            OneEnter = false;
        }

        if (!OneEnter && Bou.fillAmount < 1.0f)
        {
            Bou.fillAmount += 0.1f;

            if (Bou.fillAmount >= 1.0f)
            {
                //完全に表示が終わった時間を取得
                SaveTime = Time.time;
                TarEnter = false;
            }
        }

        //表示終わって2秒後に次の目標表示
        if (!TarEnter && SaveTime + 2.0f < Time.time)
        {
            Bou.fillAmount = 0f;
            TutorialTar.SetActive(false);
            NextTutorialTar.SetActive(true);
            GetComponent<KeyGet>().enabled = false;
        }
    }
}
