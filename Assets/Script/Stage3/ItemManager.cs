using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject shrinkItemPrefab; // �÷��̾� ũ�� ��� ������ �������� �����ϴ� ���� ����
    public GameObject ignoreCollisionItemPrefab; // ������ �浹 ���� ������ �������� �����ϴ� ���� ����

    public float spawnInterval = 15f; // ������ ���� ������ �����ϴ� ���� ���� �� �ʱ�ȭ
    public float displayTime = 5f; // �������� ǥ�õǴ� �ð��� �����ϴ� ���� ���� �� �ʱ�ȭ

    private float nextSpawnTime = 0f; // ���� ������ ���� �ð��� �����ϴ� ���� ���� �� �ʱ�ȭ

    void Update()
    {
        // ���� �ð��� ���� ������ ���� �ð����� ũ�ų� ������ �������� ����
        if (Time.time >= nextSpawnTime)
        {
            SpawnItem(); // �������� �����ϴ� �Լ� ȣ��
            nextSpawnTime = Time.time + spawnInterval; // ���� ������ ���� �ð��� ������Ʈ
        }
    }

    void SpawnItem()
    {
        // �����ϰ� �������� �����Ͽ� ����
        int randomItem = UnityEngine.Random.Range(0, 2); // UnityEngine.Random ����Ͽ� 0 �Ǵ� 1�� �����ϰ� ����
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-3f, 3f), 0f); // UnityEngine.Random ����Ͽ� ������ ���� ��ġ�� �����ϰ� ����

        GameObject itemPrefab = randomItem == 0 ? shrinkItemPrefab : ignoreCollisionItemPrefab; // ���õ� ������ �������� ����
        GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity); // ���õ� �������� ����

        Destroy(item, displayTime); // ���� �ð� �Ŀ� �������� ����
    }
}
