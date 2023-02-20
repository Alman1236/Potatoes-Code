using UnityEngine;

//Pointers are the icons that indicate where offscreen potatoes and objectives are
public class PointersManager : MonoBehaviour
{
    [SerializeField] GameObject pointerAtObjectivePrefab;
    [SerializeField] GameObject pointerAtPotatoPrefab;

    [SerializeField] Transform[] objectives;
    Transform[] potatoes;

    [SerializeField] float pointersScreenOffset;

    [SerializeField] bool showPotatoesPointers = true;

    GameObject[] pointersAtObjectives;
    GameObject[] pointersAtPotatoes = new GameObject[0];

    Camera mainCam;

    private void Start()
    {
        InitializeVariables();
    }

    void InitializeVariables()
    {
        mainCam = Camera.main;

        if (!mainCam.GetComponent<CameraBehaviour>().isCameraStatic)
        {
            InstantiatePointersForPotatoes();
            InstantiatePointersForObjectives();
        }
    }

    void InstantiatePointersForPotatoes()
    {
        if (showPotatoesPointers)
        {
            Character[] characters = LevelData.instance.characters;

            potatoes = new Transform[characters.Length];
            pointersAtPotatoes = new GameObject[characters.Length];

            for (byte i = 0; i < characters.Length; i++)
            {
                potatoes[i] = characters[i].transform;
                pointersAtPotatoes[i] = Instantiate(pointerAtPotatoPrefab);
            }
        }     
    }

    void InstantiatePointersForObjectives()
    {
        pointersAtObjectives = new GameObject[objectives.Length];

        for (byte i = 0; i < objectives.Length; i++)
        {
            pointersAtObjectives[i] = Instantiate(pointerAtObjectivePrefab);
        }
    }

    private void FixedUpdate()
    {
        for (byte i = 0; i < pointersAtPotatoes.Length; i++)
        {
            UpdatePointer(pointersAtPotatoes[i], potatoes[i].position);
        }

        for (byte i = 0; i < pointersAtObjectives.Length; i++)
        {
            UpdatePointer(pointersAtObjectives[i], objectives[i].position);
        }
    }

    void UpdatePointer(GameObject pointer, Vector3 pointedPosition)
    {
        Vector3 pointedPositionScreenPoint = mainCam.WorldToScreenPoint(pointedPosition);

        if (IsPositionOffScreen(pointedPositionScreenPoint))
        {
            pointer.SetActive(true);
            SetPointerPosition(pointer, pointedPositionScreenPoint);
            SetPointerRotation(pointer, pointedPosition);
        }
        else
        {
            pointer.SetActive(false);
        }
    }

    bool IsPositionOffScreen(Vector3 position)
    {
        bool isOffScreen = false;

        if (position.x > Screen.width || position.x < 0
            || position.y > Screen.height || position.y < 0)
        {
            isOffScreen = true;
        }

        return isOffScreen;
    }

    Vector3 GetCappedPosition(Vector3 position)
    {
        if (position.x < pointersScreenOffset)
        {
            position.x = pointersScreenOffset;
        }

        if (position.x > Screen.width - pointersScreenOffset)
        {
            position.x = Screen.width - pointersScreenOffset;
        }

        if (position.y < pointersScreenOffset)
        {
            position.y = pointersScreenOffset;
        }

        if (position.y > Screen.height - pointersScreenOffset)
        {
            position.y = Screen.height - pointersScreenOffset;
        }

        return position;
    }

    void SetPointerPosition(GameObject pointer, Vector3 pointedPositionScreenPoint)
    {
        Vector3 cappedPointedPositionScreenpoint = GetCappedPosition(pointedPositionScreenPoint);
        Vector3 pointerWorldPosition = mainCam.ScreenToWorldPoint(cappedPointedPositionScreenpoint);

        pointer.transform.position = pointerWorldPosition;
        pointer.transform.position = new Vector3(pointerWorldPosition.x, pointerWorldPosition.y, 0);
    }

    void SetPointerRotation(GameObject pointer, Vector3 pointedPosition)
    {
        Vector2 lookDir = pointedPosition - pointer.transform.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 180, angle));
    }
}
