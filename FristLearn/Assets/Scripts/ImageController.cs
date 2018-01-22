using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 实现功能：当用户视线焦点移动到Image，并注视超过5秒，这个时候Image会自动切换下一张图片
///           如果用户视线离开了Image对象，并再次注视Image的时候需要从新计时
/// </summary>
public class ImageController : MonoBehaviour
{
    public GameObject reticle;                     //用户视线十字光标
    public Image image;                            //用来显示图片纹理的组件对象
    public Sprite[] sprites;                       //用来保存图片的数组
    private bool isGazeImage = false;              //标记用户视线焦点是否在Image对象上
    private const float COUNTDOWN_TIME = 5.0f;     //计时器的时长
    private float countdown = COUNTDOWN_TIME;      //计时器（计时用户注视时间）
    private int imgIndex = 0;                      //图片数组的索引号
    public float rotateSpeed = 0.05f;             //旋转速度
    private float val;

	void Start ()
    {
		
	}
	
	/// <summary>
    /// 程序运行过程中每一帧都会调用的方法
    /// </summary>
	void Update ()
    {
        //当用户注视Image对象的时候我们才开始计时
		if(isGazeImage)
        {
            ReticleAnimation();

            countdown -= Time.deltaTime;
            if(countdown <= 0)
            {
                //计时结束切换Image图片

                if (++imgIndex > sprites.Length - 1)
                    imgIndex = 0;
                //imgIndex++;
                //if (imgIndex > 3)
                //    imgIndex = 0;

                image.sprite = sprites[imgIndex];   //imgIndex[0,3]

                countdown = COUNTDOWN_TIME;
            }
        }
        else
        {
            //用户的视线焦点离开了Image对象
            ResetReticleScale();
        }
	}
    
    /// <summary>
    /// 当用户视线焦点在Image对象上的时候触发（调用）
    /// </summary>
    public void onGazeEnter()
    {
        //Debug.Log("on gaze enter");
        isGazeImage = true;
    }

    /// <summary>
    /// 当用户视线焦点离开Image对象的时候触发（调用）
    /// </summary>
    public void onGazeExit()
    {
        //Debug.Log("on gaze exit");
        isGazeImage = false;
    }

    /// <summary>
    /// 当用户注视Image对象的时候，播放视线十字光标动画
    /// </summary>
    private void ReticleAnimation()
    {
        Vector3 scale = reticle.transform.localScale;

        if (scale.x == 1)
            val = -rotateSpeed;
        else if (scale.x == -1)
            val = rotateSpeed;

        scale.x += val;

        if (scale.x < -1)
            scale.x = -1;
        else if (scale.x > 1)
            scale.x = 1;

        reticle.transform.localScale = scale;
    }

    /// <summary>
    /// 十字光标缩放值复位
    /// </summary>
    private void ResetReticleScale()
    {
        reticle.transform.localScale = Vector3.one;
    }
}
