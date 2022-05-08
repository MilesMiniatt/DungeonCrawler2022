using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Transform p_bar;
    [SerializeField]
    GameObject[] p_hp;
    [SerializeField]
    Transform b_bar;
    [SerializeField]
    GameObject[] b_hp;
    [SerializeField]
    Image o2Bar;

    // Start is called before the first frame update
    void Start()
    {

    }

    void UpdateHealth(int hp, Transform hp_bar)
    {
        for (int i = 0; i < hp_bar.childCount; i++) {
            if (i < hp)
            {
                hp_bar.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                hp_bar.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ChangePHealth(int health)
    {
        UpdateHealth(health, p_bar);
    }

    public void ChangeBHealth(int health)
    {
        UpdateHealth(health, b_bar);
    }

    public void O2(float o2)
    {
        o2Bar.fillAmount = o2;
    }

    public void O2Toggle(GameObject o2border)
    {
        if (o2border.activeInHierarchy)
        {
            o2border.SetActive(false);
        } else
        {
            o2border.SetActive(true);
        }
    }

}
