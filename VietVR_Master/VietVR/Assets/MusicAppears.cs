namespace VRTK.Examples
{

    using UnityEngine;

    public class MusicAppears : MonoBehaviour
    {

        [SerializeField] GameObject music;
        public VRTK_InteractableObject linkedObject;
        protected bool musicAppear;


        protected virtual void Update()
        {
            if (musicAppear)
            {
                music.SetActive(true);
            }

        }

        protected virtual void OnEnable()
        {

            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            }

        }
        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            musicAppear = true;
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
            musicAppear = false;
        }


    }
}
