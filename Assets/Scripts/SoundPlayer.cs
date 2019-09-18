using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip[] tones;
    int toneGuess, currentTone;
    AudioSource _audio;
    public bool guessing = false;
    public GameObject answerText;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlaySound(int hertz)
    {
        if (!guessing)
        {
            if (hertz == 10)
                hertz = Random.Range(0, tones.Length);
            if (hertz == 11)
                hertz = currentTone;
            currentTone = hertz;
            PlayTone(hertz);
        }
        else
        {
            toneGuess = hertz;

            if (hertz == 10)
            {
                hertz = Random.Range(0, tones.Length);
                PlayTone(hertz);
                currentTone = hertz;
            }
            else if (hertz == 11)
            {
                hertz = currentTone;
                PlayTone(hertz);
            }
            else
            {
                if(toneGuess == currentTone)
                {
                    answerText.GetComponent<Text>().text = "Correct!";
                    answerText.GetComponent<Animator>().SetTrigger("Guessed");
                }
                else
                {
                    answerText.GetComponent<Text>().text = "Wrong, try again!";
                    answerText.GetComponent<Animator>().SetTrigger("Guessed");

                }
            }

        }
    }

    public void StopTone()
    {
        _audio.Stop();
    }
  
    public void ToggleGuessing()
    {
        guessing = !guessing;
    }


    void PlayTone(int hertz)
    {
        _audio.Stop();
        _audio.PlayOneShot(tones[hertz]);
    }
}
