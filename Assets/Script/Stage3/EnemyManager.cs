using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime; // ���� �ð��� �����ϴ� ���� ����

    public float createTime = 1; // �� ���� �ֱ⸦ �����ϴ� ���� ���� �� �ʱ�ȭ
    public GameObject[] enemyFactories; // ���� �� ���丮�� �����ϴ� �迭 ����

    public float minTime = 2f; // �� ���� �ð��� �ּҰ��� �����ϴ� ���� ���� �� �ʱ�ȭ
    public float maxTime = 6f; // �� ���� �ð��� �ִ밪�� �����ϴ� ���� ���� �� �ʱ�ȭ

    float minScale = 0.1f; // ���� �ּ� ũ�⸦ �����ϴ� ���� ���� �� �ʱ�ȭ
    float maxScale = 3.0f; // ���� �ִ� ũ�⸦ �����ϴ� ���� ���� �� �ʱ�ȭ

    private PlayerMovev1 playerMove; // PlayerMovev1 ��ũ��Ʈ�� �����ϴ� ���� ����

    void Start()
    {
        // PlayerMove ��ũ��Ʈ�� ����
        playerMove = GameObject.FindObjectOfType<PlayerMovev1>(); // ���� �� PlayerMovev1 ��ü�� ã��, ������ ����

        // �ʱ� Enemy ���� �ð� ����
        createTime = UnityEngine.Random.Range(minTime, maxTime); // �ʱ� �� ���� �ð��� minTime�� maxTime ���̿��� �������� ����
    }

    void Update()
    {
        currentTime += Time.deltaTime; // currentTime�� �� �����Ӹ��� Time.deltaTime ��ŭ ����

        if (currentTime > createTime) // ���� �ð��� ������ ���� �ð����� ū ���
        {
            // �� ���丮�� �������� ����
            int randomIndex = UnityEngine.Random.Range(0, enemyFactories.Length); // �迭���� ���� �ε����� ����
            GameObject selectedFactory = enemyFactories[randomIndex]; // ���õ� �ε����� �ش��ϴ� �� ���丮�� ����
            GameObject enemy = Instantiate(selectedFactory); // ���õ� �� ���丮�� �ν��Ͻ�ȭ (����)

            enemy.transform.position = transform.position; // ������ ���� ��ġ�� EnemyManager�� ��ġ�� ����

            // Player�� ũ�⿡ ���� Enemy�� ũ�� ���� ����
            float playerSize = playerMove.GetPlayerSize(); // Player�� ũ�⸦ ������
            if (playerSize <= 1.5) // �÷��̾��� ũ�Ⱑ 1.5���� �۰ų� ���� ���
            {
                minScale = 0.3f; // ���� �ּ� ũ�⸦ 0.3���� ����
                maxScale = 1.8f; // ���� �ִ� ũ�⸦ 1.8�� ����
            }
            else if (playerSize <= 2.0) // �÷��̾��� ũ�Ⱑ 2.0���� �۰ų� ���� ���
            {
                minScale = 0.5f; // ���� �ּ� ũ�⸦ 0.5���� ����
                maxScale = 2.5f; // ���� �ִ� ũ�⸦ 2.5�� ����
            }
            else // �÷��̾��� ũ�Ⱑ 2.0���� ū ���
            {
                minScale = 1.5f; // ���� �ּ� ũ�⸦ 1.5���� ����
                maxScale = 3.0f; // ���� �ִ� ũ�⸦ 3.0�� ����
            }

            float randomSize = UnityEngine.Random.Range(minScale, maxScale); // minScale�� maxScale ���̿��� ���� ũ�� ���� ����
            Vector3 randomScale = new Vector3(randomSize, randomSize, 1f); // ���� ũ�⸦ ����Ͽ� ���� �������� ����
            enemy.transform.localScale = randomScale; // ������ ���� ũ�⸦ ���� ũ��� ����

            // ���� Enemy ���� �ð� �� ũ�� ���� ����
            currentTime = 0; // currentTime�� 0���� �ʱ�ȭ
            createTime = UnityEngine.Random.Range(minTime, maxTime); // ���� �� ���� �ð��� minTime�� maxTime ���̿��� �������� ����
        }
    }
}
