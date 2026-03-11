Chess Prototype — Unity ♟️
✅ Implemented
8x8 grid generation

All chess pieces with proper movement rules

Turn-based (White starts)

Selection via mouse, Confirm (Space), Cancel (Escape)

Highlight: 🟡 moves / 🔴 attacks

Zenject + Command pattern

🚧 Planned

Check / checkmate

Pawn promotion

Castling

Visual polish

UI improvements

## 🔄 Update — March 12, 2026

### ChessValidator & Move Legality
- Implemented `IsCheck(team)` to detect if the king is under attack
- Added virtual move simulation in `MoveUnitCommand` to block moves that leave the king in check
- Created `IsMoveLegal` helper for per-move validation
- Refactored command constructors to inject `ChessValidator` dependency
- Fixed rollback logic to properly restore captured units during validation

### Next in progress:
- Full checkmate detection
