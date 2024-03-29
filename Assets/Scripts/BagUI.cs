using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUI : MonoBehaviour
{
    private bool isBagOpen=false;
    private void Start()
    {
        Player.Instance.OnBagToggle += Instance_OnBagToggle;
        Hide();
    }

    private void Instance_OnBagToggle(object sender, System.EventArgs e)
    {
        isBagOpen = !isBagOpen;
        if(isBagOpen)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
