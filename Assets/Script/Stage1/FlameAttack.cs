using UnityEngine;
using System.Collections; 

public class FlameAttack : MonoBehaviour 
{
    public GameObject fireballPrefab; // �Ҿ� ������
    public Transform fireballSpawnPoint; // �Ҿ� ���� ��ġ
    public float fireballSpeed = 5f; // �Ҿ� �ӵ�
    public float fireballSpawnInterval = 2f; // �Ҿ� ���� ����
    public Transform player; // �÷��̾� ��ġ ����

    void Start() // ���� ������Ʈ�� Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
    {
        StartCoroutine(SpawnFireball()); // �Ҿ� ���� �ڷ�ƾ ����
    }

    IEnumerator SpawnFireball() // �Ҿ��� �ֱ������� �����ϴ� �ڷ�ƾ �޼���
    {
        while (true) // ���� ����
        {
            yield return new WaitForSeconds(fireballSpawnInterval); // ������ ���ݸ�ŭ ���

            // �Ҿ� ����
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity); // �Ҿ� ������ ����

            // �÷��̾��� ���� ��ġ�� ���ϵ��� ���� ����
            Vector2 direction = (player.position - fireballSpawnPoint.position).normalized; // ���� ���� ��� �� ����ȭ
            fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed; // �Ҿ��� �ӵ� ����
        }
    }
}
