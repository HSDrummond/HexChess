using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Material[] teamMaterials;

    private Piece[,] pieces;

    [SerializeField]
    private HexGrid hexGrid;

    [SerializeField]
    private MovementSystem movementSystem;

    public bool PlayersTurn { get; private set; } = true;

    [SerializeField]
    private Piece selectedUnit;
    private Hex previouslySelectedHex;

    private void Awake()
    {
        SpawnAllPieces();
        //SpawnSinglePiece(PieceType.King, 0);
    }
    public void HandleUnitSelected(GameObject unit)
    {
        if (PlayersTurn == false)
            return;

        Piece unitReference = unit.GetComponent<Piece>();

        if (CheckIfTheSameUnitSelected(unitReference))
            return;

        PrepareUnitForMovement(unitReference);
    }

    private bool CheckIfTheSameUnitSelected(Piece unitReference)
    {
        if (this.selectedUnit == unitReference)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }

    public void HandleTerrainSelected(GameObject hexGO)
    {
        if (selectedUnit == null || PlayersTurn == false)
        {
            return;
        }

        Hex selectedHex = hexGO.GetComponent<Hex>();

        if (HandleHexOutOfRange(selectedHex.HexCoords) || HandleSelectedHexIsUnitHex(selectedHex.HexCoords))
            return;

        HandleTargetHexSelected(selectedHex);

    }

    private void PrepareUnitForMovement(Piece unitReference)
    {
        if (this.selectedUnit != null)
        {
            ClearOldSelection();
        }

        this.selectedUnit = unitReference;
        this.selectedUnit.Select();
        movementSystem.ShowRange(this.selectedUnit, this.hexGrid);
    }

    private void ClearOldSelection()
    {
        previouslySelectedHex = null;
        this.selectedUnit.Deselect();
        movementSystem.HideRange(this.hexGrid);
        this.selectedUnit = null;

    }

    private void HandleTargetHexSelected(Hex selectedHex)
    {
        if (previouslySelectedHex == null || previouslySelectedHex != selectedHex)
        {
            previouslySelectedHex = selectedHex;
            movementSystem.ShowPath(selectedHex.HexCoords, this.hexGrid);
        }
        else
        {
            movementSystem.MoveUnit(selectedUnit, this.hexGrid);
            PlayersTurn = false;
            selectedUnit.MovementFinished += ResetTurn;
            ClearOldSelection();

        }
    }

    private bool HandleSelectedHexIsUnitHex(Vector3Int hexPosition)
    {
        if (hexPosition == hexGrid.GetClosestHex(selectedUnit.transform.position))
        {
            selectedUnit.Deselect();
            ClearOldSelection();
            return true;
        }
        return false;
    }

    private bool HandleHexOutOfRange(Vector3Int hexPosition)
    {
        if (movementSystem.IsHexInRange(hexPosition) == false)
        {
            Debug.Log("Hex Out of range!");
            return true;
        }
        return false;
    }

    private void ResetTurn(Piece selectedUnit)
    {
        selectedUnit.MovementFinished -= ResetTurn;
        PlayersTurn = true;
    }

    //Spawning of the pieces
    private void SpawnAllPieces()
    {
        //pieces = new Piece[hexGrid.GetTileAt(transform.position)];
        //pieces = new Piece[TILE_COUNT_X, TILE_COUNT_Y];

        int whiteTeam = 0, blackTeam = 1;

        //White Team
        pieces[1, 0] = SpawnSinglePiece(PieceType.Spear, whiteTeam);
        /*pieces[2, 0] = SpawnSinglePiece(PieceType.Crossbow, whiteTeam);
        pieces[3, 0] = SpawnSinglePiece(PieceType.Boomerang, whiteTeam);
        pieces[4, 0] = SpawnSinglePiece(PieceType.King, whiteTeam);
        pieces[5, 0] = SpawnSinglePiece(PieceType.Longbow, whiteTeam);
        pieces[6, 0] = SpawnSinglePiece(PieceType.Boomerang, whiteTeam);
        pieces[7, 0] = SpawnSinglePiece(PieceType.Crossbow, whiteTeam);
        pieces[8, 0] = SpawnSinglePiece(PieceType.Spear, whiteTeam);
        pieces[4, 1] = SpawnSinglePiece(PieceType.Crossbow, whiteTeam);
        pieces[4, 2] = SpawnSinglePiece(PieceType.Shield, whiteTeam);
        pieces[5, 2] = SpawnSinglePiece(PieceType.Shield, whiteTeam);
        pieces[0, 3] = SpawnSinglePiece(PieceType.Shield, whiteTeam);
        pieces[8, 3] = SpawnSinglePiece(PieceType.Shield, whiteTeam);*/

        //Black Team

    }

    private Piece SpawnSinglePiece(PieceType type, int team)
    {
        Piece cp = Instantiate(prefabs[(int)type - 1], transform).GetComponent<Piece>();

        cp.type = type;
        cp.team = team;
        cp.GetComponent<MeshRenderer>().material = teamMaterials[team];

        return cp;
    }
}