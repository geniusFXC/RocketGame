using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RocketControl : MonoBehaviour
{
    //外部引用
    public GameObject leftEngine;
    public GameObject rightEngine;
    public Slider enerySlider;
    public Button reStartBtn;
    //可调节参数
    public float EngineSpeedX;
    public float acceleratedTime;//加速度时间
    public float forceMul;//力度系数
    public float gravity;

    
    private Rigidbody2D rig;
    private float timer = 0;
	// Use this for initialization
	void Start () {
        
        rig = GetComponent<Rigidbody2D>();
        reStartBtn.onClick.AddListener(ReStart);
        
	}
	
	// Update is called once per frame
	void Update () {
   
        
	}
    void FixedUpdate()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        //有能量的时候才能飞
        if (enerySlider.value >= 0)
        {
            //着陆的时候才能弹射起步 着陆的时候速度是0
            if (rig.velocity == Vector2.zero)
            {
                RaiseControl();
            }
            else
            {
                LandControl();
            }
        }    
    }
    void RaiseControl()
    {
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            EneryDecrease();
        }
        if (Input.GetMouseButtonUp(0))
        {
            rig.AddForce(Vector2.Lerp(Vector2.zero, new Vector2(0, timer * forceMul), acceleratedTime), ForceMode2D.Impulse);
            timer = 0;
        }
    }
    void LandControl()
    {
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            EneryDecrease();
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            {
                //rig.AddForce(new Vector2(EngineSpeedX,(float)(gravity * Math.Pow(timer,2))));
                rig.AddForce(new Vector2(EngineSpeedX, gravity * rig.mass));
            }
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            {
                //rig.AddForce(new Vector2(-EngineSpeedX, (float)(gravity * Math.Pow(timer, 2))));
                rig.AddForce(new Vector2(-EngineSpeedX, gravity * rig.mass));
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            timer = 0;
        }
    }
   //能量条减少
    void EneryDecrease()
    {
        enerySlider.value -= Time.fixedDeltaTime * 20;
    }
    void ReStart()
    {
        SceneManager.LoadScene("Main");
    }

    
}
