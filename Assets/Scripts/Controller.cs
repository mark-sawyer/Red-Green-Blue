using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
    public static int HORIZONTAL_PIXELS = 300;
    public static int VERTICAL_PIXELS = 150;
    public static GameObject[,] pixels;
    public GameObject pixel;
    public GameObject selectionBox;
    public GameObject radiusText;
    public GameObject timerText;
    private int MIN_TIMER_RESET = 1;
    private int MAX_TIMER_RESET = 20;
    private float timerReset;
    private float timer;
    private float clickRadius = 5f;
    private int MAX_RADIUS = 20;
    private int MIN_RADIUS = 1;
    private bool isRadiusMode;
    private bool paused;
    public static selectedColour colour = selectedColour.RED;

    void Start() {
        isRadiusMode = true;
        timerReset = MIN_TIMER_RESET;
        timer = MIN_TIMER_RESET / 20;
        radiusText.GetComponent<Text>().text = "radius: " + clickRadius;
        timerText.GetComponent<Text>().text = "update: 0.05s";
        radiusText.GetComponent<Text>().color = Color.red;
        selectionBox.GetComponent<SpriteRenderer>().color = Color.red;

        pixels = new GameObject[HORIZONTAL_PIXELS, VERTICAL_PIXELS];
        for (int row = 0; row < HORIZONTAL_PIXELS; row++) {
            for (int col = 0; col < VERTICAL_PIXELS; col++) {
                pixels[row, col] = Instantiate(pixel, new Vector3(row, col, 0), UnityEngine.Quaternion.identity);
                pixels[row, col].GetComponent<RGB>().setPosition(row, col);
            }
        }

        for (int row = 0; row < HORIZONTAL_PIXELS; row++) {
            for (int col = 0; col < VERTICAL_PIXELS; col++) {
                pixels[row, col].GetComponent<RGB>().setNeighbours();
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (!paused) {
            timer -= Time.deltaTime;
        }

        if (timer <= 0) {
            timer = timerReset / 20;

            for (int row = 0; row < HORIZONTAL_PIXELS; row++) {
                for (int col = 0; col < VERTICAL_PIXELS; col++) {
                    pixels[row, col].GetComponent<RGB>().setNeighboursUpdate();
                }
            }

            for (int row = 0; row < HORIZONTAL_PIXELS; row++) {
                for (int col = 0; col < VERTICAL_PIXELS; col++) {
                    pixels[row, col].GetComponent<RGB>().resolveColours();
                }
            }
        }

        if (Input.GetKeyDown("space")) {
            switch (colour) {
                case selectedColour.RED:
                    selectionBox.GetComponent<SpriteRenderer>().color = Color.green;
                    colour = selectedColour.GREEN;
                    break;
                case selectedColour.GREEN:
                    selectionBox.GetComponent<SpriteRenderer>().color = Color.blue;
                    colour = selectedColour.BLUE;
                    break;
                case selectedColour.BLUE:
                    selectionBox.GetComponent<SpriteRenderer>().color = Color.white;
                    colour = selectedColour.WHITE;
                    break;
                case selectedColour.WHITE:
                    selectionBox.GetComponent<SpriteRenderer>().color = Color.red;
                    colour = selectedColour.RED;
                    break;
            }
        }

        if (Input.GetMouseButton(0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] clickSpot = Physics2D.OverlapCircleAll(mousePos, clickRadius);
            for (int i = 0; i < clickSpot.Length; i++) {
                clickSpot[i].GetComponent<RGB>().manuallyChangeColour();
            }
        }

        if (Input.mouseScrollDelta.y != 0) {
            scrollHandling(Input.mouseScrollDelta.y);
        }

        if (Input.GetKeyDown("return")) {
            for (int row = 0; row < HORIZONTAL_PIXELS; row++) {
                for (int col = 0; col < VERTICAL_PIXELS; col++) {
                    pixels[row, col].GetComponent<RGB>().resetColour();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab)) {
            if (isRadiusMode) {
                radiusText.GetComponent<Text>().color = Color.black;
                timerText.GetComponent<Text>().color = Color.red;
                isRadiusMode = false;
            }
            else {
                radiusText.GetComponent<Text>().color = Color.red;
                timerText.GetComponent<Text>().color = Color.black;
                isRadiusMode = true;
            }
        }

        if (Input.GetKeyDown("p")) {
            paused = !paused;
        }
    }

    public void scrollHandling(float dir) {
        if (isRadiusMode) {
            if (dir > 0) {
                if (clickRadius < MAX_RADIUS) {
                    clickRadius += MIN_RADIUS;
                    radiusText.GetComponent<Text>().text = "radius: " + clickRadius;
                }
            }
            else if (dir < 0) {
                if (clickRadius > MIN_RADIUS) {
                    clickRadius -= MIN_RADIUS;
                    radiusText.GetComponent<Text>().text = "radius: " + clickRadius;
                }
            }
        }
        else {
            if (dir > 0) {
                if (timerReset < MAX_TIMER_RESET) {
                    timerReset += MIN_TIMER_RESET;
                    timerText.GetComponent<Text>().text = "update: " + string.Format("{0:N2}", timerReset / 20) + "s";
                }
            }
            else if (dir < 0) {
                if (timerReset > MIN_TIMER_RESET) {
                    timerReset -= MIN_TIMER_RESET;
                    timerText.GetComponent<Text>().text = "update: " + string.Format("{0:N2}", timerReset / 20) + "s";
                }
            }
        }
    }
}

public enum selectedColour {
    WHITE,
    RED,
    GREEN,
    BLUE
};
