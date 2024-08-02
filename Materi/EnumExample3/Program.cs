﻿class Piece {
	public readonly PieceType pieceType;
	public readonly Colour colour;
}
enum PieceType {
	Pawn,
	Rook,
	King,
	Queen,
	Bishop,
	Knight
}
enum Colour {
	White,
	Black
}

//Berikut merupakan contoh penggunaan enum pada game chess....
//tips penggunaan enum karena enum merupakan tipe class lebih baik di buatkan class tersendiri yag berisikan kumpulan enum