using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRockV2 : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Color lineColor = Color.yellow;
    public float indicativeLineWidth;
    public GameObject player;

    public float attackDistance;
    public Vector3 attackDir;
    public Vector3 startingPoint;
    public float waitTime;
    public float existTime;
    public float speed;

    void OnEnable()
    {
        player = GameObject.Find("player");
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth =indicativeLineWidth; // set the line width
        lineRenderer.endWidth = indicativeLineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        attackDir = (player.transform.position + new Vector3(0, 0.99f, 0) - transform.position).normalized;
        Vector3 endPoint = transform.position + attackDir * attackDistance;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.enabled = true;

        startingPoint = transform.position;
        existTime = 0;

        float angle = Vector3.SignedAngle(Vector3.up, attackDir, Vector3.forward);
        Debug.Log(angle);
        Vector3 eulerAngle = new Vector3(0, 0, angle + 45);
        transform.rotation =UnityEngine.Quaternion.Euler( eulerAngle);
    }

    void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        existTime += Time.deltaTime;
        if(existTime > waitTime)
        {
            lineRenderer.enabled = false;
            GetComponent<Rigidbody2D>().velocity = speed * Time.deltaTime * attackDir;
        }
        if(Vector3.Distance(transform.position, startingPoint) >= attackDistance)
            Destroy(this.gameObject);
    }
}
