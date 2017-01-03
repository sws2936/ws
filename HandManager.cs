using UnityEngine;
using System.Collections;

public class HandManager : MonoBehaviour {
    public HandController m_HandController;//leapmotion controller
    public GameObject[] m_Shapes;//model control
    GameObject m_currentShapeObj;//current model
    int m_index;//model index
    int m_shapeCount;//number of model
    bool m_bSmallCreated;//create model in minimum distance

    //initializaiton
    void Awake()
    {
        m_index = -1;
        m_shapeCount = m_Shapes.GetLength(0);
        m_bSmallCreated = false;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Get handmodel
        HandModel[] handmodel = m_HandController.GetAllPhysicsHands();

        //Get number of hand
        Debug.Log("handmodel.GetLength = " + handmodel.GetLength(0));
        if (handmodel.GetLength(0) == 2)
        {
            Vector3 palmOnePos = handmodel[0].GetPalmPosition();
            Vector3 palmTwoPos = handmodel[1].GetPalmPosition();

            //Print distance of palms
            Debug.Log("Palm dist = " + (palmOnePos - palmTwoPos).magnitude);

            //小于某个阈值则判断是否需要变化图形
			//decide to change model less than some threshold value
            float dist = (palmOnePos - palmTwoPos).sqrMagnitude;
            Vector3 center = (palmOnePos + palmTwoPos) / 2;

            //打印两个手掌中心的位置
			//print distance of the center of hands
            Debug.Log("center dist = " + dist);
            if (dist < 0.01)//这个距离范围考虑删除模型//when <0.01 delete model
            {
                if(m_currentShapeObj != null)//先把之前的删除掉//delete the previous model
                {
                    m_bSmallCreated = false;
                    Destroy(m_currentShapeObj);
                }
            }
            else if(dist > 0.01 && dist < 0.03)//这个距离范围生成模型//create model in this distance
            {
                if (!m_bSmallCreated)
                {
                    m_index++;
                    if (m_index >= m_shapeCount)
                    {
                        m_index = 0;
                    }

                    if(!m_currentShapeObj)
                    {
                        //一定时间之内只生成一次//only run one time in moment
                        m_currentShapeObj = Instantiate(m_Shapes[m_index], center, Quaternion.identity) as GameObject;
                        m_currentShapeObj.transform.localScale = Vector3.one * dist * 1.3f;
                        m_bSmallCreated = true;
                    }
                }
            }
            else
            {
                if(m_currentShapeObj != null)//让模型跟着手掌变大变小//change the size with distance of hands
                {
                    if(m_bSmallCreated)
                    {
                        m_bSmallCreated = false;
                    }
                    m_currentShapeObj.transform.position = center;
                    m_currentShapeObj.transform.localScale = Vector3.one * dist * 1.3f;

                    //打印模型的缩放信息//print infomation about zoom
                    Debug.Log("scale = " + m_currentShapeObj.transform.localScale);
                }
            }
        }
        else//当收的数量不是2时，存在模型直接删除// when the number is not 2, delete model
        {
            if (m_currentShapeObj != null)//先把之前的删除掉//delete the previous model
            {
                m_bSmallCreated = false;
                Destroy(m_currentShapeObj);
            }
        }
    }
}
