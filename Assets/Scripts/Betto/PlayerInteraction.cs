using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

enum Direction
{
    Up,
    Down,
    Left,
    Right
};

// An interface added to the main class
interface IPlayerInteraction
{

    // Pick up the item in front of the tile direction. Returns the item in front (probably the object)
    string PickUp(Vector3 position, Direction direction);

    // Drop the item in the next tile in the specified direction. Returns whether it was succesful or not.
    bool DropItem(Vector3 position, string item, Direction direction);

    // Launch chrono-bomb in the specified direction.
    void ThrowChronoBomb(Vector3 position, Direction direction);
}
