using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovev1 : MonoBehaviour
{
    public float speed = 2; // �÷��̾��� �̵� �ӵ��� �����ϴ� ���� ���� �� �ʱ�ȭ
    Vector3 moveL = new Vector3(-1, 0, 0); // ���� ������ ��Ÿ���� ���� ����
    Vector3 moveU = new Vector3(0, 1, 0); // ���� ������ ��Ÿ���� ���� ����

    private Coroutine speedIncreaseCoroutine; // �÷��̾� �ӵ� ���� �ڷ�ƾ�� �����ϴ� ���� ����
    private float playerSize; // �÷��̾��� ũ�⸦ �����ϴ� ���� ����

    private UIManager uiManager; // UIManager ��ũ��Ʈ�� �����ϱ� ���� ���� ����

    // Animator ������Ʈ�� �����ϱ� ���� �ʵ� ����
    private Animator animator;

    private void Start()
    {
        // �÷��̾��� �ʱ� ũ�� ����
        playerSize = transform.localScale.y; // �÷��̾��� ���� ũ�⸦ ����
        uiManager = FindObjectOfType<UIManager>(); // UIManager ��ũ��Ʈ�� ã�Ƽ� ������ ����

        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>(); // ���� ���� ������Ʈ���� Animator ������Ʈ�� ������
        if (animator == null)
        {
            UnityEngine.Debug.LogError("Animator component is not found on the object."); // Animator�� ������ ���� �޽��� ���
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // �浹�� ��ü�� ���� ��츦 Ȯ��
        {
            Vector3 enemySize = other.transform.localScale; // �浹�� ���� ũ�⸦ ������

            // �÷��̾�� ���� ũ�⸦ ���Ͽ� ���� ���� ���� Ȯ��
            if (enemySize.y > transform.localScale.y) // ���� ũ�Ⱑ �÷��̾�� ū ���
            {
                UnityEngine.Debug.Log("Game Over"); // ���� ���� �޽��� ���
                Destroy(gameObject); // �÷��̾ �ı�

                // ���� ���� ó���� UI�� �˸�
                uiManager.ShowGameOverUI(); // ���� ���� UI ǥ�� �޼��� ȣ��
            }

            // �÷��̾� ����� 10 �̻��� �� ���� Ŭ����
            if (playerSize >= 10) // �÷��̾��� ũ�Ⱑ 10 �̻��� ���
            {
                UnityEngine.Debug.Log("Game Clear"); // ���� Ŭ���� �޽��� ���
                transform.position -= moveL * speed * Time.deltaTime; // �÷��̾ �������� �̵�

                // ���� Ŭ���� ó���� UI�� �˸�
                uiManager.ShowGameClearUI(); // ���� Ŭ���� UI ǥ�� �޼��� ȣ��
            }
            else
            {
                // �÷��̾��� ũ�� ������ ����
                float growthRate = playerSize <= 1.5f ? 0.5f : 0.2f; // �÷��̾��� ũ�⿡ ���� ������ ����

                // �÷��̾��� ũ�⸦ �浹�� ���� ��������ŭ ������Ŵ
                Vector3 newPlayerSize = transform.localScale + new Vector3(enemySize.y * growthRate, enemySize.y * growthRate, 0f);

                // ���ο� �÷��̾� ũ�⸦ ������ ��, ���� �÷��̾� ũ�⺸�� Ŭ ���� ����
                if (newPlayerSize.y > transform.localScale.y)
                {
                    transform.localScale = newPlayerSize; // �÷��̾��� ũ�⸦ ������ ũ��� ����

                    // �浹�� ���� ����
                    Destroy(other.gameObject); // �浹�� �� ��ü�� �ı�

                    // �÷��̾��� ũ�� ������Ʈ
                    playerSize = transform.localScale.y; // ������Ʈ�� �÷��̾� ũ�⸦ ����
                }
            }
        }
    }

    void Update()
    {
        // �÷��̾� �̵�
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += moveU * speed * Time.deltaTime; // ���� �̵�
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.position -= moveU * speed * Time.deltaTime; // �Ʒ��� �̵�

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position += moveL * speed * Time.deltaTime; // �������� �̵�
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.position -= moveL * speed * Time.deltaTime; // ���������� �̵�

        // �����̽��ٸ� ������ �� �ִϸ��̼� Ʈ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetTrigger("flap"); // "flap" �ִϸ��̼� Ʈ���� ����
            }
        }
    }

    // �÷��̾��� ũ�⸦ ��ȯ�ϴ� �޼���
    public float GetPlayerSize()
    {
        return playerSize; // ���� �÷��̾��� ũ�⸦ ��ȯ
    }

    // �÷��̾��� �ӵ��� ������Ű�� �޼��� (���� �ð� ���ȸ� ����)
    public void IncreaseSpeed(float duration)
    {
        if (speedIncreaseCoroutine != null)
            StopCoroutine(speedIncreaseCoroutine); // ������ �ӵ� ���� �ڷ�ƾ�� ������ ����

        speedIncreaseCoroutine = StartCoroutine(IncreaseSpeedForLimitedTime(duration)); // ���ο� �ӵ� ���� �ڷ�ƾ ����
    }

    // ���� �ð� ���� �ӵ��� ������Ű�� �ڷ�ƾ
    private IEnumerator IncreaseSpeedForLimitedTime(float duration)
    {
        speed *= 2; // �ӵ��� �� ��� ����
        yield return new WaitForSeconds(duration); // ������ �ð� ���� ���
        speed /= 2; // �ӵ��� ������� �ǵ���
    }

    // �÷��̾��� ũ�⸦ ���̴� �޼���
    public void ShrinkPlayer()
    {
        transform.localScale *= 0.7f; // �÷��̾��� ũ�⸦ 70%�� ����
    }
}
