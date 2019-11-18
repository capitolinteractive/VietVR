#define LIGHTSPEED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
namespace WPM
{
    public class GlobeEventHandler : MonoBehaviour {

        public static GlobeEventHandler Current { get; private set; }

        WorldMapGlobe map;
        GUIStyle labelStyle, labelStyleShadow, buttonStyle, sliderStyle, sliderThumbStyle;
        ColorPicker colorPicker;
        bool changingFrontiersColor;
        bool minimizeState = false;
        bool animatingField;
        float zoomLevel = 1.0f;
        bool movin;

        Coroutine coru;

        /*
        public GameObject NYpoint;

        public GameObject NVietnam;
        public GameObject SVietnam;
        */

        // Use this for initialization
        void Start() {
            map = WorldMapGlobe.instance;
            Current = this;

            

        #if LIGHTSPEED
            //Camera.main.fieldOfView = 180;
            //animatingField = true;
        #endif
            map.earthInvertedMode = false;
        }

        /*
        // Update is called once per frame
        void Update() {

          
        }
           */

        public IEnumerator doThing(float duration,string country, GameObject location)
        {
            Quaternion start = transform.localRotation, end = Quaternion.Inverse(Quaternion.LookRotation(-location.transform.localPosition));
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                transform.localRotation = Quaternion.Slerp(start, end, t / duration);
                if(t>= duration - (duration/50))
                {
                    //FlyToCountry(country);
                    if(country == "North Vietnam")
                    {
                        FlyToCity("Hanoi");
                    }
                    else if (country == "South Vietnam")
                    {
                        FlyToCity("Play Ku");
                    }
                    else if(country == "United States of America")
                    {
                        FlyToCity("Baltimore");
                    }
                    else if(country == "East Germany")
                    {
                        FlyToCity("Berlin");
                    }
                    else if(country == "West Germany")
                    {
                        FlyToCity("Frankfurt");
                    }
                    
                    else
                    {
                        FlyToCountry(country);
                    }
                }
                yield return null;
            }

            //transform.rotation = Quaternion.Slerp(start, end, 1.0f);
            // same thing as:
            //transform.rotation = end;
        }
        public void GoTo(float dur,string country, GameObject location)
        {
            
            if (coru != null)
            {
                StopCoroutine(coru);
            }
           
            coru = StartCoroutine(doThing(2,country,location));
            FlyToCountry(country);

        }


        // Sample code to show how to:
        // 1.- Navigate and center a country in the map
        // 2.- Add a blink effect to one country (can be used on any number of countries)
        void FlyToCountry(string countryName)
        {
            int countryIndex = map.GetCountryIndex(countryName);
            float zoomLevel = map.GetCountryMainRegionZoomExtents(countryIndex);
            map.FlyToCountry(countryIndex, 1f, zoomLevel, 0.1f);
            map.BlinkCountry(countryIndex, Color.black, Color.green, 4, 2.5f, true);
        }

        // Sample code to show how to navigate to a city:
        void FlyToCity(string cityName)
        {
            int cityIndex = map.GetCityIndex(cityName);
            map.FlyToCity(cityIndex, 1f, 0.2f, 0.1f);
        }

        /*
        #region Bullet shooting!

        /// <summary>
        /// Creates a simple gameobject (sphere) on current map position and launch it over a random position on the globe following an arc
        /// </summary>
        void FireBullet()
        {

            // Create a "bullet" with a simple sphere at current map position from camera perspective
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.GetComponent<Renderer>().material.color = Color.yellow;

            // Choose starting pos
            Vector3 startPos = map.GetCurrentMapLocation();

            // Get a random target city
            int randomCity = Random.Range(0, map.cities.Count);
            Vector3 endPos = map.cities[randomCity].unitySphereLocation;

            // Fire the bullet!
            StartCoroutine(AnimateBullet(sphere, 0.01f, startPos, endPos));
        }

        IEnumerator AnimateBullet(GameObject sphere, float scale, Vector3 startPos, Vector3 endPos, float duration = 3f, float arc = 0.25f)
        {

            // Optional: Draw the trajectory
            map.AddLine(startPos, endPos, Color.red, arc, duration, 0.002f, 0.1f);

            // Optional: Follow the bullet
            map.FlyToLocation(endPos, duration);

            // Animate loop for moving bullet over time
            float bulletFireTime = Time.time;
            float elapsed = Time.time - bulletFireTime;
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                Vector3 pos = Vector3.Lerp(startPos, endPos, t).normalized * 0.5f;
                float altitude = Mathf.Sin(t * Mathf.PI) * arc / scale;
                map.AddMarker(sphere, pos, scale, true, altitude);
                yield return new WaitForFixedUpdate();
                elapsed = Time.time - bulletFireTime;
            }

            Destroy(sphere);

        }


        #endregion
        */
    }
}
