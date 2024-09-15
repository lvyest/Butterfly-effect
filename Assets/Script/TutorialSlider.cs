using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UnityEngine.UI ���ӽ����̽��� �߰��Ͽ� UI ��Ҹ� ��� �����ϰ� ��

public class TutorialSlider : MonoBehaviour
{
    public UnityEngine.UI.Image tutorialImage; // Ʃ�丮�� �̹����� �����ϴ� ���� ����
    public float delayBeforeSlideIn = 2f; // ���� �� �����̵� �� �� ��� �ð��� �����ϴ� ���� ���� �� �ʱ�ȭ
    public float slideInDuration = 1f; // �����̵� �� �ִϸ��̼��� ���� �ð��� �����ϴ� ���� ���� �� �ʱ�ȭ
    public float displayDuration = 3f; // �̹����� ȭ�鿡 ǥ�õǴ� �ð��� �����ϴ� ���� ���� �� �ʱ�ȭ
    public float slideOutDuration = 1f; // �����̵� �ƿ� �ִϸ��̼��� ���� �ð��� �����ϴ� ���� ���� �� �ʱ�ȭ

    private Vector2 startPosition; // �̹����� ���� ��ġ�� �����ϴ� ���� ����
    private Vector2 endPosition; // �̹����� �� ��ġ�� �����ϴ� ���� ����
    private RectTransform rectTransform; // Ʃ�丮�� �̹����� RectTransform ������Ʈ�� �����ϱ� ���� ���� ����

    void Start()
    {
        rectTransform = tutorialImage.GetComponent<RectTransform>(); // Ʃ�丮�� �̹����� RectTransform ������Ʈ�� ������

        // ���� ��ġ�� ȭ�� ������ ����
        startPosition = rectTransform.anchoredPosition; // ���� ��ġ�� ���� ��ġ�� ����

        // �� ��ġ�� ȭ�� ������ ���� (�ʿ信 ���� ���� ����)
        endPosition = new Vector2(startPosition.x + 500, startPosition.y); // �� ��ġ�� ����

        // ���� �� �̹����� ȭ�� ������ �̵�
        rectTransform.anchoredPosition = startPosition; // �̹����� ���� ��ġ�� ����

        // �����̵� �� �ִϸ��̼��� �����ϴ� �ڷ�ƾ ȣ��
        StartCoroutine(SlideInOut()); // �����̵� ��/�ƿ� �ִϸ��̼��� �����ϴ� �ڷ�ƾ ����
    }

    private IEnumerator SlideInOut()
    {
        // �����̵� �� �ִϸ��̼� �� ��� �ð�
        yield return new WaitForSeconds(delayBeforeSlideIn); // ������ �ð� ���� ���

        // �����̵� �� �ִϸ��̼�
        yield return StartCoroutine(Slide(startPosition, endPosition, slideInDuration)); // ���� ��ġ���� �� ��ġ�� �����̵� �� �ִϸ��̼� ����

        // ������ �ð� ���� �̹����� ȭ�鿡 ǥ��
        yield return new WaitForSeconds(displayDuration); // �̹����� ȭ�鿡 ǥ�õǴ� ���� ���

        // �����̵� �ƿ� �ִϸ��̼�
        yield return StartCoroutine(Slide(endPosition, startPosition, slideOutDuration)); // �� ��ġ���� ���� ��ġ�� �����̵� �ƿ� �ִϸ��̼� ����
    }

    private IEnumerator Slide(Vector2 from, Vector2 to, float duration)
    {
        float elapsedTime = 0f; // ��� �ð��� �����ϴ� ���� �ʱ�ȭ

        while (elapsedTime < duration)
        {
            // ��� �ð��� ���� ��ġ�� ���� (lerp)�Ͽ� �ε巴�� �̵�
            rectTransform.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / duration); // ���� ��ġ���� �� ��ġ���� ������ ���� ��ġ�� ����
            elapsedTime += Time.deltaTime; // ��� �ð��� ������Ʈ
            yield return null; // ���� �����ӱ��� ���
        }

        // �ִϸ��̼��� ���� �� ���� ��ġ�� ��Ȯ�ϰ� ����
        rectTransform.anchoredPosition = to; // ���� ��ġ�� ����
    }
}
