using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlowDetect : MonoBehaviour
{
    public int speed = 100;
    public Text VolumeText;
    private GameObject player;
    private AudioSource audio_source;
    private const float ALPHA = 0.05f;      // The alpha for the low pass
    private const int FREQUENCY = 48000;    // Wavelength, I think.
    private const int SAMPLECOUNT = 4096;   // Sample Count.
    private const int RECORDLEN = 10;
    private float[] samples;           // Samples
    private float volume;
    //private GameObject player;
    private Rigidbody plyer_rb;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        plyer_rb = player.GetComponent<Rigidbody>();
        samples = new float[SAMPLECOUNT];
        StartMicListener();
        VolumeText.text = "";
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!audio_source.isPlaying)
            StartMicListener();
        AnalyzeSound();
        VolumeText.text = "Volume" + volume.ToString();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0.0f, 1.0f, 0.0f);
        if (volume < 0.1)
        {
            plyer_rb.AddForce(movement * speed * -volume);
        }
        else
            plyer_rb.AddForce(movement * speed * volume);


    }

    private void StartMicListener()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source.clip = Microphone.Start("Built-in Microphone", false, RECORDLEN, FREQUENCY);
        while (!(Microphone.GetPosition("Built-in Microphone") > 0)) { }
        audio_source.Play();
    }

    private float GetRMS(int channel)
    {
        float sum = 0;
        audio_source.GetOutputData(samples, channel);
        for (int i = 0; i < SAMPLECOUNT; i++)
        {
            sum += Mathf.Pow(samples[i], 2);
        }

        return Mathf.Sqrt(sum / SAMPLECOUNT);
    }

    private void AnalyzeSound()
    {

        // Get all of our samples from the mic.
        audio_source.GetOutputData(samples, 0);
        volume = GetRMS(0) + GetRMS(1);
        // Sums squared samples


    }

    private void DeriveBlow()
    {

    }

    // Updates a record, by removing the oldest entry and adding the newest value (val).
    private void UpdateRecords(float val, List<float> record)
    {
    }

    //Gives a result (I don't really understand this yet) based on the peak volume of the record
    // and the previous low pass results.
    private float LowPassFilter(float peakVolume)
    {
        return 0.0f;
    }
}
