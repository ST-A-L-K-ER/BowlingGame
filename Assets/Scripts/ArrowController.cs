using UnityEngine;

// ����� ��� ���������� ��������, ����������� ����������� � ���� ������
public class ArrowController : MonoBehaviour
{
    private Vector3 arrowScale; // ��������� ������� �������
    private Quaternion arrowRotation; // ��������� ���� �������� �������

    // �����, ���������� ��� ������
    void Start()
    {
        arrowScale = transform.localScale; // ��������� ��������� ������� �������
        arrowRotation = transform.rotation; // ��������� ��������� �������� �������
    }

    // �����, ���������� ������ ����
    void Update()
    {
        // ������� ������� ����� ��� ������� ������� LeftArrow
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down, Time.deltaTime * 30f); // ������������ ������� ������ ������� �������
        }

        // ������� ������� ������ ��� ������� ������� RightArrow
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 30f); // ������������ ������� �� ������� �������
        }

        // ���������� ����� ������� ��� ������� ������� UpArrow (���������� ���� ������)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.localScale.z < 2) // ������������ ������������ ����� �������
            {
                transform.localScale = new Vector3(
                    transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z + (1 * Time.deltaTime)); // ����������� ����� �������
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 2); // ������������ �� 2
            }
        }

        // ���������� ����� ������� ��� ������� ������� DownArrow (���������� ���� ������)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.localScale.z > 0.1f) // ������������ ����������� ����� �������
            {
                transform.localScale = new Vector3(
                    transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z - (1 * Time.deltaTime)); // ��������� ����� �������
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.1f); // ������������ �� 0.1
            }
        }
    }

    // ����� ��� ������ ��������� ������� � ��������� ���������
    public void ResetArrowState()
    {
        transform.localScale = arrowScale; // ��������������� ��������� �������
        transform.rotation = arrowRotation; // ��������������� ��������� ��������
    }
}
