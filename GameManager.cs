using UnityEngine;
using System.Collections;
using Leap;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    Controller controller;

    Vector3 m_leftPalmPos;
    Vector3 m_rightPalmPos;
    float m_changeShapeTime;
    void Awake()
    {
        m_leftPalmPos = Vector3.zero;
        m_rightPalmPos = Vector3.zero;
        m_changeShapeTime = 0.0f;
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        controller = new Controller();
    }
	
	// Update is called once per frame
	void Update () {
//         Frame frame = controller.Frame();
//         // do something with the tracking data in the frame...
//         FingerList fingerList = frame.Fingers;
// 
//         Debug.Log("Finger pos, x = " + fingerList.Frontmost.TipPosition.x 
//             + ", y=" + fingerList.Frontmost.TipPosition.y + ", z=" + fingerList.Frontmost.TipPosition.z);
        
    }

    public void SetLeftPalmPos(Vector3 leftPos)
    {
        m_leftPalmPos = leftPos;
    }

    public void SetRightPalmPos(Vector3 rightPos)
    {
        m_rightPalmPos = rightPos;
    }
}
