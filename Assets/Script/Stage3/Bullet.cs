using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject prefab; // �Ѿ��� �������� ���� ������ ����

    public static int maxBulletCount = 10; // �ִ� �Ѿ� �߻� Ƚ���� 10���� ����
    public int bulletCount = 0; // ���� �߻��� �Ѿ� Ƚ���� 0���� �ʱ�ȭ
    public int bulletuseable; // ��� ������ �Ѿ� ������ ��� ������ ���� (UI�� ǥ�ÿ�)

    private void OnTriggerEnter2D(Collider2D other) // 2D �浹�� �߻����� �� ȣ��Ǵ� �޼��带 ����
    {
        if (other.CompareTag("Enemy")) // �浹�� ��ü�� �±װ� "Enemy"�� ��츦 Ȯ��
        {
            Destroy(other.gameObject); // �� ��ü�� �ı�
        }
    }

    private void Start() // ���� ���� �� ȣ��Ǵ� �޼��带 ����
    {
        bulletuseable = maxBulletCount; // ��� ������ �Ѿ� ������ �ִ� �Ѿ� �߻� Ƚ���� �ʱ�ȭ
    }

    void Update() // �� �����Ӹ��� ȣ��Ǵ� �޼��带 ����
    {
        if (Input.GetKeyDown(KeyCode.Space) && bulletCount < maxBulletCount) // �����̽��ٰ� ������ �߻��� �� �ִ� �Ѿ��� ���� �ִ��� Ȯ��
        {
            GameObject obj = Instantiate(prefab, transform.position, transform.rotation); // �Ѿ� �������� ���� ��ġ�� ȸ������ ����
            Destroy(obj, 5); // ������ �Ѿ��� 5�� �Ŀ� �ı�

            bulletCount++; // �߻��� �Ѿ� Ƚ���� ����
            bulletuseable = maxBulletCount - bulletCount; // ���� ��� ������ �Ѿ� ������ ���
        }
    }
}
