using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 5; // �Ѿ��� �̵� �ӵ��� �����ϴ� ���� ���� �� �ʱ�ȭ

    void Update()
    {
        // �Ѿ��� ������ �������� �̵���Ű�� �ڵ�
        // transform.right�� ������Ʈ�� ������ ������ ��Ÿ��
        // speed�� �Ѿ��� �̵� �ӵ�
        // Time.deltaTime�� ���ؼ� ������ �������� �������� ���� (�ʴ� �̵� �Ÿ�)
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
