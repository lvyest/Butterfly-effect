using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject enemyPrefab; // �� �������� �����ϴ� ���� ����
    public float speed = 3f; // ���� �̵� �ӵ��� �����ϴ� ���� ���� �� �ʱ�ȭ

    void Start()
    {
        SpawnNewEnemy(); // ���� ���� �� ���ο� ���� �����ϴ� �Լ� ȣ��
    }

    void Update()
    {
        // ���� �������� �̵���Ű�� �ڵ�
        // Vector3.left�� (-1, 0, 0)�� ��Ÿ��
        // speed�� ���� �̵� �ӵ�
        // Time.deltaTime�� ���ؼ� ������ �������� �������� ���� (�ʴ� �̵� �Ÿ�)
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void SpawnNewEnemy()
    {
        // Main Camera�� ������ �ۿ��� ���� �����ϵ��� ����
        Vector3 cameraRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 10));
        GameObject newEnemy = Instantiate(
            enemyPrefab,
            new Vector3(cameraRightEdge.x + UnityEngine.Random.Range(1f, 5f), transform.position.y, transform.position.z),
            Quaternion.identity
        );
        Destroy(newEnemy, 10f); // ���� ������ ���� ���� �ð� �� �ı��ǵ��� ����
    }

    void OnDestroy()
    {
        // ���� �ı��� ������ ���ο� ���� �����ϴ� �Լ� ȣ��
        SpawnNewEnemy();
    }
}
