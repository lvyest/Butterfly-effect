using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 4; // ���� �̵� �ӵ��� �����ϴ� ���� ���� �� �ʱ�ȭ

    void Update()
    {
        Vector3 dir = Vector3.left; // ���� �̵� ������ �������� ����

        // ���� ���� �������� �̵���Ű�� �ڵ�
        // dir�� Vector3.left�̹Ƿ� (-1, 0, 0)�� ���͸� �ǹ�
        // speed�� ���� �̵� �ӵ�
        // Time.deltaTime�� ���ؼ� ������ �������� �������� ���� (�ʴ� �̵� �Ÿ�)
        transform.position += dir * speed * Time.deltaTime;
    }
}
