using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squish : MonoBehaviour
{
    [SerializeField]
    float squish_period = 0.2f;
    [SerializeField]
    float squish_mag = 0.1f;

    Vector3 original_scale = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        original_scale = transform.localScale;

        StartCoroutine(Squishing());
    }

    IEnumerator Squishing()
    {
        yield return null;

        int pol = 1;

        // random wait so things aren't in sync with each other
        yield return new WaitForSeconds(Random.Range(0, 0.5f));

        while (true)
        {
            float progress = 0;
            float time_elapsed = 0f;

            // Stretch up
            while (progress < 1)
            {
                time_elapsed += Time.deltaTime;
                progress = time_elapsed / squish_period;

                float stretch = Mathf.Lerp(0, squish_mag * pol, progress);
                float squeeze = Mathf.Lerp(0, -squish_mag * pol, progress);

                transform.localScale = original_scale + new Vector3(stretch, squeeze, 0);
                yield return null;
            }

            progress = 0;
            time_elapsed = 0f;

            // Squish Down
            while (progress < 1)
            {
                time_elapsed += Time.deltaTime;
                progress = time_elapsed / squish_period;

                float stretch = Mathf.Lerp(squish_mag * pol, 0, progress);
                float squeeze = Mathf.Lerp(-squish_mag * pol, 0, progress);

                transform.localScale = original_scale + new Vector3(stretch, squeeze, 0);
                yield return null;
            }

            pol *= -1;
        }
    }
}
