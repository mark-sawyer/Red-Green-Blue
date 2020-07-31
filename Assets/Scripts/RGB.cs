using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB : MonoBehaviour {
    public SpriteRenderer sprite;
    public static bool round1;
    public bool red;
    public bool green;
    public bool blue;
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
        if(red) {
            redUpdate = true;
        }
        else if (green) {
            greenUpdate = true;
        }
        else if (blue) {
            blueUpdate = true;
        }
        setUpdate(north);
        setUpdate(east);
        setUpdate(south);
        setUpdate(west);
    }

    public void setUpdate(GameObject neighbour) {
        if (neighbour != null) {
            if (red) {
                neighbour.GetComponent<RGB>().redUpdate = true;
            }
            else if (green) {
                neighbour.GetComponent<RGB>().greenUpdate = true;
            }
            else if (blue) {
                neighbour.GetComponent<RGB>().blueUpdate = true;
            }
        }
    }

    public void resolveColours() {
        if (redUpdate & greenUpdate & blueUpdate) {
            red = false;
            green = false;
            blue = false;
        }
        else if (redUpdate & greenUpdate) {
            red = true;
            green = false;
            blue = false;
        }
        else if (greenUpdate & blueUpdate) {
            red = false;
            green = true;
            blue = false;
        }
        else if (blueUpdate & redUpdate) {
            red = false;
            green = false;
            blue = true;
        }
        else if (redUpdate) {
            red = true;
            green = false;
            blue = false;
        }
        else if (greenUpdate) {
            red = false;
            green = true;
            blue = false;
        }
        else if (blueUpdate) {
            red = false;
            green = false;
            blue = true;
        }

        if (red) {
            sprite.color = Color.red;
        }
        else if (green) {
            sprite.color = Color.green;
        }
        else if (blue) {
            sprite.color = Color.blue;
        }
        else {
            sprite.color = Color.white;
        }

        redUpdate = false;
        greenUpdate = false;
        blueUpdate = false;
    }

    public void manuallyChangeColour() {
        switch (Controller.colour) {
            case selectedColour.RED:
                red = true;
                green = false;
                blue = false;
                sprite.color = Color.red;
                break;
            case selectedColour.GREEN:
                red = false;
                green = true;
                blue = false;
                sprite.color = Color.green;
                break;
            case selectedColour.BLUE:
                red = false;
                green = false;
                blue = true;
                sprite.color = Color.blue;
                break;
            case selectedColour.WHITE:
                red = false;
                green = false;
                blue = false;
                sprite.color = Color.white;
                break;
        }

        redUpdate = false;
        greenUpdate = false;
        blueUpdate = false;
    }

    public void resetColour() {
        red = false;
        green = false;
        blue = false;
        redUpdate = false;
        greenUpdate = false;
        blueUpdate = false;
    }
}
