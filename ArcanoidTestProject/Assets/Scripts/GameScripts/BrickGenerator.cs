using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickGenerator : MonoBehaviour
{
    [SerializeField] TextAsset text;
    [SerializeField] int maxRows = 17;
    [SerializeField] int maxCol = 12;
    [SerializeField] Brick brickPrefab;
    //--
    private GameObject bricksContainer;
    private float X = -27f;
    private float Y = 20f;
    private float shiftAmountX = 5f;
    private float shiftAmountY = 2f;
    private List<Brick> reamainingBricks;
    //--
    public int currentLevel;
    public static int unDistroyable;
    public List<int[,]> levelsData;

    private void Start()
    {
        currentLevel = GameManager.lastLevel; 
        unDistroyable = 0;
        bricksContainer = new GameObject("BricksContainer");
        reamainingBricks = new List<Brick>();
        levelsData = LoadLevelsData();
        if (currentLevel >= levelsData.Count)
        {
            currentLevel = levelsData.Count - 1;
        }
        GenerateBricks();
    }

    private void GenerateBricks()
    {
        int[,] currentLevelData = levelsData[currentLevel];
        float currentX = X;
        float currentY = Y;

        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCol; col++)
            {
                int brickType = currentLevelData[row, col];

                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(brickPrefab, new Vector3(currentX, currentY, 0.0f ), Quaternion.identity);
                    newBrick.Init(bricksContainer.transform,  brickType);
                    reamainingBricks.Add(newBrick);
                    if (brickType == 3)
                    {
                        unDistroyable++;
                    }
                }

                currentX += shiftAmountX;

                if (col + 1 == maxCol)
                {
                    currentX = X;
                }
            }

            currentY -= shiftAmountY;
        }
    }

    private List<int[,]> LoadLevelsData()
    {
        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> levelsData = new List<int[,]>();
        int[,] currentLevel = new int[maxRows, maxCol];
        int currentRow = 0;

        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < bricks.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(bricks[col]);
                }
                currentRow++;
            }
            else
            {
                currentRow = 0;
                levelsData.Add(currentLevel);
                currentLevel = new int[maxRows, maxCol];
            }
        }

        return levelsData;
    }

    public void RemoveBrick(Brick brick)
    {
        reamainingBricks.Remove(brick);
        onNextLevel();
    }

    void onNextLevel()
    {
        if (reamainingBricks.Count == unDistroyable)
        {
            FindObjectOfType<BallController>().transform.position = new Vector3(0, 0, 0);
            if(currentLevel >= 2)
            {
                GameManager.lastLevel = currentLevel;
                GameManager.saveScore();
                GameManager.win = true;
                Debug.Log("you win!" + currentLevel);
            }
            else
            {
                currentLevel++;
                GameManager.lastLevel = currentLevel;
                GameManager.saveLevel();
                Debug.Log("we did it");
                GameManager.saveScore();
                SceneManager.LoadScene(1);
            }
        }
    }
}

