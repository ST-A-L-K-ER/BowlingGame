using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// �������� �����, ����������� ������� ����
public class GameManager : MonoBehaviour
{
    public int currentFrame = 1; // ������� ����� (�� 1 �� 10)
    public int currentRoll = 1; // ������� ������ (1 ��� 2 � ������ ������ ������)
    private int playerRollIndex = 0; // ������ �������� ������ ������ � ������� �������
    private int computerRollIndex = 0; // ������ �������� ������ ���������� � ������� �������
    public int[] playerRolls = new int[21]; // ������ ������� ������ (�������� 21 ������)
    public int[] computerRolls = new int[21]; // ������ ������� ���������� (�������� 21 ������)

    public int playerScore = 0; // ������� ���� ������
    public int computerScore = 0; // ������� ���� ����������

    private BallController ball; // ������ �� ��������� ���������� �����
    public Pin[] pins; // ������ �� ��� �����
    private ScoreUIManager UI; // ������ �� �������� ����������������� ����������

    // ������������� ����
    void Start()
    {
        pins = FindObjectsOfType<Pin>(); // ������� ��� ����� �� �����
        ball = FindObjectOfType<BallController>(); // ������� ��� �� �����
        UI = FindObjectOfType<ScoreUIManager>(); // ������� ������������ ��������
    }

    // ������ ����������� ����� ������
    public void RecordPinsAfterThrow()
    {
        int pinsKnockedDown = 0; // ������� ������ ������

        foreach (Pin pin in pins)
        {
            if (pin.hasFallen && !pin.IsFallen) // ���� ����� ����� � ��� �� ���� ������
            {
                pinsKnockedDown++;
                pin.IsFallen = true; // ��������, ��� ����� �����
            }
        }

        // ���������� ��������� � ����������� �� �������� ������
        if (ball.isPlayerTurn)
        {
            RecordPlayerRoll(pinsKnockedDown);
        }
        else
        {
            RecordComputerrRoll(pinsKnockedDown);
        }
    }

    // ������ ������ ������
    public void RecordPlayerRoll(int pinsKnockedDown)
    {
        playerRolls[playerRollIndex] = pinsKnockedDown; // ��������� ��������� ������
        bool isSpare = currentRoll == 2 && (playerRolls[playerRollIndex - 1] + pinsKnockedDown == 10); // ��������� �� ����
        UI.DisplayPlayerScore(playerRollIndex, pinsKnockedDown, isSpare); // ��������� ���������

        playerRollIndex++;

        Debug.Log($"������ ������ {currentRoll} � ������ {currentFrame}: ����� {pinsKnockedDown} ������.");

        // ��������� ���������� ������
        if (currentRoll == 1 && pinsKnockedDown == 10) // ������
        {
            ball.isPlayerTurn = false; // ������� ��� ����������
            currentRoll = 1;
            ball.ResetBall();
            ResetPins();
        }
        else if (currentRoll == 2) // ���� ��� ������ ������ ��������
        {
            ball.isPlayerTurn = false; // ������� ��� ����������
            currentRoll = 1;
            ball.ResetBall();
            ResetPins();
        }
        else
        {
            currentRoll++; // ��������� �� ������� ������
            Shreder(); // ������� ������ �����
            ball.ResetBall();
        }

        CalculatePlayerScore(); // ������������� ���� ������
    }

    // ������ ������ ����������
    public void RecordComputerrRoll(int pinsKnockedDown)
    {
        computerRolls[computerRollIndex] = pinsKnockedDown; // ��������� ��������� ������
        bool isSpare = currentRoll == 2 && (computerRolls[computerRollIndex - 1] + pinsKnockedDown == 10); // ��������� �� ����
        UI.DisplayComputerScore(computerRollIndex, pinsKnockedDown, isSpare); // ��������� ���������

        computerRollIndex++;

        Debug.Log($"������ ���������� {currentRoll} � ������ {currentFrame}: ����� {pinsKnockedDown} ������.");

        // ��������� ���������� ������
        if (currentRoll == 1 && pinsKnockedDown == 10) // ������
        {
            ball.isPlayerTurn = true; // ������� ��� ������
            NextFrame(); // ��������� � ���������� ������
        }
        else if (currentRoll == 2) // ���� ��� ������ ������ ��������
        {
            ball.isPlayerTurn = true; // ������� ��� ������
            NextFrame(); // ��������� � ���������� ������
        }
        else
        {
            currentRoll++; // ��������� �� ������� ������
            Shreder(); // ������� ������ �����
            ball.ResetBall();
        }

        CalculateComputerScore(); // ������������� ���� ����������
    }

    // ������� � ���������� ������
    private void NextFrame()
    {
        currentFrame++;
        currentRoll = 1;

        if (currentFrame == 11) // ���� ����� 11-�, ���� ���������
        {
            StartCoroutine(WaitBeforeEnd());
        }
        else
        {
            ball.ResetBall();
            ResetPins();
        }
    }

    // ������� ����� ������
    private void CalculatePlayerScore()
    {
        int score = 0;
        int rollIndex = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (playerRolls[rollIndex] == 10) // ������
            {
                score += 10 + playerRolls[rollIndex + 1] + playerRolls[rollIndex + 2];
                rollIndex++;
            }
            else if (playerRolls[rollIndex] + playerRolls[rollIndex + 1] == 10) // ����
            {
                score += 10 + playerRolls[rollIndex + 2];
                rollIndex += 2;
            }
            else // ���� �����
            {
                score += playerRolls[rollIndex] + playerRolls[rollIndex + 1];
                rollIndex += 2;
            }
        }

        playerScore = score;
        EndScore.playerScore = score;
        Debug.Log("����� ���� ������: " + score);
    }

    // ������� ����� ����������
    private void CalculateComputerScore()
    {
        int score = 0;
        int rollIndex = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (computerRolls[rollIndex] == 10) // ������
            {
                score += 10 + computerRolls[rollIndex + 1] + computerRolls[rollIndex + 2];
                rollIndex++;
            }
            else if (computerRolls[rollIndex] + computerRolls[rollIndex + 1] == 10) // ����
            {
                score += 10 + computerRolls[rollIndex + 2];
                rollIndex += 2;
            }
            else // ���� �����
            {
                score += computerRolls[rollIndex] + computerRolls[rollIndex + 1];
                rollIndex += 2;
            }
        }

        computerScore = score;
        EndScore.computerScore = score;
        Debug.Log("����� ���� ����������: " + score);
    }

    // ����� ��������� ���� ������
    public void ResetPins()
    {
        foreach (Pin pin in pins)
        {
            pin.gameObject.SetActive(true);
            pin.ResetPinState();
        }
    }

    // ������� ������ ����� � ����
    private void Shreder()
    {
        foreach (Pin pin in pins)
        {
            if (pin.hasFallen)
            {
                pin.gameObject.SetActive(false);
            }
        }
    }

    // �������� ����� ����������� ����
    IEnumerator WaitBeforeEnd()
    {
        yield return new WaitForSecondsRealtime(3);

        Debug.Log("���� ���������!");
        SceneManager.LoadScene("End"); // ������� � ����� ���������� ����
    }
}
