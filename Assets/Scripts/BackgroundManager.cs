using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject water;

    [SerializeField] private float waterSpeed;

    [SerializeField] private Vector2 waterEndPosition;

    private float offset;

    private void Start()
    {
        offset = transform.position.y - player.transform.position.y;
    }


    void LateUpdate()
    {
        if (water.transform.position.x == waterEndPosition.x)
        {
            water.transform.position = new Vector2(0, water.transform.position.y);
        } else
        {
            water.transform.localPosition = new Vector2(Vector2.MoveTowards(new Vector2(water.transform.position.x, water.transform.position.y), new Vector2(waterEndPosition.x, water.transform.position.y), Time.deltaTime * waterSpeed).x, 0);
        }
        background.transform.position = new Vector2(transform.position.x, player.transform.position.y + offset);
    }
}
