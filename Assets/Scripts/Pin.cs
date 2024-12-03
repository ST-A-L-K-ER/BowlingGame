using UnityEngine;

// ����� ��� ���������� ���������� �����
public class Pin : MonoBehaviour
{
    public bool hasFallen = false; // ����, �����������, ��� ����� ��� ����� � ���� ������
    public bool IsFallen = false; // ����, �����������, ��� ����� ����� �������
    private Vector3 pinPosition; // ��������� ������� �����
    private Quaternion pinRotation; // ��������� ���� �������� �����

    // �����, ���������� ��� ������
    void Start()
    {
        pinPosition = transform.position; // ��������� ��������� ������� �����
        pinRotation = transform.rotation; // ��������� ��������� ���� �������� �����
    }

    // �����, ���������� ������ ����
    void Update()
    {
        var pinPhysics = GetComponent<Rigidbody>(); // �������� ��������� ������ Rigidbody ��� �����

        // ���������, ���������� �� ������� ��� ���� �������� �����
        if (!hasFallen && pinPhysics.rotation != pinRotation && pinPhysics.position != pinPosition)
        {
            hasFallen = true; // ��������, ��� ����� �����
        }
    }

    // ����� ��� ������ ��������� ����� � ��������� ���������
    public void ResetPinState()
    {
        hasFallen = false; // ���������� ���� "����� �����"
        IsFallen = false; // ���������� ���� "����� ����� �������"

        var pinPhysics = GetComponent<Rigidbody>(); // �������� ��������� ������ Rigidbody ��� �����

        pinPhysics.velocity = Vector3.zero; // �������� �������� ��������
        pinPhysics.position = pinPosition; // ���������� ����� � ��������� �������
        pinPhysics.rotation = pinRotation; // ���������� ����� � ��������� ���� ��������
        pinPhysics.angularVelocity = Vector3.zero; // �������� ������� ��������
    }
}