using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public Transform AudienceHolder;
    public GameObject PersonPrefab;

    void Start()
    {
        for (float i = 0; i < 360; i += 3.6f)
            InstantiateObject(i, 45, 11);

        for (float i = 0; i < 360; i += 3f)
            InstantiateObject(i, 47, 14);


        for (float i = 0; i < 360; i += 2.6f)
            InstantiateObject(i, 49, 17);

        for (float i = 0; i < 360; i += 2.3f)
            InstantiateObject(i, 51, 20);
    }

    private void InstantiateObject(float degrees, int scale, int height)
    {
        var obj = Instantiate(PersonPrefab, AudienceHolder);
        var point = GetPoint(degrees) * scale;
        obj.GetComponent<Renderer>().material.SetColor("_Color",
        new Color(
            Random.Range(0.3f, 1f),
            Random.Range(0.3f, 1f),
            Random.Range(0.3f, 1f)));

        obj.transform.position = new Vector3(point.x, height, point.z);
        obj.transform.LookAt(Vector3.zero, Vector3.up);
    }

    private Vector3 GetPoint(float degree)
    {
        //negative transform for the correct rotation
        var cos = Mathf.Cos((-transform.localEulerAngles.y + degree) * Mathf.Deg2Rad);
        var sin = Mathf.Sin((-transform.localEulerAngles.y + degree) * Mathf.Deg2Rad);
        return new Vector3(cos, 0f, sin);
    }

}
