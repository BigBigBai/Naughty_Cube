using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text text;   //电子屏显示文本
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        Vector3 pos = text.rectTransform.localPosition;
        pos.x -= 2;

        //说明文本已经移出了电子屏幕的显示范围，需要把文本位置拉回去
        if (pos.x < -590)
            pos.x = 590;

        text.rectTransform.localPosition = pos;

	}
}
