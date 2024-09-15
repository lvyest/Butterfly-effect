using UnityEngine; 

public class Obstacle : MonoBehaviour 
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform bulletSpawnPoint; // 총알 발사 위치
    public float bulletSpeed = 5f; // 총알 속도
    public float fireInterval = 2f; // 발사 간격

    void Start() 
    {
        InvokeRepeating("FireBullet", fireInterval, fireInterval); // 일정 간격으로 FireBullet 메서드를 호출하는 타이머 설정
    }

    void FireBullet() // 총알 발사 메서드
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation); // 총알 생성
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // 총알의 Rigidbody2D 컴포넌트 참조
        rb.velocity = bulletSpawnPoint.right * bulletSpeed; // bulletSpawnPoint의 오른쪽 방향으로 총알 발사
    }
}
