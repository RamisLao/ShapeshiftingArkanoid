using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlocks : MonoBehaviour
{
    [SerializeField] private GetPatternData getPatternData;
    [SerializeField] private GameObject blockPrefab;

    private List<string> currentPattern;
    private List<string> nextPattern;
    private List<GameObject> paintedPattern = new List<GameObject>();
    [SerializeField] private float graphWidth;
    [SerializeField] private float verticalCount;
    [SerializeField] private int horizontalCount;
    private float Step => graphWidth / horizontalCount;

    [SerializeField] private AudioClip boingInClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextPattern = GetRandomPatternData(getPatternData.Data);
        for (int i = 0; i < verticalCount * horizontalCount; i++)
        {
            paintedPattern.Add(null);
        }
        StartCoroutine(DelayToStart());
    }

    private List<string> GetRandomPatternData(List<List<string>> patternData)
    {
        return patternData[Random.Range(0, patternData.Count)];
    }

    private void UpdateCurrentPattern(List<string> nextPattern_, List<List<string>> patternData)
    {
        currentPattern = nextPattern_;
        nextPattern = GetRandomPatternData(patternData);
    }

    private void PaintPattern(List<string> pattern, List<GameObject> paintedPattern_)
    {
        for (int i = 0, x = 0, z = 0; i < pattern.Count; i++, x++)
        {
            if (x == horizontalCount)
            {
                x = 0;
                z += 1;
            }

            audioSource.PlayOneShot(boingInClip, Constants.BoingInVolume);

            if (pattern[i] != "0" && pattern[i] != "" &&
                pattern[i] != " " && pattern[i] != null &&
                paintedPattern_[i] == null)
            {
                GameObject block = Instantiate(blockPrefab, transform, false);
                Vector3 endScale = new Vector3(Step * 0.75f, 0.75f, 0.5f);
                block.transform.localScale = endScale * 0.5f;
                block.transform.localPosition = new Vector3(Step * x, 0.5f, z);
                paintedPattern[i] = block;
                block.GetComponent<BreakableBlock>().BoingIn(endScale);
            }
        }
    }

    private List<int>[] GetEmptyAndUsedSpacesFromPattern(List<string> pattern)
    {
        List<int> emptyIndeces = new List<int>();
        List<int> usedIndeces = new List<int>();

        for (int i = 0; i < pattern.Count; i++)
        {
            if (pattern[0] == "0")
            {
                emptyIndeces.Add(i);
            }
            else
            {
                usedIndeces.Add(i);
            }
        }

        return new List<int>[] { emptyIndeces, usedIndeces };
    }

    private void FadeOutCurrentPattern(List<GameObject> paintedPattern_, List<string> nextPattern_)
    {
        for (int i = 0; i < paintedPattern_.Count; i++)
        {
            if (nextPattern_[i] == "0" && paintedPattern_[i] != null)
            {
                paintedPattern_[i].GetComponent<BreakableBlock>().FadeOut();
            }
        }
    }

    private IEnumerator DelayToStart()
    {
        yield return new WaitForSeconds(Constants.StartDelay);
        StartCoroutine(StartPatternTimer());
    }

    private IEnumerator StartPatternTimer()
    {
        while (true)
        {
            UpdateCurrentPattern(nextPattern, getPatternData.Data);
            PaintPattern(currentPattern, paintedPattern);

            yield return new WaitForSeconds(5);

            FadeOutCurrentPattern(paintedPattern, nextPattern);

            yield return new WaitForSeconds(5);
        }
    }
}
