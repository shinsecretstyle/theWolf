using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Moon : MonoBehaviour
{
    public GameObject Player;
    private player thePlayer;
    private PlayerWithAnim player;

    [SerializeField]
    private int rayNums = 360;
    [SerializeField]
    private float radius;

    public LayerMask layerMask;
    
    CircleCollider2D moonCollider;

    public GameObject moonObject;
    void Start()
    {
        moonCollider = GetComponent<CircleCollider2D>();
        thePlayer = Player.GetComponent<player>();
        player = Player.GetComponent<PlayerWithAnim>();
        Light2D moonData = moonObject.GetComponent<Light2D>();
        radius = moonData.pointLightOuterRadius;
    }

    void Update()
    {
        if (isInLight())
        {
            //thePlayer.checkLight(true);
            player.checkLight(true);
        }
        else
        {
            //thePlayer.checkLight(false);
            player.checkLight(false);
        }
    }

    bool isInLight()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> colliderPoints = new List<Vector2>();

        vertices.Add(Vector3.zero);

        for (int i = 0; i < rayNums; i++)
        {
            float angle = (i / (float)rayNums) * 360f;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, radius, layerMask);

            Vector3 vertex;
            if (hit.collider != null)
            {
                vertex = transform.InverseTransformPoint(hit.point);
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
            
        }
        return false;
    }
}
