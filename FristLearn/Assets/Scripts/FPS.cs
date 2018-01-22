using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用来计算FPS（每秒执行多少帧）的值。FPS等于1秒钟运行的帧数
/// </summary>
public class FPS : MonoBehaviour
{
    public Text fpsTxt;                 //显示FPS的文本
    private int frames = 0;             //记录运行的帧数
    private float nowTime = 0;          //记录当前程序运行了多长时间
    private float lastTime = 0;         //程序上次开始的时间
    private float intervalTime = 0.5f;  //间隔时间
    private float fps = 0;              //存储计算出来的fps的值
    

	// Use this for initialization
	void Start ()
    {
        //记录程序一开始执行时的时间
        lastTime = Time.realtimeSinceStartup;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        frames++;
        nowTime = Time.realtimeSinceStartup;
        if(nowTime > lastTime + intervalTime)
        {
            fps = (float)(frames / (nowTime - lastTime));
            frames = 0;
            lastTime = nowTime;
        }

        fpsTxt.text = string.Format("FPS:{0}", Mathf.RoundToInt(fps));
	}
}
