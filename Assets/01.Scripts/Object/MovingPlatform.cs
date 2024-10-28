using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // �����̴� ����, ��, ���ǵ�
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;
    // ��ġ ����
    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        startPosition = pointA.position;
        endPosition = pointB.position;
    }

    private void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1); // PingPong: 0,1���̸� �ݺ��ϵ��� �Ѵ�. / Time*speed�� ������ �ӵ� ����
        transform.position = Vector3.Lerp(startPosition, endPosition, time); // Lerp ����. time������ ����, �� ������ �̵��ϰ� �պ��ϰ� ��
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))  // Player�±װ� �ִ� ������Ʈ�� �浹�ϸ�
        {
            other.transform.parent = this.transform;  // Player��  �÷����� �ڽ��� ��, ���� �����̰� ��
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;  // Player�� ����� �θ���� ����
        }
    }

}
