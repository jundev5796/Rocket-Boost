using Mono.Cecil.Cil;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Start");
                break;
            case "Finish":
                Debug.Log("Finish");
                break;
            case "Fuel":
                Debug.Log("Got Fuel!");
                break;
            default:
                Debug.Log("You crashed!");
                break;
        }
    }
}
