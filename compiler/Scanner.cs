
using System;
using System.IO;
using System.Collections;

namespace Refal {

public class Token {
	public int kind;    // token kind
	public int pos;     // token position in the source text (starting at 0)
	public int col;     // token column (starting at 0)
	public int line;    // token line (starting at 1)
	public string val;  // token value
	public Token next;  // ML 2005-03-11 Peek tokens are kept in linked list
}

public class Buffer {
	public const char EOF = (char) 256;
	const int MAX_BUFFER_LENGTH = 64 * 1024; // 64KB
	static byte[] buf;    // input buffer
	static int bufStart;  // position of first byte in buffer relative to input stream
	static int bufLen;    // length of buffer
	static int fileLen;   // length of input stream
	static int pos;       // current position in buffer
	static Stream stream; // input stream (seekable)
	static bool isUserStream; // was the stream opened by the user?
	
	public static void Fill (Stream s, bool isUserStream) {
		stream = s; Buffer.isUserStream = isUserStream;
		fileLen = bufLen = (int) s.Length;
		if (stream.CanSeek && bufLen > MAX_BUFFER_LENGTH) bufLen = MAX_BUFFER_LENGTH;
		buf = new byte[bufLen];
		bufStart = Int32.MaxValue; // nothing in the buffer so far
		Pos = 0; // setup  buffer to position 0 (start)
		if (bufLen == fileLen) Close();
	}
	
	// called at the end of Parser.Parse()
	public static void Close() {
		if (!isUserStream && stream != null) {
			stream.Close();
			stream = null;
		}
	}
	
	public static int Read () {
		if (pos < bufLen) {
			return buf[pos++];
		} else if (Pos < fileLen) {
			Pos = Pos; // shift buffer start to Pos
			return buf[pos++];
		} else {
			return EOF;
		}
	}

	public static int Peek () {
		if (pos < bufLen) {
			return buf[pos];
		} else if (Pos < fileLen) {
			Pos = Pos; // shift buffer start to pos
			return buf[pos];
		} else {
			return EOF;
		}
	}
	
	public static string GetString (int beg, int end) {
		int len = end - beg;
		char[] buf = new char[len];
		int oldPos = Pos;
		Pos = beg;
		for (int i = 0; i < len; ++i) buf[i] = (char) Read();
		Pos = oldPos;
		return new String(buf);
	}

	public static int Pos {
		get { return pos + bufStart; }
		set {
			if (value < 0) value = 0;
			else if (value > fileLen) value = fileLen;
			if (value >= bufStart && value < bufStart + bufLen) { // already in buffer
				pos = value - bufStart;
			} else if (stream != null) { // must be swapped in
				stream.Seek(value, SeekOrigin.Begin);
				bufLen = stream.Read(buf, 0, buf.Length);
				bufStart = value; pos = 0;
			} else {
				pos = fileLen - bufStart; // make Pos return fileLen
			}
		}
	}
}

public class Scanner {
	const char EOL = '\n';
	const int eofSym = 0; /* pdt */
	const int charSetSize = 256;
	const int maxT = 34;
	const int noSym = 34;
	static short[] start = {
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  3,  0, 49,  0, 59,  7, 40, 44,  0,  0, 58,  0, 16,  0,
	 48, 47, 47, 47, 47, 47, 47, 47, 47, 47, 60, 38, 42, 30, 46,  0,
	  1,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,
	  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2, 39,  0, 43,  0,  0,
	  0,  2,  2,  2,  2, 64,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,
	  2,  2,  2, 65, 66,  2,  2,  2,  2,  2,  2, 41,  0, 45,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  -1};


	static Token t;          // current token
	static char ch;          // current input character
	static int pos;          // column number of current character
	static int line;         // line number of current character
	static int lineStart;    // start position of current line
	static int oldEols;      // EOLs that appeared in a comment;
	static BitArray ignore;  // set of characters to be ignored by the scanner

	static Token tokens;     // list of tokens already peeked (first token is a dummy)
	static Token pt;         // current peek token
	
	static char[] tval = new char[128]; // text of current token
	static int tlen;         // length of current token
	
	public static void Init (string fileName) {
		try {
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			Buffer.Fill(stream, false);
			Init();
		} catch (IOException) {
			Console.WriteLine("--- Cannot open file {0}", fileName);
			System.Environment.Exit(1);
		}
	}
	
	public static void Init (Stream s) {
		Buffer.Fill(s, true);
		Init();
	}

