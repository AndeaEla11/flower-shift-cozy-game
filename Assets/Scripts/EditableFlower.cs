using UnityEngine;

public class EditableFlower : MonoBehaviour
{
    private static EditableFlower currentSelected;

    [SerializeField] private float yOffset = 0.03f;
    [SerializeField] private float maxMovementX = 0.18f;
    [SerializeField] private float maxMovementZ = 0.18f;
    [SerializeField] private float rotationSpeed = 120f;
    [SerializeField] private float tiltSpeed = 120f;
    [SerializeField] private float maxTilt = 35f;

    private Camera worldCamera;
    private LayerMask bouquetSurfaceMask;
    private Transform bouquetBasePoint;

    private bool isSelected = false;
    private bool isDragging = false;

    private float currentYRotation = 0f;
    private float currentTiltX = 0f;
    private float currentTiltZ = 0f;

    public void Setup(Camera cameraToUse, LayerMask surfaceMask, Transform basePoint)
    {
        worldCamera = cameraToUse;
        bouquetSurfaceMask = surfaceMask;
        bouquetBasePoint = basePoint;

        currentYRotation = transform.eulerAngles.y;
        ApplyRotation();
    }

    public void SetSelected(bool value)
    {
        if (value)
        {
            if (currentSelected != null && currentSelected != this)
            {
                currentSelected.isSelected = false;
                currentSelected.isDragging = false;
            }

            currentSelected = this;
            isSelected = true;
        }
        else
        {
            isSelected = false;
            isDragging = false;

            if (currentSelected == this)
            {
                currentSelected = null;
            }
        }
    }

    private void OnMouseDown()
    {
        SetSelected(true);
        isDragging = true;
    }

    private void Update()
    {
        if (!isSelected)
        {
            return;
        }

        if (isDragging && Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            MoveFlowerWithMouse();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            currentYRotation += scroll * rotationSpeed;
            ApplyRotation();
        }

        if (Input.GetMouseButton(1))
        {
            TiltFlower();
        }
    }

    void MoveFlowerWithMouse()
    {
        if (worldCamera == null || bouquetBasePoint == null)
        {
            return;
        }

        Ray ray = worldCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, bouquetSurfaceMask))
        {
            Vector3 targetPosition = hit.point;
            Vector3 offsetFromCenter = targetPosition - bouquetBasePoint.position;

            offsetFromCenter.x = Mathf.Clamp(offsetFromCenter.x, -maxMovementX, maxMovementX);
            offsetFromCenter.z = Mathf.Clamp(offsetFromCenter.z, -maxMovementZ, maxMovementZ);

            Vector3 finalPosition = new Vector3(bouquetBasePoint.position.x + offsetFromCenter.x, hit.point.y + yOffset, bouquetBasePoint.position.z + offsetFromCenter.z); 

            transform.position = finalPosition;
        }
    }

    void TiltFlower()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentTiltX += -mouseY * tiltSpeed * Time.deltaTime;
        currentTiltZ += mouseX * tiltSpeed * Time.deltaTime;

        currentTiltX = Mathf.Clamp(currentTiltX, -maxTilt, maxTilt);
        currentTiltZ = Mathf.Clamp(currentTiltZ, -maxTilt, maxTilt);

        ApplyRotation();
    }

    void ApplyRotation()
    {
        transform.rotation = Quaternion.Euler(currentTiltX, currentYRotation, currentTiltZ);
    }
}
