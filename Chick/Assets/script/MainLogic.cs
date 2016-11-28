using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GAF.Core;
using GAFInternal.Core;
using UnityEngine.UI;
public class MainLogic : MonoBehaviour 
{
    public Text counter;
    public GameObject egg = null;
    public GameObject Chick = null;
    public int maxEgg = 100;
	// Use this for initialization

    List<GameObject> eggList;

    GAFMovieClip mc;
    GameObject preEgg;

    int Counter = 0;
	void Start () 
    {
        mc = Chick.GetComponent<GAFMovieClip>();
        InitEggs();
        InitChick();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mc.currentSequenceIndex == 1)
        {
            if (!mc.isPlaying())
            {
                mc.setSequence("idel", true);
                mc.setAnimationWrapMode(GAFWrapMode.Loop);
                Vector3 ChickPos = new Vector3(preEgg.transform.position.x-60, preEgg.transform.position.y + 60, 0);
                Chick.transform.position = ChickPos;
            }
               
        }
        if (Input.anyKeyDown && Counter <= maxEgg)
        {
            if (mc.currentSequenceIndex == 0)
                CreateEgg();
        }
	}

    void InitEggs()
    {
        eggList = new List<GameObject>();
        int posX = 0;
        int posY = 0;
        for (int i = 0; i < maxEgg; i++)
        {
            int valueX = Random.Range(-3, 3);
            int valueY = Random.Range(-10, 10);
            posX = i % 10 * 80 + valueX;
            posY = i / 10 * 100 * -1 + valueY;
            Vector3 pos = new Vector3(posX, posY, 100);
            GameObject instance = (GameObject)Instantiate(egg, pos, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            instance.SetActive(false);
            eggList.Add(instance);
        }
    }

    void InitChick()
    {
        Vector3 pos = new Vector3(Screen.width / 2 - 250, -1*Screen.height / 2 + 200, 0);
        Chick.transform.position = pos;       
    }

    void CreateEgg()
    {
        int eggIndex =  Random.Range(0, eggList.Count-1);
        GameObject egg = eggList[eggIndex];
        egg.SetActive(true);
        preEgg = egg;
        eggList.Remove(egg);
        Vector3 ChickPos = new Vector3(egg.transform.position.x - 130, egg.transform.position.y+60,0);
        Chick.transform.position = ChickPos;

        mc.setSequence("layegg", true);
        mc.setAnimationWrapMode(GAFWrapMode.Once);
        Counter++;
        counter.text = Counter.ToString();
    }
}
