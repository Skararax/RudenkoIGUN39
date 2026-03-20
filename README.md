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

## 🔄 Update — March 15, 2026
- Refactored ChessValidator: removed MonoBehaviour, now pure C# class
- Added IInitializable/ITickable/IDisposable interfaces for lifecycle management
- Injected dependencies via constructor (Battlefield)
- Updated SceneInstaller: registered ChessValidator as single instance
- Improved testability and separation of concerns

## 🔄 Update — March 17, 2026
- Full check detection — king under attack blocks illegal moves
- Checkmate detection — no legal moves available ends the game
- Virtual move simulation to validate king safety

## 🔄 Update — March 21, 2026
- Pawn promotion implemented — pawn now transforms into chosen piece upon reaching the last rank

- UI selection panel with buttons for Rook, Knight, Bishop, Queen

- Clean architecture — PawnChanger handles UI and spawning, BattleController manages game flow

- Bug fixes — fixed missing reference issues, proper cleanup of old pawn, correct cell-unit binding