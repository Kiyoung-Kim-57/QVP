using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public RectTransform uiGroup;

    public void BoxUp()
    {

        uiGroup.anchoredPosition = Vector3.zero;





    }

    public void BoxDown()
    {

        uiGroup.anchoredPosition = Vector3.down * 1000;
    }


}
