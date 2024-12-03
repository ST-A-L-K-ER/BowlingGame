using UnityEngine;

// Класс для управления стрелкой, указывающей направление и силу броска
public class ArrowController : MonoBehaviour
{
    private Vector3 arrowScale; // Начальный масштаб стрелки
    private Quaternion arrowRotation; // Начальный угол поворота стрелки

    // Метод, вызываемый при старте
    void Start()
    {
        arrowScale = transform.localScale; // Сохраняем начальный масштаб стрелки
        arrowRotation = transform.rotation; // Сохраняем начальное вращение стрелки
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        // Поворот стрелки влево при нажатии клавиши LeftArrow
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down, Time.deltaTime * 30f); // Поворачиваем стрелку против часовой стрелки
        }

        // Поворот стрелки вправо при нажатии клавиши RightArrow
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 30f); // Поворачиваем стрелку по часовой стрелке
        }

        // Увеличение длины стрелки при нажатии клавиши UpArrow (увеличение силы броска)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.localScale.z < 2) // Ограничиваем максимальную длину стрелки
            {
                transform.localScale = new Vector3(
                    transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z + (1 * Time.deltaTime)); // Увеличиваем длину стрелки
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 2); // Ограничиваем до 2
            }
        }

        // Уменьшение длины стрелки при нажатии клавиши DownArrow (уменьшение силы броска)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.localScale.z > 0.1f) // Ограничиваем минимальную длину стрелки
            {
                transform.localScale = new Vector3(
                    transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z - (1 * Time.deltaTime)); // Уменьшаем длину стрелки
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.1f); // Ограничиваем до 0.1
            }
        }
    }

    // Метод для сброса состояния стрелки в начальное положение
    public void ResetArrowState()
    {
        transform.localScale = arrowScale; // Восстанавливаем начальный масштаб
        transform.rotation = arrowRotation; // Восстанавливаем начальное вращение
    }
}
