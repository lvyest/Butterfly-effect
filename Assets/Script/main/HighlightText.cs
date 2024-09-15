using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class HighlightText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public UnityEngine.UI.Text textComponent; // ������ Text ������Ʈ ����
    public Color highlightColor = Color.red; // �ؽ�Ʈ�� ������ �� ����� ����
    public AudioClip hoverSound; // ���콺 ���� �� ����� ȿ����

    private Color originalColor; // �ؽ�Ʈ ���� ������ ���� ����
    private AudioSource audioSource; // ����� �ҽ��� ���� ����

    void Start() 
    {
        originalColor = textComponent.color; // �ؽ�Ʈ�� ���� ������ ���� �������� ����
        audioSource = gameObject.AddComponent<AudioSource>(); // ���� ������Ʈ�� AudioSource ������Ʈ�� �߰�, ���� ����
    }

    public void OnPointerEnter(PointerEventData eventData) // ���콺 �����Ͱ� UI ��ҿ� �� �� ȣ��Ǵ� �޼���
    {
        textComponent.color = highlightColor; // ���� �������� ����
        PlaySound(hoverSound); // ȿ�������
    }

    public void OnPointerExit(PointerEventData eventData) // ���콺 �����Ͱ� UI ��ҿ��� ���� �� ȣ��
    {
        textComponent.color = originalColor; // �ؽ�Ʈ ������ ���� �������� �ǵ���
    }

    private void PlaySound(AudioClip clip){ // ����� Ŭ�� ��� �޼���
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); // ����� �ҽ��� ���� ��ȸ������ Ŭ���� ���
        }
    }
}
