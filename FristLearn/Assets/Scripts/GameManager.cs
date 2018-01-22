using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  //单例类（伪）

    public GameObject uiCanvas;    //屏幕UI画布
    public Text showUIText;        //按钮文本
    public Text scoreTxt;          //显示分数的文本
    public GameObject cubePrefab;  //Cube预设体
    public GameObject cubePanel;   //用于存放Cube对象

    private int boomCount = 0;     //记录爆炸的次数
    private int score = 0;         //记录cube被消灭所得到的分数
    public bool showUI = true;     //标记屏幕UI是否处于显示状态


    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 生成新的Cube对象
    /// </summary>
    public void MakeCubes()
    {
        //RandomManager.GetInstance().GetRandomPosition();
        //GameObject cube = Instantiate(cubePrefab, RandomManager.GetInstance().GetRandomPosition(), Quaternion.identity);
        //cube.transform.parent = cubePanel.transform;

        boomCount++;

        MakeCube();
        MakeCube();

    }

    /// <summary>
    /// 生成一个新的Cube对象
    /// </summary>
    private void MakeCube()
    {
        GameObject cube = Instantiate(cubePrefab, RandomManager.GetInstance().GetRandomPosition(), Quaternion.identity);
        cube.transform.parent = cubePanel.transform;
        cube.transform.localScale = Vector3.one / boomCount;
    }

    /// <summary>
    /// 增加分数
    /// </summary>
    public void AddScore(int value)
    {
        score += value;
        scoreTxt.text = string.Format("Score:{0}", score.ToString());
    }
    
    /// <summary>
    /// 显示或隐藏屏幕UI
    /// </summary>
    public void ShowAndHideScreenUI()
    {
        showUI = !showUI;
        showUIText.text = showUI ? "关闭屏幕UI" : "显示屏幕UI";
        uiCanvas.SetActive(showUI);
    }
    
   
}
