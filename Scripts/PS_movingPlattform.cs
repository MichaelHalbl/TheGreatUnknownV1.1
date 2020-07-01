using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_movingPlattform : MonoBehaviour
{

    public Vector3[] points;
    public int point_number = 0;
    private Vector3 current_target;
    public float tolerance; // snap clean to final destination
    public float speed; // Geschwindigkeit der Plattform
    public float delay_time; // wann bewegt sich die Plattform wieder zurück

    public float delay_start;
    public bool automatic;

    // Start is called before the first frame update
    void Start()
    {
        if(points.Length > 0){
            current_target = points[0];
        }
        tolerance = speed * Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != current_target){
            MovePlattform();
        }else{
            Updatelarget();
        }
    }

    void MovePlattform(){
        Vector3 heading = current_target - transform.position;
        transform.position += (heading/heading.magnitude) * speed * Time.deltaTime;
        if(heading.magnitude < tolerance){
            transform.position = current_target;
            delay_start = Time.time;
        }
    }

    void Updatelarget(){
        if(automatic){
            if(Time.time - delay_start > delay_time){
                NextPlatform();
            }
        }
    }

    public void NextPlatform(){
        point_number++;
        if(point_number >= points.Length){
            point_number =0;
        }
        current_target = points[point_number];
    }

    private void OnTriggerEnter(Collider other){
        other.transform.parent= transform;
    }
    private void OnTriggerExit(Collider other){
        other.transform.parent = null;
    }
}
