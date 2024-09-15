using UnityEngine; 

public class Obstacle : MonoBehaviour 
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ� �߻� ��ġ
    public float bulletSpeed = 5f; // �Ѿ� �ӵ�
    public float fireInterval = 2f; // �߻� ����

    void Start() 
    {
        InvokeRepeating("FireBullet", fireInterval, fireInterval); // ���� �������� FireBullet �޼��带 ȣ���ϴ� Ÿ�̸� ����
    }

    void FireBullet() // �Ѿ� �߻� �޼���
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation); // �Ѿ� ����
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // �Ѿ��� Rigidbody2D ������Ʈ ����
        rb.velocity = bulletSpawnPoint.right * bulletSpeed; // bulletSpawnPoint�� ������ �������� �Ѿ� �߻�
    }
}
