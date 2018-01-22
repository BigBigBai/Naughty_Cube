using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    //用于随机Cube在场景中的位置的变量值
    private float xzMinRandomVal = -9.5f;
    private float xzMaxRandomVal = 9.5f;
    private float yMinRandomVal = 0.5f;
    private float yMaxRandomVal = 4.5f;

    /// <summary>
    /// 单例类，返回唯一的实例(只有唯一的一个实例才叫单例类)
    /// </summary>
    private static RandomManager instance = null;
    public static RandomManager GetInstance()
    {
        if (instance == null)
            instance = new RandomManager();

        return instance;
    }

    /// <summary>
    /// 返回一个在指定范围内的随机位置（xz = []）
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(xzMinRandomVal, xzMaxRandomVal);
        float z = Random.Range(xzMinRandomVal, xzMaxRandomVal);
        float y = Random.Range(yMinRandomVal, yMaxRandomVal);

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// 私有构造函数
    /// </summary>
    private RandomManager()
    {

    }
}
