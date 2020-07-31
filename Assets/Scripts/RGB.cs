using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB : MonoBehaviour {
    public SpriteRenderer sprite;
    public static bool round1;
    public selectedColour pixelColour;
    public bool redUpdate;
    public bool greenUpdate;
    public bool blueUpdate;
    public int x;
    public int y;
    private GameObject north;
    private GameObject east;
    private GameObject south;
    private GameObject west;

    public void setPosition(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void setNeighbours() {
        // Set north
        if (y != Controller.VERTICAL_PIXELS - 1) {
            north = Controller.pixels[x, y + 1];
        }
        // Set east
        if (x != Controller.HORIZONTAL_PIXELS - 1) {
            east = Controller.pixels[x + 1, y];
        }
        // Set south
        if (y != 0) {
            south = Controller.pixels[x, y - 1];
        }
        // Set west
        if (x != 0) {
            west = Controller.pixels[x - 1, y];
        }
    }

    public void setNeighboursUpdate() {
        switch (pixelColour) {
            case selectedColour.RED:
                redUpdate = true;
                break;
            case selectedColour.GREEN:
                greenUpdate = true;
                break;
            case selectedColour.BLUE:
                blueUpdate = true;
                break;
        }

        setUpdate(north);
        setUpdate(east);
        setUpdate(south);
        setUpdate(west);
    }

    public void setUpdate(GameObject neighbour) {
        if (neighbour != null) {
            switch (pixelColour) {
                case selectedColour.RED:
                    neighbour.GetComponent<RGB>().redUpdate = true;
                    break;
                case selectedColour.GREEN:
                    neighbour.GetComponent<RGB>().greenUpdate = true;
                    break;
                case selectedColour.BLUE:
                    neighbour.GetComponent<RGB>().blueUpdate = true;
                    break;
            }
        }
    }

    public void resolveColours() {
        if (redUpdate & greenUpdate & blueUpdate) {
            pixelColour = selectedColour.WHITE;
        }
        else if (redUpdate & greenUpdate) {
            pixelColour = selectedColour.RED;
        }
        else if (greenUpdate & blueUpdate) {
            pixelColour = selectedColour.GREEN;
        }
        else if (blueUpdate & redUpdate) {
            pixelColour = selectedColour.BLUE;
        }
        else if (redUpdate) {
            pixelColour = selectedColour.RED;
        }
        else if (greenUpdate) {
            pixelColour = selectedColour.GREEN;
        }
        else if (blueUpdate) {
            pixelColour = selectedColour.BLUE;
        }

        switch (pixelColour) {
            case selectedColour.RED:
                sprite.color = Color.red;
                break;
            case selectedColour.GREEN:
                sprite.color = Color.green;
                break;
            case selectedColour.BLUE:
                sprite.color = Color.blue;
                break;
            default:
                sprite.color = Color.white;
                break;
        }

        redUpdate = false;
        greenUpdate = false;
        blueUpdate = false;
    }

    public void manuallyChangeColour() {
        switch (Controller.colour) {
            case selectedColour.RED:
                pixelColour = selectedColour.RED;
                sprite.color = Color.red;
                break;
            case selectedColour.GREEN:
                pixelColour = selectedColour.GREEN;
                sprite.color = Color.green;
                break;
            case selectedColour.BLUE:
                pixelColour = selectedColour.BLUE;
                sprite.color = Color.blue;
                break;
            case selectedColour.WHITE:
                pixelColour = selectedColour.WHITE;
                sprite.color = Color.white;
                break;
        }

        redUpdate = false;
        greenUpdate = false;
        blueUpdate = false;
    }

    public void resetColour() {
        pixelColour = selectedColour.WHITE;
        redUpdate = false;
        greenUpdate = false;
        blueUpdate = false;
        sprite.color = Color.white;
    }
}
