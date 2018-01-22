using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    //Cube对象的状态
    //枚举里的类型默认是int,
    enum CubeStatus
    {
        //1和2按位或运算的结果是：0000 0000 0000 0011
        Normal = 1 << 0,  //0000 0000 0000 0001
        Select = 1 << 1,  //0000 0000 0000 0010
        Bother = 1 << 2,  //0000 0000 0000 0100
        Angry  = 1 << 3,   //0000 0000 0000 1000
    }

    public Material normalMaterial;//正常状态下的材质（蓝）
    public Material selectMaterial;//选中状态下的材质（绿）
    public Material botherMaterial;//心烦状态下的材质（黄）
    public Material angryMaterial; //愤怒状态下的材质（红）
    public MeshRenderer meshRender;//用来更换Cube的材质球
    public GameObject boomEffect;  //爆炸粒子特效预设体

    private int clickCount = 0;    //Cube被点击的次数
    private float rotateSpeed = 20;//Cube旋转速度
    private bool isGazeEnter = false;//记录十字光标是否在Cube对象身上

    private CubeStatus cubeStatus = CubeStatus.Normal;//Cube对象的当前状态

    private void Update()
    {
        //if(cubeStatus == CubeStatus.Bother || cubeStatus == CubeStatus.Angry)
        //{
        //   if(isGazeEnter)
        //        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        //}

        //按位与判断cube的state是否在组合串中
        //4和8按位或运算结果：0000 0000 0000 1100
        //按位与运算得出的cubeStatus当前这个状态是否在组合串（CubeStatus.Bother和CubeStatus.Angry）中
        //4和8位运算的结果在和cubeStatus进行位于运算的结果是：
        //如果cubeStatus == CubeStatus.Bother: 0000 0000 0000 1100  
        //                                   & 0000 0000 0000 0100 = 0000 0000 0000 0100
        //如果cubeStatus == CubeStatus.Angry:  0000 0000 0000 1100  
        //                                   & 0000 0000 0000 1000 = 0000 0000 0000 1000
        //如果cubeStatus == CubeStatus.Normal: 0000 0000 0000 1100  
        //                                   & 0000 0000 0000 0001 = 0000 0000 0000 0000
        if (((CubeStatus.Bother | CubeStatus.Angry) & cubeStatus) > 0)
        {
            if (isGazeEnter)
                transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        }
    }

    /// <summary>
    /// 视线的焦点进入Cube
    /// </summary>
    public void OnGazeEnter()
    {
        //meshRender.material = selectMaterial;

        isGazeEnter = true;

        //Cube状态切换成被选中状态
        if(clickCount == 0)
            cubeStatus = CubeStatus.Select;

        SetCubeMaterial();
    }

    /// <summary>
    /// 视线的焦点离开Cube
    /// </summary>
    public void OnGazeExit()
    {
        //meshRender.material = normalMaterial;

        isGazeEnter = false;

        //Cube状态切换回正常状态
        if(clickCount == 0)
            cubeStatus = CubeStatus.Normal;

        SetCubeMaterial();
    }

    /// <summary>
    /// 视线的焦点进入并且点击
    /// </summary>
    public void OnGazeAndClick()
    {
        clickCount++;

        if(clickCount == 1)
        {
            cubeStatus = CubeStatus.Bother;
        }
        else if(clickCount == 2)
        {
            cubeStatus = CubeStatus.Angry;
        }
        else if(clickCount == 3)//爆炸
        {
            clickCount = 0;//重置
            BoomParticleEffect();

            //生成两个新的Cube对象
            GameManager.Instance.MakeCubes();

            //增加分数，炸毁一个cubed对象得2分
            GameManager.Instance.AddScore(2);
        }

        SetCubeMaterial();
        //if(clickCount == 1)
        //{
        //    Debug.Log("被点击了一次");
        //    meshRender.material = botherMaterial;
        //}
        //else if(clickCount == 2)
        //{
        //    Debug.Log("被点击了两次");
        //    meshRender.material = angryMaterial;
        //}

        transform.position = RandomManager.GetInstance().GetRandomPosition();
    }

    /// <summary>
    /// 设置Cube对象的材质球
    /// </summary>
    private void SetCubeMaterial()
    {
       switch(cubeStatus)
       {
            case CubeStatus.Normal:
                meshRender.material = normalMaterial;
                break;
            case CubeStatus.Select:
                meshRender.material = selectMaterial;
                break;
            case CubeStatus.Bother:
                meshRender.material = botherMaterial;
                break;
            case CubeStatus.Angry:
                meshRender.material = angryMaterial;
                break;
            
        }
        
    }

    /// <summary>
    /// 粒子爆炸效果
    /// </summary>
    private void BoomParticleEffect()
    {
        Destroy(Instantiate(boomEffect, transform.position, Quaternion.identity), 4.0f);
        Destroy(gameObject);//删除当前立方体
    }

}
