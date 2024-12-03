using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// Класс для управления шаром в игре
public class BallController : MonoBehaviour
{
    public float startSpeed = 40f; // Начальная скорость броска

    private Rigidbody rb; // Компонент Rigidbody для управления физикой шара
    public Vector3 ballPosition; // Начальная позиция шара

    private bool isLaunched = false; // Флаг, указывающий, что шар был запущен
    private bool PCLaunched = false; // Флаг, указывающий, что компьютер запустил шар
    public bool isPlayerTurn = true; // Чей ход (игрока или компьютера)

    private Transform _arrow; // Указатель стрелки, показывающей направление броска

    // Метод, вызываемый при старте
    void Start()
    {
        _arrow = GameObject.FindGameObjectWithTag("Arrow").transform; // Находим объект стрелки
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
        ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position; // Сохраняем стартовую позицию шара
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        if (!isLaunched) // Если шар ещё не запущен
        {
            // Если ход игрока и нажата клавиша пробела
            if (isPlayerTurn && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(LaunchPlayerBall()); // Запускаем шар игрока
            }

            // Если ход компьютера и он ещё не запустил шар
            if (!isPlayerTurn && !PCLaunched)
            {
                StartCoroutine(LaunchComputerBall()); // Запускаем шар компьютера
                PCLaunched = true; // Устанавливаем флаг запуска компьютера
            }
        }
    }

    // Метод для запуска шара игрока
    IEnumerator LaunchPlayerBall()
    {
        _arrow.gameObject.SetActive(false); // Скрываем стрелку

        isLaunched = true; // Устанавливаем флаг, что шар запущен
        rb.isKinematic = false; // Включаем физику для шара

        // Вычисляем направление и силу броска
        Vector3 forceVector = _arrow.forward * (startSpeed * _arrow.transform.localScale.z);
        Vector3 forcePosition = transform.position + (transform.right * 0.5f);

        // Прикладываем силу к шару
        rb.AddForceAtPosition(forceVector, forcePosition, ForceMode.Impulse);

        yield return new WaitForSecondsRealtime(10); // Ждём 10 секунд

        FindObjectOfType<GameManager>().RecordPinsAfterThrow(); // Запускаем подсчёт сбитых кеглей
    }

    // Метод для запуска шара компьютера
    IEnumerator LaunchComputerBall()
    {
        _arrow.Rotate(Vector3.down, 90); // Поворачиваем стрелку
        _arrow.gameObject.SetActive(false); // Скрываем стрелку

        yield return new WaitForSecondsRealtime(2); // Делаем паузу перед броском

        isLaunched = true; // Устанавливаем флаг, что шар запущен
        rb.isKinematic = false; // Включаем физику для шара

        // Вычисляем направление и силу броска
        Vector3 targetDirection = CalculateComputerThrowDirection();
        float throwForce = CalculateComputerThrowForce();
        Vector3 forcePosition = transform.position + (transform.right * 0.5f);

        // Выполняем бросок
        rb.AddForceAtPosition(targetDirection * throwForce, forcePosition, ForceMode.Impulse);

        yield return new WaitForSecondsRealtime(10); // Ждём 10 секунд

        FindObjectOfType<GameManager>().RecordPinsAfterThrow(); // Запускаем подсчёт сбитых кеглей
    }

    // Метод для вычисления направления броска компьютера
    private Vector3 CalculateComputerThrowDirection()
    {
        Vector3 direction = _arrow.forward; // Базовое направление (вперед)
        float randomness = Random.Range(-0.1f, 0.1f); // Добавляем небольшую погрешность
        direction.z += randomness; // Вносим погрешность в направлении по оси Z

        return direction.normalized; // Возвращаем нормализованное направление
    }

    // Метод для вычисления силы броска компьютера
    private float CalculateComputerThrowForce()
    {
        float randomness = Random.Range(-36f, 40f); // Добавляем случайный разброс к силе броска
        return Mathf.Clamp(startSpeed + randomness, 0f, 85f); // Ограничиваем силу броска в заданных пределах
    }

    // Метод для сброса состояния шара в начальное положение
    public void ResetBall()
    {
        rb.velocity = Vector3.zero; // Сбрасываем скорость
        rb.angularVelocity = Vector3.zero; // Сбрасываем вращение
        transform.position = ballPosition; // Возвращаем в начальную позицию
        transform.rotation = Quaternion.identity; // Сбрасываем угол поворота

        isLaunched = false; // Сбрасываем флаг запуска шара
        PCLaunched = false; // Сбрасываем флаг запуска компьютера
        rb.isKinematic = false; // Включаем физику

        _arrow.gameObject.SetActive(true); // Показываем стрелку
        FindObjectOfType<ArrowController>().ResetArrowState(); // Сбрасываем состояние стрелки
    }
}
