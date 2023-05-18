using UnityEngine;

namespace ArtProject.Controllers
{
    public class Cam : MonoBehaviour
    {
        public GameObject objectToFollow;

        private void Update()
        {
            if (!objectToFollow) return;
            transform.position = new Vector3(objectToFollow.transform.position.x, transform.position.y,
                transform.position.z);
        }
    }
}