	private static void Init() {
		pos = -1; line = 1; lineStart = 0;
		oldEols = 0;
		NextCh();
		ignore = new BitArray(charSetSize+1);
		ignore[' '] = true;  // blanks are always white space
		ignore[9] = true; ignore[10] = true; ignore[13] = true; 
		pt = tokens = new Token();  // first token is a dummy
	}
	
	static void NextCh() {
		if (oldEols > 0) { ch = EOL; oldEols--; } 
		else {
			ch = (char)Buffer.Read(); pos++;
			// replace isolated '\r' by '\n' in order to make
			// eol handling uniform across Windows, Unix and Mac
			if (ch == '\r' && Buffer.Peek() != '\n') ch = EOL;
			if (ch == EOL) { line++; lineStart = pos + 1; }
		}

	}

	static void AddCh() {
		if (tlen >= tval.Length) {
			char[] newBuf = new char[2 * tval.Length];
			Array.Copy(tval, 0, newBuf, 0, tval.Length);
			tval = newBuf;
		}
		tval[tlen++] = ch;
		NextCh();
	}


	static bool Comment0() {
		int level = 1, line0 = line, lineStart0 = lineStart;
		NextCh();
			for(;;) {
				if (ch == 10) {
					level--;
					if (level == 0) { oldEols = line - line0; NextCh(); return true; }
					NextCh();
				} else if (ch == Buffer.EOF) return false;
				else NextCh();
			}
	}

	static bool Comment1() {
		int level = 1, line0 = line, lineStart0 = lineStart;
		NextCh();
			for(;;) {
				if (ch == 13) {
					level--;
					if (level == 0) { oldEols = line - line0; NextCh(); return true; }
					NextCh();
				} else if (ch == Buffer.EOF) return false;
				else NextCh();
			}
	}

	static bool Comment2() {
		int level = 1, line0 = line, lineStart0 = lineStart;
		NextCh();
		if (ch == '*') {
			NextCh();
			for(;;) {
				if (ch == '*') {
					NextCh();
					if (ch == '/') {
						level--;
						if (level == 0) { oldEols = line - line0; NextCh(); return true; }
						NextCh();
					}
				} else if (ch == '/') {
					NextCh();
					if (ch == '*') {
						level++; NextCh();
					}
				} else if (ch == Buffer.EOF) return false;
				else NextCh();
			}
		} else {
			if (ch==EOL) {line--; lineStart = lineStart0;}
			pos = pos - 2; Buffer.Pos = pos+1; NextCh();
		}
		return false;
	}


	static void CheckLiteral() {
		switch (t.val) {
			case "bool": t.kind = 7; break;
			case "char": t.kind = 8; break;
			case "float": t.kind = 9; break;
			case "int": t.kind = 10; break;
			case "null": t.kind = 11; break;
			case "object": t.kind = 12; break;
			case "str": t.kind = 13; break;
			case "True": t.kind = 14; break;
			case "False": t.kind = 15; break;
			case "else": t.kind = 16; break;
			default: break;
		}
	}

