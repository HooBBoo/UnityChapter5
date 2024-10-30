using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // 움직이는 시작, 끝, 스피드
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;
    // 위치 저장
    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        startPosition = pointA.position;
        endPosition = pointB.position;
    }

    private void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1); // PingPong: 0,1사이를 반복하도록 한다. / Time*speed로 일정한 속도 조절
        transform.position = Vector3.Lerp(startPosition, endPosition, time); // Lerp 보간. time값으로 시작, 끝 지점을 이동하고 왕복하게 함
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))  // Player태그가 있는 오브젝트와 충돌하면
        {
            other.transform.parent = this.transform;  // Player가  플랫폼의 자식이 됨, 같이 움직이게 됨
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;  // Player가 벗어나면 부모관계 해제
        }
    }

}