	static Token NextToken() {
		while (ignore[ch]) NextCh();
		if (ch == '*' && Comment0() ||ch == '*' && Comment1() ||ch == '/' && Comment2()) return NextToken();
		t = new Token();
		t.pos = pos; t.col = pos - lineStart + 1; t.line = line; 
		int state = start[ch];
		tlen = 0; AddCh();
		
		switch (state) {
			case -1: { t.kind = eofSym; break; } // NextCh already done
			case 0: { t.kind = noSym; break; }   // NextCh already done
			case 1:
				if ((ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z')) {AddCh(); goto case 2;}
				else {t.kind = noSym; break;}
			case 2:
				if ((ch == '-' || ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) {AddCh(); goto case 2;}
				else {t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 3:
				if ((ch <= '!' || ch >= '#' && ch <= '[' || ch >= ']')) {AddCh(); goto case 3;}
				else if (ch == '"') {AddCh(); goto case 6;}
				else if (ch == 92) {AddCh(); goto case 4;}
				else {t.kind = noSym; break;}
			case 4:
				if ((ch >= ' ' && ch <= '~')) {AddCh(); goto case 5;}
				else {t.kind = noSym; break;}
			case 5:
				if ((ch <= '!' || ch >= '#' && ch <= '/' || ch >= ':' && ch <= '@' || ch >= 'G' && ch <= '[' || ch >= ']' && ch <= '`' || ch >= 'g')) {AddCh(); goto case 3;}
				else if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f')) {AddCh(); goto case 50;}
				else if (ch == '"') {AddCh(); goto case 6;}
				else if (ch == 92) {AddCh(); goto case 4;}
				else {t.kind = noSym; break;}
			case 6:
				{t.kind = 2; break;}
			case 7:
				if ((ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '&' || ch >= '(' && ch <= '[' || ch >= ']')) {AddCh(); goto case 7;}
				else if (ch == 39) {AddCh(); goto case 10;}
				else if (ch == 92) {AddCh(); goto case 8;}
				else {t.kind = noSym; break;}
			case 8:
				if ((ch >= ' ' && ch <= '~')) {AddCh(); goto case 9;}
				else {t.kind = noSym; break;}
			case 9:
				if ((ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '&' || ch >= '(' && ch <= '/' || ch >= ':' && ch <= '@' || ch >= 'G' && ch <= '[' || ch >= ']' && ch <= '`' || ch >= 'g')) {AddCh(); goto case 7;}
				else if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f')) {AddCh(); goto case 51;}
				else if (ch == 39) {AddCh(); goto case 10;}
				else if (ch == 92) {AddCh(); goto case 8;}
				else {t.kind = noSym; break;}
			case 10:
				{t.kind = 3; break;}
			case 11:
				if ((ch == 'L' || ch == 'l')) {AddCh(); goto case 15;}
				else {t.kind = 4; break;}
			case 12:
				if ((ch == 'U' || ch == 'u')) {AddCh(); goto case 15;}
				else {t.kind = 4; break;}
			case 13:
				if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f')) {AddCh(); goto case 14;}
				else {t.kind = noSym; break;}
			case 14:
				if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f')) {AddCh(); goto case 14;}
				else if ((ch == 'L' || ch == 'U')) {AddCh(); goto case 15;}
				else if (ch == 'u') {AddCh(); goto case 11;}
				else if (ch == 'l') {AddCh(); goto case 12;}
				else {t.kind = 4; break;}
			case 15:
				{t.kind = 4; break;}
			case 16:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 17;}
				else {t.kind = noSym; break;}
			case 17:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 17;}
				else if ((ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm')) {AddCh(); goto case 29;}
				else if ((ch == 'E' || ch == 'e')) {AddCh(); goto case 18;}
				else {t.kind = 5; break;}
			case 18:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 20;}
				else if ((ch == '+' || ch == '-')) {AddCh(); goto case 19;}
				else {t.kind = noSym; break;}
			case 19:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 20;}
				else {t.kind = noSym; break;}
			case 20:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 20;}
				else if ((ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm')) {AddCh(); goto case 29;}
				else {t.kind = 5; break;}
			case 21:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 22;}
				else {t.kind = noSym; break;}
			case 22:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 22;}
				else if ((ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm')) {AddCh(); goto case 29;}
				else if ((ch == 'E' || ch == 'e')) {AddCh(); goto case 23;}
				else {t.kind = 5; break;}
			case 23:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 25;}
				else if ((ch == '+' || ch == '-')) {AddCh(); goto case 24;}
				else {t.kind = noSym; break;}
			case 24:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 25;}
				else {t.kind = noSym; break;}
			case 25:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 25;}
				else if ((ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm')) {AddCh(); goto case 29;}
				else {t.kind = 5; break;}
			case 26:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 28;}
				else if ((ch == '+' || ch == '-')) {AddCh(); goto case 27;}
				else {t.kind = noSym; break;}
			case 27:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 28;}
				else {t.kind = noSym; break;}
			case 28:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 28;}
				else if ((ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm')) {AddCh(); goto case 29;}
				else {t.kind = 5; break;}
			case 29:
				{t.kind = 5; break;}
			case 30:
				{t.kind = 6; break;}
			case 31:
				if (ch == 'T') {AddCh(); goto case 32;}
				else {t.kind = noSym; break;}
			case 32:
				if (ch == 'R') {AddCh(); goto case 33;}
				else {t.kind = noSym; break;}
			case 33:
				if (ch == 'Y') {AddCh(); goto case 34;}
				else {t.kind = noSym; break;}
			case 34:
				{t.kind = 17; break;}
			case 35:
				if (ch == 'N') {AddCh(); goto case 37;}
				else {t.kind = noSym; break;}
			case 36:
				if (ch == 'L') {AddCh(); goto case 37;}
				else {t.kind = noSym; break;}
			case 37:
				{t.kind = 18; break;}
			case 38:
				{t.kind = 19; break;}
			case 39:
				{t.kind = 20; break;}
			case 40:
				{t.kind = 21; break;}
			case 41:
				{t.kind = 22; break;}
			case 42:
				{t.kind = 23; break;}
			case 43:
				{t.kind = 24; break;}
			case 44:
				{t.kind = 25; break;}
			case 45:
				{t.kind = 26; break;}
			case 46:
				{t.kind = 27; break;}
			case 47:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 47;}
				else if ((ch == 'L' || ch == 'U')) {AddCh(); goto case 15;}
				else if (ch == 'u') {AddCh(); goto case 11;}
				else if (ch == 'l') {AddCh(); goto case 12;}
				else if (ch == '.') {AddCh(); goto case 21;}
				else if ((ch == 'E' || ch == 'e')) {AddCh(); goto case 26;}
				else if ((ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm')) {AddCh(); goto case 29;}
				else {t.kind = 4; break;}
			case 48:
				if ((ch >= '0' && ch <= '9')) {AddCh(); goto case 47;}
				else if ((ch == 'L' || ch == 'U')) {AddCh(); goto case 15;}
				else if (ch == 'u') {AddCh(); goto case 11;}
				else if (ch == 'l') {AddCh(); goto case 12;}
				else if ((ch == 'X' || ch == 'x')) {AddCh(); goto case 13;}
				else if (ch == '.') {AddCh(); goto case 21;}
				else if ((ch == 'E' || ch == 'e')) {AddCh(); goto case 26;}
				else if ((ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm')) {AddCh(); goto case 29;}
				else {t.kind = 4; break;}
			case 49:
				if (ch == 'E') {AddCh(); goto case 52;}
				else {t.kind = noSym; break;}
			case 50:
				if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f')) {AddCh(); goto case 50;}
				else if ((ch <= '!' || ch >= '#' && ch <= '/' || ch >= ':' && ch <= '@' || ch >= 'G' && ch <= '[' || ch >= ']' && ch <= '`' || ch >= 'g')) {AddCh(); goto case 3;}
				else if (ch == '"') {AddCh(); goto case 6;}
				else if (ch == 92) {AddCh(); goto case 4;}
				else {t.kind = noSym; break;}
			case 51:
				if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f')) {AddCh(); goto case 51;}
				else if ((ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '&' || ch >= '(' && ch <= '/' || ch >= ':' && ch <= '@' || ch >= 'G' && ch <= '[' || ch >= ']' && ch <= '`' || ch >= 'g')) {AddCh(); goto case 7;}
				else if (ch == 39) {AddCh(); goto case 10;}
				else if (ch == 92) {AddCh(); goto case 8;}
				else {t.kind = noSym; break;}
			case 52:
				if (ch == 'N') {AddCh(); goto case 31;}
				else if (ch == 'X') {AddCh(); goto case 53;}
				else {t.kind = noSym; break;}
			case 53:
				if (ch == 'T') {AddCh(); goto case 54;}
				else {t.kind = noSym; break;}
			case 54:
				if (ch == 'R') {AddCh(); goto case 35;}
				else if (ch == 'E') {AddCh(); goto case 55;}
				else {t.kind = noSym; break;}
			case 55:
				if (ch == 'R') {AddCh(); goto case 56;}
				else {t.kind = noSym; break;}
			case 56:
				if (ch == 'N') {AddCh(); goto case 57;}
				else {t.kind = noSym; break;}
			case 57:
				if (ch == 'A') {AddCh(); goto case 36;}
				else {t.kind = 18; break;}
			case 58:
				{t.kind = 28; break;}
			case 59:
				{t.kind = 29; break;}
			case 60:
				{t.kind = 30; break;}
			case 61:
				{t.kind = 31; break;}
			case 62:
				{t.kind = 32; break;}
			case 63:
				{t.kind = 33; break;}
			case 64:
				if ((ch == '-' || ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) {AddCh(); goto case 2;}
				else if (ch == '.') {AddCh(); goto case 61;}
				else {t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 65:
				if ((ch == '-' || ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) {AddCh(); goto case 2;}
				else if (ch == '.') {AddCh(); goto case 62;}
				else {t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 66:
				if ((ch == '-' || ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) {AddCh(); goto case 2;}
				else if (ch == '.') {AddCh(); goto case 63;}
				else {t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}

		}
		t.val = new String(tval, 0, tlen);
		return t;
	}
	
	// get the next token (possibly a token already seen during peeking)
	public static Token Scan () {
		if (tokens.next == null) {
			return NextToken();
		} else {
			pt = tokens = tokens.next;
			return tokens;
		}
	}

	// peek for the next token, ignore pragmas
	public static Token Peek () {
		if (pt.next == null) {
			do {
				pt = pt.next = NextToken();
			} while (pt.kind > maxT); // skip pragmas
		} else {
			do {
				pt = pt.next; 
			} while (pt.kind > maxT);
		}
		return pt;
	}
	
	// make sure that peeking starts at current scan position
	public static void ResetPeek () { pt = tokens; }

} // end Scanner

}